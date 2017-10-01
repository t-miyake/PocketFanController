using System.Windows;
using System.Windows.Input;

namespace PocketFanController
{
    public class ConfigWindowViewModel : ViewModelBase
    {
        private Model Model = Model.Instance;

        public ICommand LoadDefaultButton { get; }
        public ICommand OkButton { get; }
        public ICommand CancelButton { get; }
        public ICommand ApplyButton { get; }

        public int Margin
        {
            get => Model.ManualMargin;
            set
            {
                Model.ManualMargin = value;
                OnPropertyChanged("Margin");
            }
        }

        public int BorderOfSlow
        {
            get => Model.ManualT0;
            set
            {
                Model.ManualT0 =value;
                OnPropertyChanged("BorderOfSlow");
            }
        }
        public int BorderOfFast
        {
            get => Model.ManualT1;
            set
            {
                Model.ManualT1 = value;
                OnPropertyChanged("BorderOfFast");
            }
        }
        public int BorderOfFastest
        {
            get => Model.ManualT2;
            set
            {
                Model.ManualT2 = value;
                OnPropertyChanged("BorderOfFastest");
            }
        }

        public ConfigWindowViewModel()
        {
            LoadDefaultButton = new RelayCommand(() =>
            {
                Margin = 5;
                BorderOfSlow = 40;
                BorderOfFast = 60;
                BorderOfFastest = 75;
            });

            OkButton = new RelayCommand(() =>
            {
                Model.SaveManualConfig(Margin, BorderOfSlow, BorderOfFast, BorderOfFastest);
                SetManual();
                Application.Current.MainWindow.Close();
            });

            ApplyButton = new RelayCommand(() =>
            {
                Model.SaveManualConfig(Margin, BorderOfSlow, BorderOfFast, BorderOfFastest);
                SetManual();
                MessageBox.Show("Apply completed.");
            });

            CancelButton = new RelayCommand(() =>
            {
                Application.Current.MainWindow.Close();
            });

            Model.GetManualConfigs();
            UpdateStatus();
        }

        private void SetManual()
        {
            if (Model.CurrentState == 5)
            {
                Model.SetManual();
            }
        }

        private void UpdateStatus()
        {
            OnPropertyChanged("Margin");
            OnPropertyChanged("BorderOfSlow");
            OnPropertyChanged("BorderOfFast");
            OnPropertyChanged("BorderOfFastest");
        }
    }
}