namespace GameOfLife.UI
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    using GameOfLife.Contracts;
    using GameOfLife.UI.Annotations;

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int mapWidth;

        private int mapHeight;

        public MainViewModel()
        {
            this.LoadButtonCommand = new FlowCommand();
            this.gameTimer = new Timer(10);
            this.gameTimer.Elapsed += (sender, args) => this.OnCalculateNextGeneration();
            this.mapWidth  = 500;
            this.mapHeight = 500;
            this.Cells = new Cells();
            this.BitMapFilePath = @"C:\Data\Development\Katas\Trumpf CleanCodeWorkshop\GameOfLive\lib\Patterns\Images\glider.gif";
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Cells cells;
        
        public Cells Cells
        {
            get
            {
                return this.cells;
            }

            set
            {
                if (this.cells == value)
                {
                    return;
                }

                this.cells = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged("Population");
                this.OnPropertyChanged("Generation");
                this.Map = MapDrawer.DrawMap(this.mapWidth, this.mapHeight, value);
            }
        }

        private string bitMapFilePath;

        public string BitMapFilePath
        {
            get { return this.bitMapFilePath; } 
            set
            {
                if (this.bitMapFilePath == value)
                {
                    return;
                }

                this.bitMapFilePath = value;
                this.OnPropertyChanged();
            }
        }

        public int Population
        {
            get { return this.Cells.Population; } 
        }

        public int Generation
        {
            get { return this.Cells.Generation; }
        }

        private BitmapSource map;

        public BitmapSource Map
        {
            get { return this.map; }
            set
            {
                if (object.Equals(this.map, value))
                {
                    return;
                }

                this.map = value;
                this.OnPropertyChanged();
            }
        }

        public FlowCommand LoadButtonCommand { get; set; }

        public ICommand StartButtonCommand
        {
            get { return new FlowCommand(this.StartTimer); }
        }

        internal void StartTimer()
        {
            this.gameTimer.Start();
        }

        public event Action CalculateNextGeneration;

        protected virtual void OnCalculateNextGeneration()
        {
            var handler = this.CalculateNextGeneration;
            if (handler != null)
            {
                handler();
            }
        }

        private readonly Timer gameTimer;
    }
}

