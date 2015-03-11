using System;
using System.Drawing;
using GameOfLife.BitmapAdapter;
using GameOfLife.GameLogic;
using GameOfLife.UI;
using UI;

namespace GameOfLife.Host
{

    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var mainWindow = new MainWindow();
            var mainViewModel = new MainViewModel();

            CreateLoadBitmapFeature(mainViewModel);
            CreateGameSimulationFeature(mainViewModel);

            mainWindow.DataContext = mainViewModel;

            mainWindow.ShowDialog();

        }

        private static void CreateGameSimulationFeature(MainViewModel mainViewModel)
        {
            mainViewModel.CalculateNextGeneration += () =>
            {
                var nextGeneration = GameOfLiveSimulation.CalculateNextGeneration(mainViewModel.Cells);

                mainViewModel.Cells = nextGeneration;
            };
        }

        private static void CreateLoadBitmapFeature(MainViewModel mainViewModel)
        {
            mainViewModel.LoadButtonCommand.Executed += () =>
            {
                var cells = CellLoader.Load(mainViewModel.BitMapFilePath, new Size(500,500));

                mainViewModel.Cells = cells;
            };

        }
    }
}
