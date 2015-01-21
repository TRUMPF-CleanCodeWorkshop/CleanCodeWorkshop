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
            var app = new App();
            var mainViewModel = new MainViewModel();

            mainWindow.DataContext = mainViewModel;

            Erstelle_LadeText_Feature(mainViewModel);

            mainWindow.Show();
            app.MainWindow = mainWindow;
            app.Run();

        }

        private static void Erstelle_LadeText_Feature(MainViewModel mainViewModel)
        {
            mainViewModel.LadeText.Executed += () =>
            {
                var text = Fileadapter.Fileadapter.ReadTextFromFile(mainViewModel.SourceFileName);
                mainViewModel.SourceText = text;
            };
        }
    }
}
