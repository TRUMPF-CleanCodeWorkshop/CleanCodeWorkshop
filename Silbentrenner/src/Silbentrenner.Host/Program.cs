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
            //Build
            var mainWindow = new MainWindow();
            var app = new App();
            var mainViewModel = new MainViewModel();

            // Configure
            mainWindow.DataContext = mainViewModel;

            // Wire
            Erstelle_LadeText_Feature(mainViewModel);
            Erstelle_SpeichereText_Feature(mainViewModel);
            Erstelle_TrenneText_Feature(mainViewModel);

            // Run
            mainWindow.Show();
            app.MainWindow = mainWindow;
            app.Run();

        }

        private static void Erstelle_TrenneText_Feature(MainViewModel mainViewModel)
        {
            mainViewModel.TrenneText.Executed += () =>
            {
                var text = Logik.Logik.ErsetzeCRLFDurchSpace(mainViewModel.SourceText);
                text = Logik.Logik.BereinigenVonLeerzeichen(text);
                var woerter = Logik.Logik.TextInWoerterZerlegen(text);
                woerter = Logik.Logik.WoerterInSilbenTrennen(woerter);
                var zeilen = Logik.Logik.WoerterZuZeilenZusammensetzen(woerter, mainViewModel.MaxCharacters);

                mainViewModel.SplittedText = string.Join(Environment.NewLine, zeilen);
            };
        }

        private static void Erstelle_SpeichereText_Feature(MainViewModel mainViewModel)
        {
            mainViewModel.SpeichereText.Executed += () => { Fileadapter.Fileadapter.SaveToOutputFile(mainViewModel.SplittedText, mainViewModel.TargetFileName); };
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
