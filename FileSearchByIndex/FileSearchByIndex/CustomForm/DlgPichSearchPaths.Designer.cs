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
            btnOK.Location = new Point(769, 511);
            btnOK.Margin = new Padding(4, 4, 4, 4);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(96, 31);
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
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(881, 552);
            panel1.TabIndex = 1;
            // 
            // cblPathList
            // 
            cblPathList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cblPathList.CausesValidation = false;
            cblPathList.CheckOnClick = true;
            cblPathList.FormattingEnabled = true;
            cblPathList.HorizontalScrollbar = true;
            cblPathList.Location = new Point(15, 16);
            cblPathList.Margin = new Padding(4, 4, 4, 4);
            cblPathList.Name = "cblPathList";
            cblPathList.Size = new Size(849, 466);
            cblPathList.Sorted = true;
            cblPathList.TabIndex = 1;
            // 
            // DlgPichSearchPaths
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 552);
            Controls.Add(panel1);
            Margin = new Padding(4, 4, 4, 4);
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