using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using ShelterManagementSystem.Data;

namespace ShelterManagementSystem.Forms
{
    public partial class UserProfileForm : Form
    {
        private int UserId;

        public UserProfileForm(int userId)
        {
            UserId = userId;
            InitializeComponent();
            UIHelper.StyleGrid(dtGrid);
            LoadHistory();
        }

        private void LoadHistory()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT a.Name, a.Species, r.RequestDate, r.Status 
                    FROM Adoptions r
                    JOIN Animals a ON r.AnimalID = a.AnimalID
                    WHERE r.UserID = @uid
                    ORDER BY r.RequestDate DESC";

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@uid", UserId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtGrid.DataSource = dt;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOld.Text) || string.IsNullOrWhiteSpace(txtNew.Text))
            {
                MessageBox.Show("Please fill fields.");
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                // 1. Verify Old
                string sqlCheck = "SELECT Count(*) FROM Users WHERE UserID=@id AND Password=@p";
                using(var cmd = new SQLiteCommand(sqlCheck, conn))
                {
                    cmd.Parameters.AddWithValue("@id", UserId);
                    cmd.Parameters.AddWithValue("@p", txtOld.Text);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if(count == 0)
                    {
                        MessageBox.Show("Old password incorrect.");
                        return;
                    }
                }
                
                // 2. Update
                string sqlUpd = "UPDATE Users SET Password=@p WHERE UserID=@id";
                using(var cmd = new SQLiteCommand(sqlUpd, conn))
                {
                    cmd.Parameters.AddWithValue("@p", txtNew.Text);
                    cmd.Parameters.AddWithValue("@id", UserId);
                    cmd.ExecuteNonQuery();
                }
                
                MessageBox.Show("Password updated!");
                txtOld.Clear();
                txtNew.Clear();
            }
        }
    }
}
