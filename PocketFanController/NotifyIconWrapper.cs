using System;
using System.ComponentModel;
using System.Windows;

namespace PocketFanController
{
    public partial class NotifyIconWrapper : Component
    {
        private Model Model = Model.Instance;

        public NotifyIconWrapper()
        {
            InitializeComponent();

            toolStripMenuItem_Exit.Click += Exit;
            toolStripMenuItem_OpenWindow.Click += OpenWindow;
            toolStripMenuItem_OpenAbout.Click += OpenAbout;

            toolStripMenuItem_SetDefault.Click += SetDefault;
            toolStripMenuItem_SetFastest.Click += SetFastest;
            toolStripMenuItem_SetFast.Click += SetFast;
            toolStripMenuItem_SetSlow.Click += SetSlow;
            toolStripMenuItem_SetSlowest.Click += SetSlowest;

            //ちょっと無理やりだけど、アイコンをクリックするたびにステータスを更新する
            notifyIcon1.Click += UpdateCurrentStatus;

            UpdateCurrentStatus(null,null);
        }

        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void SetDefault (object sender, EventArgs e)
        {
            Model.SetDefault();
        }

        private void SetFastest(object sender, EventArgs e)
        {
            Model.SetFastest();
        }

        private void SetFast(object sender, EventArgs e)
        {
            Model.SetFast();
        }

        private void SetSlow(object sender, EventArgs e)
        {
            Model.SetSlow();
        }

        private void SetSlowest(object sender, EventArgs e)
        {
            Model.SetSlowest();
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenWindow(object sender, EventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void OpenAbout(object sender, EventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void UpdateCurrentStatus(object sender, EventArgs e)
        {
            toolStripMenuItem_CpuTemp.Text = "CPU " + Model.GetCpuTemp() + "℃";

            Model.GetCurrentStatus();

            switch (Model.CurrentState)
            {
                case 0:
                    toolStripMenuItem_SetDefault.Checked = true;
                    toolStripMenuItem_SetFastest.Checked=false;
                    toolStripMenuItem_SetFast.Checked = false;
                    toolStripMenuItem_SetSlow.Checked = false;
                    toolStripMenuItem_SetSlowest.Checked = false;
                    break;
                case 1:
                    toolStripMenuItem_SetDefault.Checked = false;
                    toolStripMenuItem_SetFastest.Checked = true;
                    toolStripMenuItem_SetFast.Checked = false;
                    toolStripMenuItem_SetSlow.Checked = false;
                    toolStripMenuItem_SetSlowest.Checked = false;
                    break;
                case 2:
                    toolStripMenuItem_SetDefault.Checked = false;
                    toolStripMenuItem_SetFastest.Checked = false;
                    toolStripMenuItem_SetFast.Checked = true;
                    toolStripMenuItem_SetSlow.Checked = false;
                    toolStripMenuItem_SetSlowest.Checked = false;
                    break;
                case 3:
                    toolStripMenuItem_SetDefault.Checked = false;
                    toolStripMenuItem_SetFastest.Checked = false;
                    toolStripMenuItem_SetFast.Checked = false;
                    toolStripMenuItem_SetSlow.Checked = true;
                    toolStripMenuItem_SetSlowest.Checked = false;
                    break;
                case 4:
                    toolStripMenuItem_SetDefault.Checked = false;
                    toolStripMenuItem_SetFastest.Checked = false;
                    toolStripMenuItem_SetFast.Checked = false;
                    toolStripMenuItem_SetSlow.Checked = false;
                    toolStripMenuItem_SetSlowest.Checked = true;
                    break;
                default:
                    toolStripMenuItem_SetDefault.Checked = true;
                    toolStripMenuItem_SetFastest.Checked = false;
                    toolStripMenuItem_SetFast.Checked = false;
                    toolStripMenuItem_SetSlow.Checked = false;
                    toolStripMenuItem_SetSlowest.Checked = false;
                    break;
            }
        }
    }
}