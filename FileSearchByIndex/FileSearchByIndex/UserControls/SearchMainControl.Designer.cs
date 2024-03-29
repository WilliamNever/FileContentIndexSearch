﻿namespace FileSearchByIndex.UserControls
{
    partial class SearchMainControl
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
            pnlMain = new Panel();
            chbIsSearchingInSampleTxt = new CheckBox();
            btnSearch = new Button();
            ListSearchingPath = new ListBox();
            txtFileFilter = new TextBox();
            lblFileFilter = new Label();
            txtKeywords = new TextBox();
            lblKeyword = new Label();
            btnPickPaths = new Button();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(chbIsSearchingInSampleTxt);
            pnlMain.Controls.Add(btnSearch);
            pnlMain.Controls.Add(ListSearchingPath);
            pnlMain.Controls.Add(txtFileFilter);
            pnlMain.Controls.Add(lblFileFilter);
            pnlMain.Controls.Add(txtKeywords);
            pnlMain.Controls.Add(lblKeyword);
            pnlMain.Controls.Add(btnPickPaths);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Margin = new Padding(4);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(993, 508);
            pnlMain.TabIndex = 2;
            // 
            // chbIsSearchingInSampleTxt
            // 
            chbIsSearchingInSampleTxt.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chbIsSearchingInSampleTxt.AutoSize = true;
            chbIsSearchingInSampleTxt.Location = new Point(4, 475);
            chbIsSearchingInSampleTxt.Margin = new Padding(4);
            chbIsSearchingInSampleTxt.Name = "chbIsSearchingInSampleTxt";
            chbIsSearchingInSampleTxt.Size = new Size(229, 24);
            chbIsSearchingInSampleTxt.TabIndex = 7;
            chbIsSearchingInSampleTxt.Text = "Is Searching in Sample Text";
            chbIsSearchingInSampleTxt.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSearch.Location = new Point(891, 472);
            btnSearch.Margin = new Padding(4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(96, 31);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // ListSearchingPath
            // 
            ListSearchingPath.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ListSearchingPath.CausesValidation = false;
            ListSearchingPath.FormattingEnabled = true;
            ListSearchingPath.HorizontalScrollbar = true;
            ListSearchingPath.ItemHeight = 20;
            ListSearchingPath.Location = new Point(4, 79);
            ListSearchingPath.Margin = new Padding(4);
            ListSearchingPath.Name = "ListSearchingPath";
            ListSearchingPath.SelectionMode = SelectionMode.MultiSimple;
            ListSearchingPath.Size = new Size(982, 384);
            ListSearchingPath.Sorted = true;
            ListSearchingPath.TabIndex = 1;
            // 
            // txtFileFilter
            // 
            txtFileFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFileFilter.Location = new Point(671, 40);
            txtFileFilter.Margin = new Padding(4);
            txtFileFilter.Name = "txtFileFilter";
            txtFileFilter.Size = new Size(315, 27);
            txtFileFilter.TabIndex = 5;
            // 
            // lblFileFilter
            // 
            lblFileFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblFileFilter.AutoSize = true;
            lblFileFilter.Location = new Point(671, 11);
            lblFileFilter.Margin = new Padding(4, 0, 4, 0);
            lblFileFilter.Name = "lblFileFilter";
            lblFileFilter.Size = new Size(209, 20);
            lblFileFilter.TabIndex = 4;
            lblFileFilter.Text = "FileFilter - Sample as .txt|.cs";
            // 
            // txtKeywords
            // 
            txtKeywords.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtKeywords.Location = new Point(4, 40);
            txtKeywords.Margin = new Padding(4);
            txtKeywords.Name = "txtKeywords";
            txtKeywords.Size = new Size(651, 27);
            txtKeywords.TabIndex = 3;
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Location = new Point(4, 11);
            lblKeyword.Margin = new Padding(4, 0, 4, 0);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(95, 20);
            lblKeyword.TabIndex = 2;
            lblKeyword.Text = "Keywords - ";
            // 
            // btnPickPaths
            // 
            btnPickPaths.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPickPaths.Location = new Point(783, 472);
            btnPickPaths.Margin = new Padding(4);
            btnPickPaths.Name = "btnPickPaths";
            btnPickPaths.Size = new Size(96, 31);
            btnPickPaths.TabIndex = 0;
            btnPickPaths.Text = "Pick Paths";
            btnPickPaths.UseVisualStyleBackColor = true;
            btnPickPaths.Click += btnPickPaths_Click;
            // 
            // SearchMainControl
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMain);
            Margin = new Padding(4);
            Name = "SearchMainControl";
            Size = new Size(993, 508);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Button btnSearch;
        private TextBox txtFileFilter;
        private Label lblFileFilter;
        private TextBox txtKeywords;
        private Label lblKeyword;
        private ListBox ListSearchingPath;
        private Button btnPickPaths;
        private CheckBox chbIsSearchingInSampleTxt;
    }
}
