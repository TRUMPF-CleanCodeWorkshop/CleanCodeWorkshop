using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Silbentrenner.Client
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            LadeText = new FlowCommand(() => File.Exists(SourceFileName));
            SpeichereText = new FlowCommand(() => TargetFileName.Length > 0);
            TrenneText = new FlowCommand(() => SourceText.Length > 0);
            MaxCharacters = 20;
        }

        private string m_SourceFileName = "";
        private string m_TargetFileName = "";
        private string m_SourceText = "";
        private string m_SplittedText = "";
        private int m_MaxCharacters;

        public event PropertyChangedEventHandler PropertyChanged;

        public FlowCommand LadeText { get; set; }
        public FlowCommand SpeichereText { get; set; }
        public FlowCommand TrenneText { get; set; }

        public string SourceFileName
        {
            get { return m_SourceFileName; }
            set
            {
                m_SourceFileName = value;
                OnPropertyChanged();
                LadeText.CheckCanExecute();
            }
        }

        public string TargetFileName
        {
            get { return m_TargetFileName; }
            set
            {
                m_TargetFileName = value;
                OnPropertyChanged();
                SpeichereText.CheckCanExecute();
            }
        }

        public string SourceText
        {
            get { return m_SourceText; }
            set
            {
                m_SourceText = value;
                OnPropertyChanged();
                TrenneText.CheckCanExecute();
            }
        }

        public string SplittedText
        {
            get { return m_SplittedText; }
            set { m_SplittedText = value; OnPropertyChanged(); }
        }

        public int MaxCharacters
        {
            get { return m_MaxCharacters; }
            set { m_MaxCharacters = value; OnPropertyChanged(); }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
