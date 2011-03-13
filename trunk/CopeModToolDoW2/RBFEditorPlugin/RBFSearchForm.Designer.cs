namespace RBFPlugin
{
    partial class RBFSearchForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.m_progBarSearch = new System.Windows.Forms.ProgressBar();
            this._tbx_initialNode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnSearch = new System.Windows.Forms.Button();
            this.m_tbxSearchText = new System.Windows.Forms.TextBox();
            this._rab_searchValue = new System.Windows.Forms.RadioButton();
            this.m_radbtnSearchKey = new System.Windows.Forms.RadioButton();
            this.m_lbxSearchResults = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_rtbResultDisplay = new System.Windows.Forms.RichTextBox();
            this.m_cbxFullText = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_rtbResultDisplay);
            this.splitContainer1.Size = new System.Drawing.Size(635, 316);
            this.splitContainer1.SplitterDistance = 426;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 423F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.93989F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_lbxSearchResults, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(426, 316);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cbxFullText);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.m_progBarSearch);
            this.panel1.Controls.Add(this._tbx_initialNode);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.m_btnCancel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.m_btnSearch);
            this.panel1.Controls.Add(this.m_tbxSearchText);
            this.panel1.Controls.Add(this._rab_searchValue);
            this.panel1.Controls.Add(this.m_radbtnSearchKey);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 119);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Progress:";
            // 
            // _progBar_search
            // 
            this.m_progBarSearch.Location = new System.Drawing.Point(92, 91);
            this.m_progBarSearch.Name = "m_progBarSearch";
            this.m_progBarSearch.Size = new System.Drawing.Size(321, 23);
            this.m_progBarSearch.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_progBarSearch.TabIndex = 8;
            // 
            // _tbx_initialNode
            // 
            this._tbx_initialNode.Location = new System.Drawing.Point(92, 39);
            this._tbx_initialNode.Name = "_tbx_initialNode";
            this._tbx_initialNode.Size = new System.Drawing.Size(321, 20);
            this._tbx_initialNode.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "Relative Path\r\nof Start-Node:";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(338, 65);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 5;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Search For:";
            // 
            // m_btnSearch
            // 
            this.m_btnSearch.Location = new System.Drawing.Point(257, 65);
            this.m_btnSearch.Name = "m_btnSearch";
            this.m_btnSearch.Size = new System.Drawing.Size(75, 23);
            this.m_btnSearch.TabIndex = 3;
            this.m_btnSearch.Text = "Search";
            this.m_btnSearch.UseVisualStyleBackColor = true;
            this.m_btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // _tbx_search
            // 
            this.m_tbxSearchText.Location = new System.Drawing.Point(92, 12);
            this.m_tbxSearchText.Name = "m_tbxSearchText";
            this.m_tbxSearchText.Size = new System.Drawing.Size(321, 20);
            this.m_tbxSearchText.TabIndex = 2;
            // 
            // _rab_searchValue
            // 
            this._rab_searchValue.AutoSize = true;
            this._rab_searchValue.Location = new System.Drawing.Point(99, 68);
            this._rab_searchValue.Name = "_rab_searchValue";
            this._rab_searchValue.Size = new System.Drawing.Size(52, 17);
            this._rab_searchValue.TabIndex = 1;
            this._rab_searchValue.TabStop = true;
            this._rab_searchValue.Text = "Value";
            this._rab_searchValue.UseVisualStyleBackColor = true;
            // 
            // _rab_searchKey
            // 
            this.m_radbtnSearchKey.AutoSize = true;
            this.m_radbtnSearchKey.Location = new System.Drawing.Point(50, 68);
            this.m_radbtnSearchKey.Name = "m_radbtnSearchKey";
            this.m_radbtnSearchKey.Size = new System.Drawing.Size(43, 17);
            this.m_radbtnSearchKey.TabIndex = 0;
            this.m_radbtnSearchKey.TabStop = true;
            this.m_radbtnSearchKey.Text = "Key";
            this.m_radbtnSearchKey.UseVisualStyleBackColor = true;
            // 
            // m_lbxSearchResults
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.m_lbxSearchResults, 2);
            this.m_lbxSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbxSearchResults.FormattingEnabled = true;
            this.m_lbxSearchResults.Location = new System.Drawing.Point(3, 146);
            this.m_lbxSearchResults.Name = "m_lbxSearchResults";
            this.m_lbxSearchResults.Size = new System.Drawing.Size(420, 167);
            this.m_lbxSearchResults.Sorted = true;
            this.m_lbxSearchResults.TabIndex = 1;
            this.m_lbxSearchResults.SelectedIndexChanged += new System.EventHandler(this.LbxSearchResultsSelectedIndexChanged);
            this.m_lbxSearchResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LbxSearchResultsMouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 119);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(423, 24);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Results:";
            // 
            // m_rtbResultDisplay
            // 
            this.m_rtbResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbResultDisplay.Location = new System.Drawing.Point(0, 0);
            this.m_rtbResultDisplay.Name = "m_rtbResultDisplay";
            this.m_rtbResultDisplay.Size = new System.Drawing.Size(205, 316);
            this.m_rtbResultDisplay.TabIndex = 0;
            this.m_rtbResultDisplay.Text = "";
            // 
            // m_cbxFullText
            // 
            this.m_cbxFullText.AutoSize = true;
            this.m_cbxFullText.Location = new System.Drawing.Point(157, 69);
            this.m_cbxFullText.Name = "m_cbxFullText";
            this.m_cbxFullText.Size = new System.Drawing.Size(97, 17);
            this.m_cbxFullText.TabIndex = 10;
            this.m_cbxFullText.Text = "Full-text search";
            this.m_cbxFullText.UseVisualStyleBackColor = true;
            // 
            // RBFSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 316);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(651, 354);
            this.Name = "RBFSearchForm";
            this.ShowIcon = false;
            this.Text = "RBF Search";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar m_progBarSearch;
        private System.Windows.Forms.TextBox _tbx_initialNode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_btnSearch;
        private System.Windows.Forms.TextBox m_tbxSearchText;
        private System.Windows.Forms.RadioButton _rab_searchValue;
        private System.Windows.Forms.RadioButton m_radbtnSearchKey;
        private System.Windows.Forms.ListBox m_lbxSearchResults;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox m_rtbResultDisplay;
        private System.Windows.Forms.CheckBox m_cbxFullText;
    }
}