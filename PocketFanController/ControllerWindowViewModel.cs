using System.Windows.Input;

namespace PocketFanController
{
    public class ControllerWindowViewModel : ViewModelBase
    {
        private Model Model = Model.Instance;

        public ICommand SetDefault { get; }
        public ICommand SetManual { get; }
        public ICommand SetSlow { get; }
        public ICommand SetFast { get; }
        public ICommand SetFastest { get; }
        public ICommand SetSlowest { get; }

        public ControllerWindowViewModel()
        {
            SetDefault = new RelayCommand(()=>
            {
                Model.SetDefault();
                CurrentState = 0;
            });

            SetManual = new RelayCommand(() =>
            {
                Model.SetManual();
                CurrentState = 5;
            });

            SetSlow = new RelayCommand(() =>
            {
                Model.SetSlow();
                CurrentState = 3;
            });

            SetFast = new RelayCommand(() =>
            {
                Model.SetFast();
                CurrentState = 2;
            });

            SetFastest = new RelayCommand(() =>
            {
                Model.SetFastest();
                CurrentState = 1;
            });

            SetSlowest = new RelayCommand(() =>
            {
                Model.SetSlowest();
                CurrentState = 4;
            });

            Model.GetCurrentStatus();
        }

        public int CurrentState
        {
            get => Model.CurrentState;
            set
            {
                Model.CurrentState = value;
                OnPropertyChanged("CurrentStateText");
            }
        }

        public string CurrentStateText
        {
            get
            {
                switch (CurrentState)
                {
                    case 0:
                        return "Current status : Auto (Default)";
                    case 1:
                        return "Current status : Fastest";
                    case 2:
                        return "Current status : Fast";
                    case 3:
                        return "Current status : Slow";
                    case 4:
                        return "Current status : Slowest";
                    case 5:
                        return "Current status : Auto (Manual)";
                    default:
                        return "Current status : Auto (Default)";
                }
            }
        }

    }
}