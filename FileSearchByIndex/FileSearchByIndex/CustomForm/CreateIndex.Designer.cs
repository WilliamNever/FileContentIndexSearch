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
            btnCancel = new Button();
            txtInfo = new TextBox();
            pnlContainer = new Panel();
            btnClear = new Button();
            pnlContainer.SuspendLayout();
            SuspendLayout();
            // 
            // searchSurface
            // 
            searchSurface.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchSurface.Location = new Point(3, 3);
            searchSurface.Name = "searchSurface";
            searchSurface.Size = new Size(770, 127);
            searchSurface.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(3, 107);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtInfo
            // 
            txtInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtInfo.Location = new Point(12, 148);
            txtInfo.Multiline = true;
            txtInfo.Name = "txtInfo";
            txtInfo.ScrollBars = ScrollBars.Vertical;
            txtInfo.Size = new Size(776, 300);
            txtInfo.TabIndex = 2;
            // 
            // pnlContainer
            // 
            pnlContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlContainer.BackColor = Color.Transparent;
            pnlContainer.Controls.Add(btnClear);
            pnlContainer.Controls.Add(btnCancel);
            pnlContainer.Controls.Add(searchSurface);
            pnlContainer.Location = new Point(12, 12);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(776, 130);
            pnlContainer.TabIndex = 3;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(84, 107);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 4;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // fmCreateIndex
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 460);
            Controls.Add(txtInfo);
            Controls.Add(pnlContainer);
            Name = "fmCreateIndex";
            Text = "CreateIndex";
            pnlContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UserControls.SearchSurface searchSurface;
        private Button btnCancel;
        private TextBox txtInfo;
        private Panel pnlContainer;
        private Button btnClear;
    }
}