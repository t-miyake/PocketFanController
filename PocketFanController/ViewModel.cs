using System.Windows.Input;

namespace PocketFanController
{
    public class ViewModel:ViewModelBase
    {
        private Model Model = Model.Instance;

        public ICommand SetDefault { get; }
        public ICommand SetLow { get; }
        public ICommand SetMiddle { get; }
        public ICommand SetFast { get; }

        public ViewModel()
        {
            SetDefault = new RelayCommand(()=>
            {
                Model.SetDefault();
                CurrentState = 0;
            });

            SetLow = new RelayCommand(() =>
            {
                Model.SetLow();
                CurrentState = 3;
            });

            SetMiddle = new RelayCommand(() =>
            {
                Model.SetMiddle();
                CurrentState = 2;
            });

            SetFast = new RelayCommand(() =>
            {
                Model.SetFast();
                CurrentState = 1;
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
                    default:
                        return "Current status : Auto (Default)";
                }
            }
        }

    }
}