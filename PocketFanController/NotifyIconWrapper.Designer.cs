namespace PocketFanController
{
    partial class NotifyIconWrapper
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_OpenWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SetFastest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_SetFast = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SetSlow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SetSlowest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SetDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Pocket Fan Controller";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_OpenWindow,
            this.toolStripSeparator1,
            this.toolStripMenuItem_SetDefault,
            this.toolStripMenuItem_SetFastest,
            this.toolStripMenuItem_SetFast,
            this.toolStripMenuItem_SetSlow,
            this.toolStripMenuItem_SetSlowest,
            this.toolStripSeparator2,
            this.toolStripMenuItem_Exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 184);
            // 
            // toolStripMenuItem_OpenWindow
            // 
            this.toolStripMenuItem_OpenWindow.Name = "toolStripMenuItem_OpenWindow";
            this.toolStripMenuItem_OpenWindow.Size = new System.Drawing.Size(173, 24);
            this.toolStripMenuItem_OpenWindow.Text = "Open Window";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // toolStripMenuItem_Exit
            // 
            this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            this.toolStripMenuItem_Exit.Size = new System.Drawing.Size(173, 24);
            this.toolStripMenuItem_Exit.Text = "Exit";
            // 
            // toolStripMenuItem_SetFastest
            // 
            this.toolStripMenuItem_SetFastest.Name = "toolStripMenuItem_SetFastest";
            this.toolStripMenuItem_SetFastest.Size = new System.Drawing.Size(173, 24);
            this.toolStripMenuItem_SetFastest.Text = "Fastest";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // toolStripMenuItem_SetFast
            // 
            this.toolStripMenuItem_SetFast.Name = "toolStripMenuItem_SetFast";
            this.toolStripMenuItem_SetFast.Size = new System.Drawing.Size(173, 24);
            this.toolStripMenuItem_SetFast.Text = "Fast";
            // 
            // toolStripMenuItem_SetSlow
            // 
            this.toolStripMenuItem_SetSlow.Name = "toolStripMenuItem_SetSlow";
            this.toolStripMenuItem_SetSlow.Size = new System.Drawing.Size(173, 24);
            this.toolStripMenuItem_SetSlow.Text = "Slow";
            // 
            // toolStripMenuItem_SetSlowest
            // 
            this.toolStripMenuItem_SetSlowest.Name = "toolStripMenuItem_SetSlowest";
            this.toolStripMenuItem_SetSlowest.Size = new System.Drawing.Size(173, 24);
            this.toolStripMenuItem_SetSlowest.Text = "Slowest";
            // 
            // toolStripMenuItem_SetDefault
            // 
            this.toolStripMenuItem_SetDefault.Name = "toolStripMenuItem_SetDefault";
            this.toolStripMenuItem_SetDefault.Size = new System.Drawing.Size(173, 24);
            this.toolStripMenuItem_SetDefault.Text = "Auto(Default)";
            this.contextMenuStrip1.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_OpenWindow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SetDefault;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SetFastest;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SetFast;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SetSlow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SetSlowest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
