using System;
using System.Windows.Forms;
using ShelterManagementSystem.Data;
using ShelterManagementSystem.Forms;

namespace ShelterManagementSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Re-initialize to apply schema changes if file was deleted
            DatabaseHelper.InitializeDatabase();

            Application.Run(new LoginForm());
        }
    }
}