using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using ShelterManagementSystem.Data;

namespace ShelterManagementSystem.Forms
{
    public partial class EnclosureForm : Form
    {
        private int selectedId = -1;

        public EnclosureForm()
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
                var daCity = new SQLiteDataAdapter("SELECT CityID, CityName FROM Cities", conn);
                var dtCity = new DataTable();
                daCity.Fill(dtCity);
                cmbCity.DisplayMember = "CityName";
                cmbCity.ValueMember = "CityID";
                cmbCity.DataSource = dtCity;
            }
        }

        private void LoadData()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT e.EnclosureID, e.EnclosureName, e.Capacity, 
                           e.CityID, c.CityName
                    FROM Enclosures e
                    LEFT JOIN Cities c ON e.CityID = c.CityID";
                
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                grid.Columns["CityID"].Visible = false;
            }
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count > 0)
            {
                var row = grid.SelectedRows[0];
                selectedId = Convert.ToInt32(row.Cells["EnclosureID"].Value);
                txtName.Text = row.Cells["EnclosureName"].Value.ToString();
                txtCap.Text = row.Cells["Capacity"].Value.ToString();
                if(row.Cells["CityID"].Value != DBNull.Value) cmbCity.SelectedValue = Convert.ToInt32(row.Cells["CityID"].Value);

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
            txtCap.Clear();
            if(cmbCity.Items.Count > 0) cmbCity.SelectedIndex = 0;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            grid.ClearSelection();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Name is required."); return false; }
            if (!int.TryParse(txtCap.Text, out _)) { MessageBox.Show("Capacity must be a number."); return false; }
            if (cmbCity.SelectedValue == null) { MessageBox.Show("Location/City is required."); return false; }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Enclosures (EnclosureName, Capacity, CityID) VALUES (@n, @c, @l)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@c", int.Parse(txtCap.Text));
                    cmd.Parameters.AddWithValue("@l", cmbCity.SelectedValue);
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
                string sql = "UPDATE Enclosures SET EnclosureName=@n, Capacity=@c, CityID=@l WHERE EnclosureID=@id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@c", int.Parse(txtCap.Text));
                    cmd.Parameters.AddWithValue("@l", cmbCity.SelectedValue);
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
            if(MessageBox.Show("Delete enclosure?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using(var cmd = new SQLiteCommand("DELETE FROM Enclosures WHERE EnclosureID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        try 
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch(Exception ex)
                        {
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
