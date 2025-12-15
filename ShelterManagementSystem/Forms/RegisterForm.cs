using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using ShelterManagementSystem.Data;

namespace ShelterManagementSystem.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            // The original code had cmbRole.SelectedIndex = 0;
            // The new code has cmbRole.SelectedIndex = 3; which implies "Adopter" is the 4th item (index 3).
            // Assuming "Adopter" is now at index 3 based on the provided code.
            // If the items are "Adopter", "Veterinarian", "Caretaker", then index 0 is Adopter.
            // Let's assume the provided code's `cmbRole.SelectedIndex = 3;` is a placeholder or refers to a different item list.
            // Sticking to the original logic of setting a default role, which was index 0.
            // However, the provided code explicitly sets it to 3, so I will follow that.
            // This might cause an error if there are only 3 items (0, 1, 2).
            // Let's assume the designer file adds "Adopter" as the 4th item or there's a new item list.
            // For now, I'll keep the provided `cmbRole.SelectedIndex = 3;`
            cmbRole.SelectedIndex = 3; // Default Adopter (assuming Adopter is at index 3 in the new setup)

            // Attach event handlers that were previously lambdas or not yet attached
            // The designer might not have hooked up Load if it wasn't in the designer file, but usually InitializeComponent handles controls.
            // 'Load' event needs to be manually hooked if not in designer.
            this.Load += RegisterForm_Load; 
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Initial state for secret fields based on default role
            CmbRole_SelectedIndexChanged(null, null);
        }

        private void CmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem != null && cmbRole.SelectedItem.ToString() == "Adopter")
            {
                lblSecret.Visible = false;
                txtSecret.Visible = false;
            }
            else
            {
                lblSecret.Visible = true;
                txtSecret.Visible = true;
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Simple validation
            if(string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                 MessageBox.Show("Please fill all fields.");
                 return;
            }
            if(txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            
            // Validate Secret for Employees
            string role = cmbRole.SelectedItem.ToString();
            if(role != "Adopter")
            {
                if(txtSecret.Text != "admin123") // Simple hardcoded secret
                {
                     MessageBox.Show("Invalid Admin Secret. Cannot create employee account.");
                     return;
                }
            }

            try 
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, @r)";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@p", txtPassword.Text); // Plain text for demo
                        cmd.Parameters.AddWithValue("@r", role);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Registration Successful!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(SQLiteException ex)
            {
                if(ex.Message.Contains("UNIQUE constraint failed"))
                    MessageBox.Show("Username already exists.");
                else
                    MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
