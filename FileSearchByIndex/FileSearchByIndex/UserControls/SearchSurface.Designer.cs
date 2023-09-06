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
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 0;
            label1.Text = "Filter(*.*):";
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilter.Location = new Point(111, 5);
            txtFilter.Margin = new Padding(4);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(377, 27);
            txtFilter.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 85);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(98, 20);
            label2.TabIndex = 2;
            label2.Text = "Search Path:";
            // 
            // cbkIncludeSub
            // 
            cbkIncludeSub.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbkIncludeSub.AutoSize = true;
            cbkIncludeSub.Checked = true;
            cbkIncludeSub.CheckState = CheckState.Checked;
            cbkIncludeSub.Location = new Point(648, 7);
            cbkIncludeSub.Margin = new Padding(4);
            cbkIncludeSub.Name = "cbkIncludeSub";
            cbkIncludeSub.Size = new Size(172, 24);
            cbkIncludeSub.TabIndex = 3;
            cbkIncludeSub.Text = "Include sub Folders";
            cbkIncludeSub.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPath.Location = new Point(111, 79);
            txtPath.Margin = new Padding(4);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(604, 27);
            txtPath.TabIndex = 4;
            // 
            // btnBrowsPath
            // 
            btnBrowsPath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowsPath.Location = new Point(723, 79);
            btnBrowsPath.Margin = new Padding(4);
            btnBrowsPath.Name = "btnBrowsPath";
            btnBrowsPath.Size = new Size(96, 31);
            btnBrowsPath.TabIndex = 5;
            btnBrowsPath.Text = "Browse";
            btnBrowsPath.UseVisualStyleBackColor = true;
            btnBrowsPath.Click += btnBrowsPath_Click;
            // 
            // btnCreateIndex
            // 
            btnCreateIndex.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreateIndex.Location = new Point(700, 123);
            btnCreateIndex.Margin = new Padding(4);
            btnCreateIndex.Name = "btnCreateIndex";
            btnCreateIndex.Size = new Size(120, 31);
            btnCreateIndex.TabIndex = 6;
            btnCreateIndex.Text = "Create Index";
            btnCreateIndex.UseVisualStyleBackColor = true;
            btnCreateIndex.Click += btnCreateIndex_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(391, 47);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(97, 20);
            label3.TabIndex = 7;
            label3.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtDescription.Location = new Point(493, 42);
            txtDescription.Margin = new Padding(4);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(325, 27);
            txtDescription.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 47);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 20);
            label4.TabIndex = 9;
            label4.Text = "Index Name:";
            // 
            // txtIndexFileName
            // 
            txtIndexFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtIndexFileName.Location = new Point(111, 42);
            txtIndexFileName.Margin = new Padding(4);
            txtIndexFileName.Name = "txtIndexFileName";
            txtIndexFileName.Size = new Size(273, 27);
            txtIndexFileName.TabIndex = 10;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(497, 10);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(138, 20);
            label5.TabIndex = 11;
            label5.Text = "Sample - *.txt|*.cs";
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtIndexFileName);
            panel1.Controls.Add(txtFilter);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtDescription);
            panel1.Controls.Add(cbkIncludeSub);
            panel1.Controls.Add(txtPath);
            panel1.Controls.Add(btnCreateIndex);
            panel1.Controls.Add(btnBrowsPath);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(832, 163);
            panel1.TabIndex = 12;
            // 
            // SearchSurface
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "SearchSurface";
            Size = new Size(832, 163);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
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
        private Panel panel1;
    }
}
