using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using ShelterManagementSystem.Data;

namespace ShelterManagementSystem.Forms
{
    public partial class AdoptionForm : Form
    {
        private int selectedId = -1;
        private int selectedAnimalId = -1;

        public AdoptionForm()
        {
            InitializeComponent();
            UIHelper.StyleGrid(grid);
            grid.SelectionChanged += Grid_SelectionChanged;
            LoadData();
        }

        private void LoadData()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT r.AdoptionID, r.AnimalID, a.Name AS AnimalName, u.Username AS AdopterName, r.RequestDate, r.Status
                    FROM Adoptions r
                    JOIN Animals a ON r.AnimalID = a.AnimalID
                    JOIN Users u ON r.UserID = u.UserID
                    WHERE r.Status = 'Pending'";
                
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
            }
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count > 0)
            {
                var row = grid.SelectedRows[0];
                selectedId = Convert.ToInt32(row.Cells["AdoptionID"].Value);
                selectedAnimalId = Convert.ToInt32(row.Cells["AnimalID"].Value);
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
            }
            else
            {
                selectedId = -1;
                selectedAnimalId = -1;
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            ProcessRequest("Approved");
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            ProcessRequest("Rejected");
        }

        private void ProcessRequest(string newStatus)
        {
            if (selectedId == -1) return;
            
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try 
                    {
                        // 1. Update Request Status
                        string sqlReq = "UPDATE Adoptions SET Status = @s WHERE AdoptionID = @id";
                        using(var cmd = new SQLiteCommand(sqlReq, conn))
                        {
                            cmd.Parameters.AddWithValue("@s", newStatus);
                            cmd.Parameters.AddWithValue("@id", selectedId);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Update Animal Status
                        string animalStatus = (newStatus == "Approved") ? "Adopted" : "Available";
                        string sqlAni = "UPDATE Animals SET AdoptionStatus = @as WHERE AnimalID = @aid";
                        using (var cmd = new SQLiteCommand(sqlAni, conn))
                        {
                            cmd.Parameters.AddWithValue("@as", animalStatus);
                            cmd.Parameters.AddWithValue("@aid", selectedAnimalId);
                            cmd.ExecuteNonQuery();
                        }
                        
                        trans.Commit();
                        MessageBox.Show("Request " + newStatus);
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            LoadData();
        }
    }
}
