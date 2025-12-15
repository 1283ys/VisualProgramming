using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShelterManagementSystem.Forms
{
    public partial class MainForm : Form
    {
        private string UserRole;
        private int UserId;
        private Button currentButton;

        // Colors (Still used for dynamic button styling)
        private Color colorSidebar = Color.FromArgb(51, 51, 76);
        private Color colorActive = Color.FromArgb(70, 70, 100); 

        public MainForm(string role, int userId)
        {
            UserRole = role;
            UserId = userId;
            InitializeComponent();
            SetupMenu();
        }

        private void SetupMenu()
        {
            if (UserRole == "Admin")
            {
                AddSidebarButton("Adoption Requests", () => OpenChildForm(new AdoptionForm(), lblTitle));
                AddSidebarButton("Cities", () => OpenChildForm(new CityForm(), lblTitle));
                AddSidebarButton("Enclosures", () => OpenChildForm(new EnclosureForm(), lblTitle));
                AddSidebarButton("Employees", () => OpenChildForm(new EmployeeForm(), lblTitle));
            }
            
            // Pass UserId to AnimalForm
            AddSidebarButton("Animals", () => OpenChildForm(new AnimalForm(UserRole, UserId), lblTitle));
        }

        private void AddSidebarButton(string text, Action onClick)
        {
            Button btn = new Button()
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            
            btn.Click += (s, e) =>
            {
                ActivateButton(s);
                onClick?.Invoke();
            };

            pnlMenu.Controls.Add(btn); 
            // Important: To make sure new buttons appear at the TOP of the panel (standard stack),
            // we usually just Add using Dock.Top. The "first added" stays at the bottom if using default z-order? 
            // Actually Dock.Top stacks from top.
            // But we need to make sure they are in correct order. 
            // If we use Controls.Add(btn) with Dock.Top, the last added one goes to the VERY TOP?
            // Windows Forms Docking: Last added control with Dock=Top is placed at the top edge, PUSHING previous ones down?
            // No, the one at index 0 (Front) is processed first (or last?).
            // Let's rely on standard Add. If order is reversed, we can utilize BringToFront.
            btn.BringToFront(); 
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = colorActive;
                    currentButton.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in pnlMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = colorSidebar;
                    previousBtn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }
        }

        private void OpenChildForm(Form childForm, Label titleLabel)
        {
            if (pnlContent.Controls.Count > 0)
            {
                Form activeForm = pnlContent.Controls[0] as Form;
                if (activeForm != null)
                {
                    activeForm.Close();
                }
                else
                {
                    pnlContent.Controls[0].Dispose();
                }
                pnlContent.Controls.Clear();
            }

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            
            titleLabel.Text = childForm.Text;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide(); 
            new LoginForm().ShowDialog(); 
            this.Close(); 
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UserProfileForm(UserId), lblTitle);
        }

        private void lblLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
