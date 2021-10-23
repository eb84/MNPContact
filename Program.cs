using System;
using System.Windows.Forms;

namespace MNPContact
{
    static class Program
    {
        internal static ContactModel __contactModel = new ContactModel();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
