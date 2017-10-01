using System.Windows;

namespace PocketFanController
{
    /// <summary>
    /// ConfigWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
            var vm = new ConfigWindowViewModel();
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = Close;
        }
    }
}
