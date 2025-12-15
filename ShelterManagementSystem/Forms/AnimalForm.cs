using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using ShelterManagementSystem.Data;

namespace ShelterManagementSystem.Forms
{
    public partial class AnimalForm : Form
    {
        private int selectedId = -1;
        private string UserRole;
        private int CurrentUserId; 
        
        public AnimalForm(string role, int userId = 0)
        {
            UserRole = role;
            CurrentUserId = userId;
            InitializeComponent();
            UIHelper.StyleGrid(grid);
            grid.SelectionChanged += Grid_SelectionChanged;
            LoadCombos();
            LoadData();
            ApplyRolePermissions();
        }
        
        private void ApplyRolePermissions()
        {
            if (UserRole == "Adopter")
            {
                // Adopters can't manage data
                btnAdd.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                
                // Read-only fields
                txtName.Enabled = false;
                txtSpecies.Enabled = false;
                cmbGender.Enabled = false;
                dtAdmission.Enabled = false;
                cmbCity.Enabled = false;
                cmbEnclosure.Enabled = false;
                cmbHealth.Enabled = false;
                
                btnAdopt.Visible = true;
                btnAdopt.Enabled = false; // Enabled only on selection
            }
            else if (UserRole == "Caretaker")
            {
                // Caretaker Read-only? Or maybe update Feed times? Specs said: "statuslarni gorebilsin" (view status)
                btnAdd.Visible = false;
                btnUpdate.Visible = false; // Just view
                btnDelete.Visible = false;
            }
            else if (UserRole == "Veterinarian")
            {
                // Vet can Update Health. Cannot Delete.
                btnDelete.Visible = false; 
                // Can Update.
                // Should lock other fields? Ideally yes, but for now allow editing.
            }
            // Admin has full access (Default)
        }

        private void LoadCombos()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                
                SQLiteDataAdapter daCity = new SQLiteDataAdapter("SELECT CityID, CityName FROM Cities", conn);
                DataTable dtCity = new DataTable();
                daCity.Fill(dtCity);
                cmbCity.DisplayMember = "CityName";
                cmbCity.ValueMember = "CityID";
                cmbCity.DataSource = dtCity;

