namespace FileSearchByIndex
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mnMainMenu = new UserControls.MainMenu();
            searchMainInterFace = new UserControls.SearchMainControl();
            SuspendLayout();
            // 
            // mnMainMenu
            // 
            mnMainMenu.Dock = DockStyle.Top;
            mnMainMenu.Location = new Point(0, 0);
            mnMainMenu.Name = "mnMainMenu";
            mnMainMenu.Size = new Size(828, 26);
            mnMainMenu.TabIndex = 0;
            // 
            // searchMainInterFace
            // 
            searchMainInterFace.Dock = DockStyle.Fill;
            searchMainInterFace.Location = new Point(0, 26);
            searchMainInterFace.Name = "searchMainInterFace";
            searchMainInterFace.Size = new Size(828, 384);
            searchMainInterFace.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(828, 410);
            Controls.Add(searchMainInterFace);
            Controls.Add(mnMainMenu);
            Name = "MainForm";
            Text = "Main Form";
            ResumeLayout(false);
        }

        #endregion

        private UserControls.MainMenu mnMainMenu;
        private UserControls.SearchMainControl searchMainInterFace;
    }
}