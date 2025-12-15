using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using ShelterManagementSystem.Data;

namespace ShelterManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            if(reg.ShowDialog() == DialogResult.OK)
            {
                lblError.Text = "Account created! Please login.";
                lblError.ForeColor = Color.Green;
                lblError.Visible = true;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            int userId = -1;
            string role = "";

            // 1. Fetch credentials and CLOSE connection immediately
            using (var conn = DatabaseHelper.GetConnection())
            {
                try 
                {
                    conn.Open();
                    string sql = "SELECT UserID, Role FROM Users WHERE Username = @u AND Password = @p";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", password);
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                 userId = Convert.ToInt32(reader["UserID"]);
                                 role = reader["Role"].ToString();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("DB Error: " + ex.Message);
                    return;
                }
            } // Connection is CLOSED here

            // 2. Open Main Form if credentials valid
            if (userId != -1 && !string.IsNullOrEmpty(role))
            {
                 // Hide Login Form
                 this.Hide();
                 
                 // Show Main Form
                 MainForm main = new MainForm(role, userId);
                 main.ShowDialog();
                 
                 // Close Login Form after Main Form closes
                 this.Close();
            }
            else
            {
                lblError.Text = "Invalid credentials.";
                lblError.ForeColor = Color.Red;
                lblError.Visible = true;
            }
        }
    }
}
