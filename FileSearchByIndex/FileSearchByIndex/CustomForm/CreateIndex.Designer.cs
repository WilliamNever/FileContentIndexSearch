namespace FileSearchByIndex.CustomForm
{
    partial class fmCreateIndex
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
            searchSurface = new UserControls.SearchSurface();
            SuspendLayout();
            // 
            // searchSurface
            // 
            searchSurface.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchSurface.Location = new Point(12, 12);
            searchSurface.Name = "searchSurface";
            searchSurface.Size = new Size(776, 130);
            searchSurface.TabIndex = 0;
            // 
            // fmCreateIndex
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 420);
            Controls.Add(searchSurface);
            Name = "fmCreateIndex";
            Text = "CreateIndex";
            ResumeLayout(false);
        }

        #endregion

        private UserControls.SearchSurface searchSurface;
    }
}