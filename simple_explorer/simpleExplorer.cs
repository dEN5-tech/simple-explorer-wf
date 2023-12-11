using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace simpleExplorer
{


    public partial class Simple_explorer : Form
    {

        public class FolderListItem
        {
            public string Title { get; set; }
            public Image Icon { get; set; }

            public override string ToString()
            {
                return Title;
            }
        }


        private readonly string mainDirectory = @"C:\"; // Set your main directory here

        public Simple_explorer()
        {
            InitializeComponent();
        }

        // Event handler for browsing directories
        private void BrowseDirectory(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string directoryPath = folderDialog.SelectedPath;
                    pathEntry.Text = directoryPath;
                    ShowDirectoryContents(directoryPath);
                }
            }
        }

        // Event handler for double-clicking on a listbox item
        private void OnListboxDoubleClick(object sender, EventArgs e)
        {
            int selectedIndex = folderListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string selectedDirectory = ((FolderListItem)folderListBox.Items[selectedIndex]).Title;
                string currentPath = pathEntry.Text;
                string newPath = Path.Combine(currentPath, selectedDirectory);
                pathEntry.Text = newPath;
                ShowDirectoryContents(newPath);
            }
        }

        // Method to show the contents of a directory
        private void ShowDirectoryContents(string path)
        {
            folderListBox.Items.Clear(); // Clear existing items in folderListBox

            try
            {
                string[] directories = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);

                // Add directories to the folderListBox with folder icon
                foreach (string directory in directories)
                {
                    string folderName = Path.GetFileName(directory);
                    Image folderIcon = GetIconImage(IconChar.Folder);
                    folderListBox.Items.Add(new FolderListItem { Title = folderName, Icon = folderIcon });
                }

                // Add files to the folderListBox with file icon
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    Image fileIcon = GetIconImage(IconChar.File);
                    folderListBox.Items.Add(new FolderListItem { Title = fileName, Icon = fileIcon });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Event handler for the folder list draw item
        private void OnFolderListDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();

                FolderListItem listItem = (FolderListItem)folderListBox.Items[e.Index];

                // Draw the icon
                e.Graphics.DrawImage(listItem.Icon, e.Bounds.Left, e.Bounds.Top);

                // Draw the title
                e.Graphics.DrawString(listItem.Title, e.Font, Brushes.Black, e.Bounds.Left + listItem.Icon.Width, e.Bounds.Top);

                e.DrawFocusRectangle();
            }
        }

        // Method to get the icon image for a given FontAwesome icon
        private Image GetIconImage(IconChar icon)
        {
            int iconSize = 16; // Set the desired icon size
            Color iconColor = Color.Black; // Set the desired icon color

            // Add padding to the icon if needed
            var iconWithPadding = AddPaddingToIcon(icon, iconSize, iconColor, 0, FlipOrientation.Normal, 2);

            return iconWithPadding;
        }

        // Event handler for the folder list measure item
        private void OnFolderListMeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                FolderListItem listItem = (FolderListItem)folderListBox.Items[e.Index];

                // Set the height based on the icon height
                e.ItemHeight = Math.Max(listItem.Icon.Height, e.ItemHeight);
            }
        }

        // Event handler for the Previous button click
        private void OnPrevButtonClick(object sender, EventArgs e)
        {
            string currentPath = pathEntry.Text;

            // Get the parent directory
            string parentDirectory = Directory.GetParent(currentPath)?.FullName;

            if (!string.IsNullOrEmpty(parentDirectory))
            {
                pathEntry.Text = parentDirectory;
                ShowDirectoryContents(parentDirectory);
            }
        }

        // Event handler for the folder list selection change
        private void OnFolderListSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = folderListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string selectedDirectory = ((FolderListItem)folderListBox.Items[selectedIndex]).Title;
                string currentPath = pathEntry.Text;
                string newPath = Path.Combine(currentPath, selectedDirectory);
                pathEntry.Text = newPath;
                ShowDirectoryContents(newPath);
            }
        }

        // Event handler for the general Browse button click
        private void OnBrowseButtonClick(object sender, EventArgs e)
        {
            BrowseDirectory(sender, e);
        }

        // Event handler for the form load
        private void simple_explorer_Load(object sender, EventArgs e)
        {

            int headerHeight = headerLabel.Height;
            int navButtonHeight = navBarFileButton.Height;

            // Calculate the desired height for folderListBox
            int desiredHeight = headerHeight + navButtonHeight;

            // Subtract the desired height from the total height of the form
            int newHeight = this.Height - desiredHeight;

            // Set the size of folderListBox
            folderListBox.Size = new System.Drawing.Size(headerLabel.Width, newHeight-20);
            this.folderListBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.folderListBox.DrawItem += OnFolderListDrawItem;
            this.folderListBox.MeasureItem += OnFolderListMeasureItem;

            // Set up the icon image for the Home button
            var homeIcon = IconChar.Home;
            var fileIcon = IconChar.Folder;
            var prevIcon = IconChar.LeftLong;

            // Add padding to icons and set them to corresponding buttons
            var homeIconImage = AddPaddingToIcon(homeIcon, 25, Color.White, 0, FlipOrientation.Normal, 10);
            var fileIconImage = AddPaddingToIcon(fileIcon, 25, Color.White, 0, FlipOrientation.Normal, 10);
            var prevIconImage = AddPaddingToIcon(prevIcon, 25, Color.White, 0, FlipOrientation.Normal, 10);

            // Set the icon image for the Home button
            navBarHomeButton.Image = homeIconImage;
            // Set the icon image for the File button
            navBarFileButton.Image = fileIconImage;
            // Set the icon image for the Prev button
            navBarPrevButton.Image = prevIconImage;

            // Set the button text for the File button
            navBarFileButton.Text = "Папка"; // Set the appropriate text here
            // Set the button text for the Prev button
            navBarPrevButton.Text = "Назад"; // Set the appropriate text here

            // Set the main directory when the form loads
            pathEntry.Text = mainDirectory;
            ShowDirectoryContents(mainDirectory);

            // Align icon and text in the buttons
            navBarHomeButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarFileButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarPrevButton.TextImageRelation = TextImageRelation.ImageBeforeText;
        }

        // Method to add padding to an icon
        private Bitmap AddPaddingToIcon(IconChar icon, int size, Color color, double rotation, FlipOrientation flip, int padding)
        {
            // Get the original icon bitmap
            var originalIcon = icon.ToBitmap(IconFont.Auto, size, color, rotation, flip);

            // Create a new bitmap with padding
            var iconWithPadding = new Bitmap(originalIcon.Width + padding, originalIcon.Height);

            using (var graphics = Graphics.FromImage(iconWithPadding))
            {
                // Draw the original icon onto the new bitmap with padding
                graphics.DrawImage(originalIcon, new Rectangle(padding, 0, originalIcon.Width, originalIcon.Height));
            }

            return iconWithPadding;
        }

        private void OnHomeButtonClick(object sender, EventArgs e)
        {
            ShowDirectoryContents(mainDirectory);
        }
    }
}