                SQLiteDataAdapter daEnc = new SQLiteDataAdapter("SELECT EnclosureID, EnclosureName FROM Enclosures", conn);
                DataTable dtEnc = new DataTable();
                daEnc.Fill(dtEnc);
                cmbEnclosure.DisplayMember = "EnclosureName";
                cmbEnclosure.ValueMember = "EnclosureID";
                cmbEnclosure.DataSource = dtEnc;
            }
        }

        private void LoadData()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT a.AnimalID, a.Name, a.Species, a.Gender, a.AdmissionDate, 
                           c.CityName, e.EnclosureName, a.CityID, a.EnclosureID,
                           a.HealthStatus, a.AdoptionStatus
                    FROM Animals a
                    LEFT JOIN Cities c ON a.CityID = c.CityID
                    LEFT JOIN Enclosures e ON a.EnclosureID = e.EnclosureID";
                
                // Adopters see only Available
                if (UserRole == "Adopter")
                {
                    sql += " WHERE a.AdoptionStatus = 'Available'";
                }

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                grid.Columns["CityID"].Visible = false;
                grid.Columns["EnclosureID"].Visible = false;
            }
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count > 0)
            {
                var row = grid.SelectedRows[0];
                selectedId = Convert.ToInt32(row.Cells["AnimalID"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtSpecies.Text = row.Cells["Species"].Value.ToString();
                cmbGender.SelectedItem = row.Cells["Gender"].Value.ToString();
                // Safe parsing
                DateTime dt;
                if(DateTime.TryParse(row.Cells["AdmissionDate"].Value.ToString(), out dt)) 
                     dtAdmission.Value = dt;

                if (row.Cells["CityID"].Value != DBNull.Value)
                    cmbCity.SelectedValue = Convert.ToInt32(row.Cells["CityID"].Value);
                if (row.Cells["EnclosureID"].Value != DBNull.Value)
                    cmbEnclosure.SelectedValue = Convert.ToInt32(row.Cells["EnclosureID"].Value);
                if (row.Cells["HealthStatus"].Value != DBNull.Value)
                    cmbHealth.SelectedItem = row.Cells["HealthStatus"].Value.ToString();

                string adoptStatus = row.Cells["AdoptionStatus"].Value.ToString();

                // Permission Logic for Actions
                if(UserRole == "Admin")
                {
                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else if (UserRole == "Veterinarian")
                {
                    btnAdd.Enabled = true;
                    btnUpdate.Enabled = true; // To update Health
                }
                else if (UserRole == "Adopter")
                {
                    btnAdopt.Enabled = (adoptStatus == "Available");
                }
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
            txtSpecies.Clear();
            cmbGender.SelectedIndex = 0;
            dtAdmission.Value = DateTime.Now;
            if(cmbCity.Items.Count > 0) cmbCity.SelectedIndex = 0;
            if(cmbEnclosure.Items.Count > 0) cmbEnclosure.SelectedIndex = 0;
            if(cmbHealth.Items.Count > 0) cmbHealth.SelectedIndex = 0;

            if(UserRole != "Adopter" && UserRole != "Caretaker") btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAdopt.Enabled = false;
            grid.ClearSelection();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Name is required."); return false; }
            if (string.IsNullOrWhiteSpace(txtSpecies.Text)) { MessageBox.Show("Species is required."); return false; }
            if (cmbGender.SelectedItem == null) { MessageBox.Show("Gender is required."); return false; }
            if (cmbCity.SelectedValue == null) { MessageBox.Show("City is required."); return false; }
            if (cmbEnclosure.SelectedValue == null) { MessageBox.Show("Enclosure is required."); return false; }
            if (cmbHealth.SelectedItem == null) { MessageBox.Show("Health status is required."); return false; }
            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Animals (Name, Species, Gender, AdmissionDate, EnclosureID, CityID, HealthStatus, AdoptionStatus) VALUES (@n, @s, @g, @d, @e, @c, @h, 'Available')";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@s", txtSpecies.Text.Trim());
                    cmd.Parameters.AddWithValue("@g", cmbGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@d", dtAdmission.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@e", cmbEnclosure.SelectedValue);
                    cmd.Parameters.AddWithValue("@c", cmbCity.SelectedValue);
                    cmd.Parameters.AddWithValue("@h", cmbHealth.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            ResetForm();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == -1) return;
            if (!ValidateInputs()) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE Animals SET Name=@n, Species=@s, Gender=@g, AdmissionDate=@d, EnclosureID=@e, CityID=@c, HealthStatus=@h WHERE AnimalID=@id";
                // Only Vet should update Health ideally, but Admin can too.
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@s", txtSpecies.Text.Trim());
                    cmd.Parameters.AddWithValue("@g", cmbGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@d", dtAdmission.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@e", cmbEnclosure.SelectedValue);
                    cmd.Parameters.AddWithValue("@c", cmbCity.SelectedValue);
                    cmd.Parameters.AddWithValue("@h", cmbHealth.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@id", selectedId);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            ResetForm();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1) return;
            if(UserRole != "Admin") 
            {
                MessageBox.Show("Only Admin can delete animals.");
                return;
            }
            
            if (MessageBox.Show("Are you sure? This cannot be undone.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                 using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM Animals WHERE AnimalID = @id";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                ResetForm();
            }
        }
        
        private void BtnAdopt_Click(object sender, EventArgs e)
        {
             if (selectedId == -1) return;
             
             if(MessageBox.Show("Do you want to adopt " + txtName.Text + "?", "Adopt", MessageBoxButtons.YesNo) == DialogResult.Yes)
             {
                 using (var conn = DatabaseHelper.GetConnection())
                 {
                     conn.Open();
                     using(var trans = conn.BeginTransaction())
                     {
                         try
                         {
                             // 1. Create Request
                             string sqlReq = "INSERT INTO Adoptions (AnimalID, UserID, RequestDate, Status) VALUES (@aid, @uid, @date, 'Pending')";
                             using(var cmd = new SQLiteCommand(sqlReq, conn))
                             {
                                 cmd.Parameters.AddWithValue("@aid", selectedId);
                                 cmd.Parameters.AddWithValue("@uid", CurrentUserId); // TODO: Get Real User ID
                                 cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                                 cmd.ExecuteNonQuery();
                             }
                             
                             // 2. Update Animal Status
                             string sqlUpd = "UPDATE Animals SET AdoptionStatus = 'Pending' WHERE AnimalID = @id";
                             using(var cmd = new SQLiteCommand(sqlUpd, conn))
                             {
                                 cmd.Parameters.AddWithValue("@id", selectedId);
                                 cmd.ExecuteNonQuery();
                             }
                             
                             trans.Commit();
                             MessageBox.Show("Adoption request sent! Waiting for Admin approval.");
                         }
                         catch(Exception ex)
                         {
                             trans.Rollback();
                             MessageBox.Show("Error: " + ex.Message);
                         }
                     }
                 }
                 LoadData();
                 ResetForm();
             }
        }
    }
}
