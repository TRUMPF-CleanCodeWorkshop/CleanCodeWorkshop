using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silbentrenner.Client;

namespace Silbentrenner.Host
{
    

    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var mainWindow = new MainWindow();

            mainWindow.DataContext = new MainViewModel();

            mainWindow.Show();

            var app = new App();
            app.
           
        }
    }
}
