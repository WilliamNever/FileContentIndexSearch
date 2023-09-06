namespace FileSearchByIndex.CustomForm
{
    partial class SearchResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMain = new Panel();
            tvResults = new TreeView();
            menuMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exportResultsToolStripMenuItem = new ToolStripMenuItem();
            pnlMain.SuspendLayout();
            menuMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(tvResults);
            pnlMain.Controls.Add(menuMain);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(800, 450);
            pnlMain.TabIndex = 0;
            // 
            // tvResults
            // 
            tvResults.Dock = DockStyle.Fill;
            tvResults.Location = new Point(0, 24);
            tvResults.Name = "tvResults";
            tvResults.Size = new Size(800, 426);
            tvResults.TabIndex = 3;
            // 
            // menuMain
            // 
            menuMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuMain.Location = new Point(0, 0);
            menuMain.Name = "menuMain";
            menuMain.Size = new Size(800, 24);
            menuMain.TabIndex = 4;
            menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportResultsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            fileToolStripMenuItem.DropDownItemClicked += fileToolStripMenuItem_DropDownItemClicked;
            // 
            // exportResultsToolStripMenuItem
            // 
            exportResultsToolStripMenuItem.AccessibleName = "ExportResults";
            exportResultsToolStripMenuItem.Name = "exportResultsToolStripMenuItem";
            exportResultsToolStripMenuItem.Size = new Size(180, 22);
            exportResultsToolStripMenuItem.Text = "Export Results";
            // 
            // SearchResultForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlMain);
            MainMenuStrip = menuMain;
            Name = "SearchResultForm";
            Text = "SearchResultForm";
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            menuMain.ResumeLayout(false);
            menuMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private TreeView tvResults;
        private MenuStrip menuMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exportResultsToolStripMenuItem;
    }
}