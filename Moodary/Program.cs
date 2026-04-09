using System;
using System.Windows.Forms;

namespace Moodary
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); // Makes controls look modern
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form5()); // Starts the app with Form1
        }
    }
}

