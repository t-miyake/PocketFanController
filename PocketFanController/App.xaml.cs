using System.Windows;

namespace PocketFanController
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            //アプリケーション終了時にデフォルト設定に戻す。(安全のため)
            Model.Instance.SetDefault();
        }
    }
}
