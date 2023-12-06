
using System.Windows.Forms;

namespace simple_explorer
{
    partial class simple_explorer
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TextBox pathEntry;
        private Button browseButton;
        private Button prevButton;
        private ListBox listBox;
        private VScrollBar scrollbar;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            // Form settings
            Text = "Directory Viewer";
            Width = 600;
            Height = 400;

            // Entry widget for directory path input
            pathEntry = new TextBox
            {
                Width = 400,
                Location = new System.Drawing.Point(10, 10),
            };
            Controls.Add(pathEntry);

            // Button to browse for a directory
            browseButton = new Button
            {
                Text = "Browse",
                Location = new System.Drawing.Point(420, 10),
            };

            browseButton.Click += BrowseDirectory;
            Controls.Add(browseButton);

            // Button for going to the previous directory
            prevButton = new Button
            {
                Text = "<",
                Location = new System.Drawing.Point(530, 10),
            };
            prevButton.Click += OnPrevButtonClick;
            Controls.Add(prevButton);

            // Listbox to display directory contents
            listBox = new ListBox
            {
                Width = 570,
                Height = 250,
                Location = new System.Drawing.Point(10, 40),
            };
            listBox.DoubleClick += OnListboxDoubleClick;
            Controls.Add(listBox);

            // Scrollbar for the Listbox
            scrollbar = new VScrollBar
            {
                Location = new System.Drawing.Point(580, 40),
                Height = 250,
            };
            scrollbar.Scroll += (sender, e) => listBox.TopIndex = ((VScrollBar)sender).Value;
            Controls.Add(scrollbar);
        }
        #endregion


    }
}

