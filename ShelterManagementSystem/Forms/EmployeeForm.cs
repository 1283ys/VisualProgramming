using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using ShelterManagementSystem.Data;

namespace ShelterManagementSystem.Forms
{
    public partial class EmployeeForm : Form
    {
        private int selectedId = -1;

        public EmployeeForm()
        {
            InitializeComponent();
            UIHelper.StyleGrid(grid);
            grid.SelectionChanged += Grid_SelectionChanged;
            LoadCombos();
            LoadData();
        }

        private void LoadCombos()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                
                // Cities
                var daCity = new SQLiteDataAdapter("SELECT CityID, CityName FROM Cities", conn);
                var dtCity = new DataTable();
                daCity.Fill(dtCity);
                cmbCity.DisplayMember = "CityName";
                cmbCity.ValueMember = "CityID";
                cmbCity.DataSource = dtCity;

                // Enclosures
                var daEnc = new SQLiteDataAdapter("SELECT EnclosureID, EnclosureName FROM Enclosures", conn);
                var dtEnc = new DataTable();
                daEnc.Fill(dtEnc);
                // Add "None" option?
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
                    SELECT e.EmployeeID, e.FirstName, e.LastName, e.Position, 
                           e.CityID, c.CityName, 
                           e.EnclosureID, enc.EnclosureName
                    FROM Employees e
                    LEFT JOIN Cities c ON e.CityID = c.CityID
                    LEFT JOIN Enclosures enc ON e.EnclosureID = enc.EnclosureID";
                
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
                selectedId = Convert.ToInt32(row.Cells["EmployeeID"].Value);
                txtFirst.Text = row.Cells["FirstName"].Value.ToString();
                txtLast.Text = row.Cells["LastName"].Value.ToString();
                txtPos.Text = row.Cells["Position"].Value.ToString();
                if(row.Cells["CityID"].Value != DBNull.Value) cmbCity.SelectedValue = Convert.ToInt32(row.Cells["CityID"].Value);
                if(row.Cells["EnclosureID"].Value != DBNull.Value) cmbEnclosure.SelectedValue = Convert.ToInt32(row.Cells["EnclosureID"].Value);

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
            txtFirst.Clear(); 
            txtLast.Clear(); 
            txtPos.Clear(); 
            if(cmbCity.Items.Count > 0) cmbCity.SelectedIndex = 0;
            if(cmbEnclosure.Items.Count > 0) cmbEnclosure.SelectedIndex = 0;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            grid.ClearSelection();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirst.Text)) { MessageBox.Show("First Name is required."); return false; }
            if (string.IsNullOrWhiteSpace(txtLast.Text)) { MessageBox.Show("Last Name is required."); return false; }
            if (string.IsNullOrWhiteSpace(txtPos.Text)) { MessageBox.Show("Position is required."); return false; }
            if (cmbCity.SelectedValue == null) { MessageBox.Show("City is required."); return false; }
            if (cmbEnclosure.SelectedValue == null) { MessageBox.Show("Workplace is required."); return false; }
            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Employees (FirstName, LastName, Position, CityID, EnclosureID) VALUES (@f, @l, @p, @c, @e)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@f", txtFirst.Text.Trim());
                    cmd.Parameters.AddWithValue("@l", txtLast.Text.Trim());
                    cmd.Parameters.AddWithValue("@p", txtPos.Text.Trim());
                    cmd.Parameters.AddWithValue("@c", cmbCity.SelectedValue);
                    cmd.Parameters.AddWithValue("@e", cmbEnclosure.SelectedValue);
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
                string sql = "UPDATE Employees SET FirstName=@f, LastName=@l, Position=@p, CityID=@c, EnclosureID=@e WHERE EmployeeID=@id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@f", txtFirst.Text.Trim());
                    cmd.Parameters.AddWithValue("@l", txtLast.Text.Trim());
                    cmd.Parameters.AddWithValue("@p", txtPos.Text.Trim());
                    cmd.Parameters.AddWithValue("@c", cmbCity.SelectedValue);
                    cmd.Parameters.AddWithValue("@e", cmbEnclosure.SelectedValue);
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
             if(MessageBox.Show("Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
             {
                 using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using(var cmd = new SQLiteCommand("DELETE FROM Employees WHERE EmployeeID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                ResetForm();
             }
        }
    }
}
