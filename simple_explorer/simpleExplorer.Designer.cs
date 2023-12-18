
namespace simpleExplorer
{
    partial class Simple_explorer
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pathEntry = new System.Windows.Forms.TextBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.prevButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.scrollbar = new System.Windows.Forms.VScrollBar();
            this.headerLabel = new System.Windows.Forms.Label();
            this.navBarHomeButton = new System.Windows.Forms.Button();
            this.navBarFileButton = new System.Windows.Forms.Button();
            this.folderListBox = new System.Windows.Forms.ListBox();
            this.twoFolderListBox = new System.Windows.Forms.ListBox();
            this.navBarPrevButton = new System.Windows.Forms.Button();
            this.navBarArchiveButton = new System.Windows.Forms.Button();
            this.navBarUnpackButton = new System.Windows.Forms.Button();
            this.navBarPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.navBarCopyButton = new System.Windows.Forms.Button();
            this.navBarPasteButton = new System.Windows.Forms.Button();
            this.navBarDeleteButton = new System.Windows.Forms.Button();
            this.navBarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.listBox1.ForeColor = System.Drawing.Color.Black;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(12, 50);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(450, 450);
            this.listBox1.TabIndex = 1;
            this.listBox1.DoubleClick += new System.EventHandler(this.OnListboxDoubleClick);
            // 
            // pathEntry
            // 
            this.pathEntry.BackColor = System.Drawing.Color.White;
            this.pathEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pathEntry.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.pathEntry.ForeColor = System.Drawing.Color.Black;
            this.pathEntry.Location = new System.Drawing.Point(12, 12);
            this.pathEntry.Name = "pathEntry";
            this.pathEntry.Size = new System.Drawing.Size(300, 29);
            this.pathEntry.TabIndex = 2;
            // 
            // listBox
            // 
            this.listBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.listBox.ForeColor = System.Drawing.Color.Black;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 17;
            this.listBox.Location = new System.Drawing.Point(12, 50);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(450, 450);
            this.listBox.TabIndex = 3;
            this.listBox.DoubleClick += new System.EventHandler(this.OnListboxDoubleClick);
            // 
            // prevButton
            // 
            this.prevButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.prevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prevButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.prevButton.ForeColor = System.Drawing.Color.White;
            this.prevButton.Location = new System.Drawing.Point(12, 220);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(82, 30);
            this.prevButton.TabIndex = 4;
            this.prevButton.Text = "Previous";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.OnPrevButtonClick);
            // 
            // browseButton
            // 
            this.browseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.browseButton.ForeColor = System.Drawing.Color.White;
            this.browseButton.Location = new System.Drawing.Point(119, 220);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 30);
            this.browseButton.TabIndex = 5;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.OnBrowseButtonClick);
            // 
            // scrollbar
            // 
            this.scrollbar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.scrollbar.Location = new System.Drawing.Point(162, 50);
            this.scrollbar.Name = "scrollbar";
            this.scrollbar.Size = new System.Drawing.Size(17, 150);
            this.scrollbar.TabIndex = 6;
            // 
            // headerLabel
            // 
            this.headerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(0, 10);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(521, 50);
            this.headerLabel.TabIndex = 4;
            this.headerLabel.Text = "Explorer";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // navBarHomeButton
            // 
            this.navBarHomeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarHomeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBarHomeButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.navBarHomeButton.ForeColor = System.Drawing.Color.White;
            this.navBarHomeButton.Location = new System.Drawing.Point(11, 11);
            this.navBarHomeButton.Name = "navBarHomeButton";
            this.navBarHomeButton.Size = new System.Drawing.Size(110, 30);
            this.navBarHomeButton.TabIndex = 3;
            this.navBarHomeButton.Text = "Главная";
            this.navBarHomeButton.UseVisualStyleBackColor = false;
            this.navBarHomeButton.Click += new System.EventHandler(this.OnHomeButtonClick);
            // 
            // navBarFileButton
            // 
            this.navBarFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBarFileButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.navBarFileButton.ForeColor = System.Drawing.Color.White;
            this.navBarFileButton.Location = new System.Drawing.Point(127, 11);
            this.navBarFileButton.Name = "navBarFileButton";
            this.navBarFileButton.Size = new System.Drawing.Size(110, 30);
            this.navBarFileButton.TabIndex = 2;
            this.navBarFileButton.Text = "Папка";
            this.navBarFileButton.UseVisualStyleBackColor = false;
            this.navBarFileButton.Click += new System.EventHandler(this.OnBrowseButtonClick);
            // 
            // folderListBox
            // 
            this.folderListBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.folderListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.folderListBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.folderListBox.ForeColor = System.Drawing.Color.Black;
            this.folderListBox.FormattingEnabled = true;
            this.folderListBox.ItemHeight = 21;
            this.folderListBox.Location = new System.Drawing.Point(0, 97);
            this.folderListBox.Name = "folderListBox";
            this.folderListBox.Size = new System.Drawing.Size(200, 294);
            this.folderListBox.TabIndex = 1;
            this.folderListBox.SelectedIndexChanged += new System.EventHandler(this.OnFolderListSelectedIndexChanged);
            // 
            // twoFolderListBox
            // 
            this.twoFolderListBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.twoFolderListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.twoFolderListBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.twoFolderListBox.ForeColor = System.Drawing.Color.Black;
            this.twoFolderListBox.FormattingEnabled = true;
            this.twoFolderListBox.ItemHeight = 21;
            this.twoFolderListBox.Location = new System.Drawing.Point(250, 97);
            this.twoFolderListBox.Name = "twoFolderListBox";
            this.twoFolderListBox.Size = new System.Drawing.Size(200, 294);
            this.twoFolderListBox.TabIndex = 2;
            this.twoFolderListBox.SelectedIndexChanged += new System.EventHandler(this.OnTwoFolderListSelectedIndexChanged);
            this.twoFolderListBox.DoubleClick += new System.EventHandler(this.OnListboxDoubleClick);
            // 
            // navBarPrevButton
            // 
            this.navBarPrevButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarPrevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBarPrevButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.navBarPrevButton.ForeColor = System.Drawing.Color.White;
            this.navBarPrevButton.Location = new System.Drawing.Point(243, 11);
            this.navBarPrevButton.Name = "navBarPrevButton";
            this.navBarPrevButton.Size = new System.Drawing.Size(110, 30);
            this.navBarPrevButton.TabIndex = 2;
            this.navBarPrevButton.Text = "Назад";
            this.navBarPrevButton.UseVisualStyleBackColor = false;
            this.navBarPrevButton.Click += new System.EventHandler(this.OnPrevButtonClick);
            // 
            // navBarArchiveButton
            // 
            this.navBarArchiveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarArchiveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBarArchiveButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.navBarArchiveButton.ForeColor = System.Drawing.Color.White;
            this.navBarArchiveButton.Location = new System.Drawing.Point(668, 11);
            this.navBarArchiveButton.Name = "navBarArchiveButton";
            this.navBarArchiveButton.Size = new System.Drawing.Size(110, 30);
            this.navBarArchiveButton.TabIndex = 2;
            this.navBarArchiveButton.Text = "Архив";
            this.navBarArchiveButton.UseVisualStyleBackColor = false;
            this.navBarArchiveButton.Click += new System.EventHandler(this.OnNavBarArchiveButtonClick);
            // 
            // navBarUnpackButton
            // 
            this.navBarUnpackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarUnpackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBarUnpackButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.navBarUnpackButton.ForeColor = System.Drawing.Color.White;
            this.navBarUnpackButton.Location = new System.Drawing.Point(784, 11);
            this.navBarUnpackButton.Name = "navBarUnpackButton";
            this.navBarUnpackButton.Size = new System.Drawing.Size(97, 30);
            this.navBarUnpackButton.TabIndex = 2;
            this.navBarUnpackButton.Text = "Архив";
            this.navBarUnpackButton.UseVisualStyleBackColor = false;
            this.navBarUnpackButton.Click += new System.EventHandler(this.OnNavBarUnpackButtonClick);
            // 
            // navBarPanel
            // 
            this.navBarPanel.AutoScroll = true;
            this.navBarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarPanel.Controls.Add(this.navBarHomeButton);
            this.navBarPanel.Controls.Add(this.navBarFileButton);
            this.navBarPanel.Controls.Add(this.navBarPrevButton);
            this.navBarPanel.Controls.Add(this.navBarCopyButton);
            this.navBarPanel.Controls.Add(this.navBarPasteButton);
            this.navBarPanel.Controls.Add(this.navBarDeleteButton);
            this.navBarPanel.Controls.Add(this.navBarArchiveButton);
            this.navBarPanel.Controls.Add(this.navBarUnpackButton);
            this.navBarPanel.Location = new System.Drawing.Point(0, 53);
            this.navBarPanel.Name = "navBarPanel";
            this.navBarPanel.Padding = new System.Windows.Forms.Padding(8);
            this.navBarPanel.Size = new System.Drawing.Size(521, 62);
            this.navBarPanel.TabIndex = 3;
            this.navBarPanel.WrapContents = false;
            // 
            // navBarCopyButton
            // 
            this.navBarCopyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarCopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBarCopyButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.navBarCopyButton.ForeColor = System.Drawing.Color.White;
            this.navBarCopyButton.Location = new System.Drawing.Point(359, 11);
            this.navBarCopyButton.Name = "navBarCopyButton";
            this.navBarCopyButton.Size = new System.Drawing.Size(97, 30);
            this.navBarCopyButton.TabIndex = 2;
            this.navBarCopyButton.Text = "Архив";
            this.navBarCopyButton.UseVisualStyleBackColor = false;
            this.navBarCopyButton.Click += new System.EventHandler(this.OnNavBarCopyButtonClick);
            // 
            // navBarPasteButton
            // 
            this.navBarPasteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarPasteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBarPasteButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.navBarPasteButton.ForeColor = System.Drawing.Color.White;
            this.navBarPasteButton.Location = new System.Drawing.Point(462, 11);
            this.navBarPasteButton.Name = "navBarPasteButton";
            this.navBarPasteButton.Size = new System.Drawing.Size(97, 30);
            this.navBarPasteButton.TabIndex = 2;
            this.navBarPasteButton.Text = "Архив";
            this.navBarPasteButton.UseVisualStyleBackColor = false;
            this.navBarPasteButton.Click += new System.EventHandler(this.OnNavBarPasteButtonClick);
            // 
            // navBarDeleteButton
            // 
            this.navBarDeleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(156)))), ((int)(((byte)(74)))));
            this.navBarDeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navBarDeleteButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.navBarDeleteButton.ForeColor = System.Drawing.Color.White;
            this.navBarDeleteButton.Location = new System.Drawing.Point(565, 11);
            this.navBarDeleteButton.Name = "navBarDeleteButton";
            this.navBarDeleteButton.Size = new System.Drawing.Size(97, 30);
            this.navBarDeleteButton.TabIndex = 2;
            this.navBarDeleteButton.Text = "Delete";
            this.navBarDeleteButton.UseVisualStyleBackColor = false;
            this.navBarDeleteButton.Click += new System.EventHandler(this.navBarDeleteButton_Click);
            // 
            // Simple_explorer
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(520, 270);
            this.Controls.Add(this.folderListBox);
            this.Controls.Add(this.twoFolderListBox);
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.navBarPanel);
            this.Name = "Simple_explorer";
            this.Load += new System.EventHandler(this.simple_explorer_Load);
            this.navBarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox pathEntry;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.VScrollBar scrollbar;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button navBarHomeButton;
        private System.Windows.Forms.Button navBarFileButton;
        private System.Windows.Forms.ListBox folderListBox;
        private System.Windows.Forms.ListBox twoFolderListBox;
        private System.Windows.Forms.Button navBarPrevButton;
        private System.Windows.Forms.Button navBarArchiveButton;
        private System.Windows.Forms.Button navBarUnpackButton;
        private System.Windows.Forms.FlowLayoutPanel navBarPanel;
        private System.Windows.Forms.Button navBarCopyButton;
        private System.Windows.Forms.Button navBarPasteButton;
        private System.Windows.Forms.Button navBarDeleteButton;
    }
}