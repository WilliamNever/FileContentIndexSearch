namespace FileSearchByIndex.UserControls
{
    partial class MainMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrips = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            indexesToolStripMenuItem = new ToolStripMenuItem();
            createIndexesToolStripMenuItem = new ToolStripMenuItem();
            menuStrips.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrips
            // 
            menuStrips.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, indexesToolStripMenuItem });
            menuStrips.Location = new Point(0, 0);
            menuStrips.Name = "menuStrips";
            menuStrips.Size = new Size(583, 24);
            menuStrips.TabIndex = 0;
            menuStrips.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, saveAsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            fileToolStripMenuItem.DropDownItemClicked += FileToolStripMenuItem_DropDownItemClicked;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.AccessibleName = "Save";
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(180, 22);
            saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.AccessibleName = "SaveAs";
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(180, 22);
            saveAsToolStripMenuItem.Text = "Save As ...";
            // 
            // indexesToolStripMenuItem
            // 
            indexesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createIndexesToolStripMenuItem });
            indexesToolStripMenuItem.Name = "indexesToolStripMenuItem";
            indexesToolStripMenuItem.Size = new Size(59, 20);
            indexesToolStripMenuItem.Text = "Indexes";
            indexesToolStripMenuItem.DropDownItemClicked += indexesToolStripMenuItem_DropDownItemClicked;
            // 
            // createIndexesToolStripMenuItem
            // 
            createIndexesToolStripMenuItem.AccessibleName = "CreateIndexes";
            createIndexesToolStripMenuItem.Name = "createIndexesToolStripMenuItem";
            createIndexesToolStripMenuItem.Size = new Size(180, 22);
            createIndexesToolStripMenuItem.Text = "Create Indexes";
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuStrips);
            Name = "MainMenu";
            Size = new Size(583, 26);
            menuStrips.ResumeLayout(false);
            menuStrips.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrips;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem indexesToolStripMenuItem;
        private ToolStripMenuItem createIndexesToolStripMenuItem;
    }
}
