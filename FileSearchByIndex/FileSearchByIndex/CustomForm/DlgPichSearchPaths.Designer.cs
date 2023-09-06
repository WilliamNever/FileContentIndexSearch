namespace FileSearchByIndex.CustomForm
{
    partial class DlgPichSearchPaths
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
            btnOK = new Button();
            panel1 = new Panel();
            cblPathList = new CheckedListBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(598, 383);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 0;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(cblPathList);
            panel1.Controls.Add(btnOK);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(685, 414);
            panel1.TabIndex = 1;
            // 
            // cblPathList
            // 
            cblPathList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cblPathList.CausesValidation = false;
            cblPathList.CheckOnClick = true;
            cblPathList.FormattingEnabled = true;
            cblPathList.HorizontalScrollbar = true;
            cblPathList.Location = new Point(12, 12);
            cblPathList.Name = "cblPathList";
            cblPathList.Size = new Size(661, 364);
            cblPathList.Sorted = true;
            cblPathList.TabIndex = 1;
            // 
            // DlgPichSearchPaths
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(685, 414);
            Controls.Add(panel1);
            Name = "DlgPichSearchPaths";
            Text = "DlgPichSearchPaths";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnOK;
        private Panel panel1;
        private CheckedListBox cblPathList;
    }
}