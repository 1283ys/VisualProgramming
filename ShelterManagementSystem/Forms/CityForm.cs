using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using ShelterManagementSystem.Data;

namespace ShelterManagementSystem.Forms
{
    public partial class CityForm : Form
    {
        private int selectedId = -1;

        public CityForm()
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
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM Cities", conn);
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
                selectedId = Convert.ToInt32(row.Cells["CityID"].Value);
                txtName.Text = row.Cells["CityName"].Value.ToString();
                txtZip.Text = row.Cells["ZipCode"].Value.ToString();
                
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                ResetForm();
            }
        }

        private void ResetForm()
        {
            selectedId = -1;
            txtName.Clear();
            txtZip.Clear();
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            grid.ClearSelection();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("City Name is required."); return false; }
            if (string.IsNullOrWhiteSpace(txtZip.Text)) { MessageBox.Show("Zip Code is required."); return false; }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Cities (CityName, ZipCode) VALUES (@n, @z)"; 
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@z", txtZip.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            ResetForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == -1) return;
            if (!ValidateInputs()) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE Cities SET CityName = @n, ZipCode = @z WHERE CityID = @id";
                using(var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@z", txtZip.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", selectedId);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            ResetForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1) return;
            if (MessageBox.Show("Are you sure? This might affect animals/employees.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM Cities WHERE CityID = @id";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        try 
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Error deleting: " + ex.Message);
                        }
                    }
                }
                LoadData();
                ResetForm();
            }
        }

        private void txtZip_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
