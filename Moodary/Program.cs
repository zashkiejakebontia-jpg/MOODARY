using System;
using System.Windows.Forms;

namespace Moodary
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // .NET Framework 4.7.2 does not support SetHighDpiMode here.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form5());
        }
    }
}
