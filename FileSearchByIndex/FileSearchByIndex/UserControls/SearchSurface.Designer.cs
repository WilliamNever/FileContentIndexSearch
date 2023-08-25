namespace FileSearchByIndex.UserControls
{
    partial class SearchSurface
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
            label1 = new Label();
            txtFilter = new TextBox();
            label2 = new Label();
            cbkIncludeSub = new CheckBox();
            txtPath = new TextBox();
            btnBrowsPath = new Button();
            btnCreateIndex = new Button();
            label3 = new Label();
            txtDescription = new TextBox();
            label4 = new Label();
            txtIndexFileName = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 15);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 0;
            label1.Text = "Filter(*.*):";
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilter.Location = new Point(81, 11);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(294, 23);
            txtFilter.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 71);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 2;
            label2.Text = "Search Path:";
            // 
            // cbkIncludeSub
            // 
            cbkIncludeSub.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbkIncludeSub.AutoSize = true;
            cbkIncludeSub.Checked = true;
            cbkIncludeSub.CheckState = CheckState.Checked;
            cbkIncludeSub.Location = new Point(490, 13);
            cbkIncludeSub.Name = "cbkIncludeSub";
            cbkIncludeSub.Size = new Size(128, 19);
            cbkIncludeSub.TabIndex = 3;
            cbkIncludeSub.Text = "Include sub Folders";
            cbkIncludeSub.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPath.Location = new Point(81, 67);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(456, 23);
            txtPath.TabIndex = 4;
            // 
            // btnBrowsPath
            // 
            btnBrowsPath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowsPath.Location = new Point(543, 67);
            btnBrowsPath.Name = "btnBrowsPath";
            btnBrowsPath.Size = new Size(75, 23);
            btnBrowsPath.TabIndex = 5;
            btnBrowsPath.Text = "Browse";
            btnBrowsPath.UseVisualStyleBackColor = true;
            btnBrowsPath.Click += btnBrowsPath_Click;
            // 
            // btnCreateIndex
            // 
            btnCreateIndex.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreateIndex.Location = new Point(525, 100);
            btnCreateIndex.Name = "btnCreateIndex";
            btnCreateIndex.Size = new Size(93, 23);
            btnCreateIndex.TabIndex = 6;
            btnCreateIndex.Text = "Create Index";
            btnCreateIndex.UseVisualStyleBackColor = true;
            btnCreateIndex.Click += btnCreateIndex_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(239, 43);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 7;
            label3.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(315, 39);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(303, 23);
            txtDescription.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 43);
            label4.Name = "label4";
            label4.Size = new Size(74, 15);
            label4.TabIndex = 9;
            label4.Text = "Index Name:";
            // 
            // txtIndexFileName
            // 
            txtIndexFileName.Location = new Point(81, 39);
            txtIndexFileName.Name = "txtIndexFileName";
            txtIndexFileName.Size = new Size(152, 23);
            txtIndexFileName.TabIndex = 10;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(381, 15);
            label5.Name = "label5";
            label5.Size = new Size(87, 15);
            label5.TabIndex = 11;
            label5.Text = "Separated by '|'";
            // 
            // SearchSurface
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label5);
            Controls.Add(txtIndexFileName);
            Controls.Add(label4);
            Controls.Add(txtDescription);
            Controls.Add(label3);
            Controls.Add(btnCreateIndex);
            Controls.Add(btnBrowsPath);
            Controls.Add(txtPath);
            Controls.Add(cbkIncludeSub);
            Controls.Add(label2);
            Controls.Add(txtFilter);
            Controls.Add(label1);
            Name = "SearchSurface";
            Size = new Size(621, 126);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFilter;
        private Label label2;
        private CheckBox cbkIncludeSub;
        private TextBox txtPath;
        private Button btnBrowsPath;
        private Button btnCreateIndex;
        private Label label3;
        private TextBox txtDescription;
        private Label label4;
        private TextBox txtIndexFileName;
        private Label label5;
    }
}
