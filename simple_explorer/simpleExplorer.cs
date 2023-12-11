using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace simpleExplorer
{


    public partial class Simple_explorer : Form
    {
        private bool ctrlKeyPressed = false;

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
                if (IsZipArchive(path))
                {
                    AddFilesFromArchive(path);
                }
                else
                {
                    // Continue with the logic for regular directories
                    // Check if the directory exists
                    if (!Directory.Exists(path))
                    {
                        MessageBox.Show("Invalid directory path or insufficient permissions.");
                        return;
                    }

                    string[] directories = Directory.GetDirectories(path);
                    string[] files = Directory.GetFiles(path);

                    // Add parent directory (if not root)
                    if (path != mainDirectory)
                    {
                        string parentFolderName = Path.GetFileName(Path.GetDirectoryName(path));
                        Image folderIcon = GetIconImage(IconChar.Folder);
                        folderListBox.Items.Add(new FolderListItem { Title = ".." + parentFolderName, Icon = folderIcon });
                    }

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
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Insufficient permissions to access the directory or archive.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private bool IsZipArchive(string path)
        {
            // Check if the file has a .zip extension
            return string.Equals(Path.GetExtension(path), ".zip", StringComparison.OrdinalIgnoreCase);
        }


        // Method to add files from an archive to the folderListBox
        private void AddFilesFromArchive(string archivePath)
        {
            try
            {
                using (var archive = ZipFile.OpenRead(archivePath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        // Combine the entry name with the original path to preserve folder structure
                        string entryName = Path.Combine(Path.GetFileNameWithoutExtension(archivePath), entry.FullName);

                        // Ensure that the entry name is correctly formatted for the current platform
                        entryName = entryName.Replace(Path.DirectorySeparatorChar, Path.PathSeparator);

                        Image entryIcon = GetIconImage(IconChar.File); // You may customize the icon

                        folderListBox.Items.Add(new FolderListItem { Title = entryName, Icon = entryIcon });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading archive: {ex.Message}");
            }
        }

        // Method to check if the file extension corresponds to a known archive extension
        private bool IsArchiveExtension(string extension)
        {
            // Add more archive extensions if needed
            string[] archiveExtensions = { ".zip", ".rar", ".7z" };

            return archiveExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
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
            if (ctrlKeyPressed)
            {
                // If Ctrl key is pressed, handle multiple item selection
                // You may want to customize this behavior based on your requirements
                folderListBox.SelectionMode = SelectionMode.MultiExtended;
            }
            else
            {
                // If Ctrl key is not pressed, handle single item selection
                folderListBox.SelectionMode = SelectionMode.One;
            }

            int selectedIndex = folderListBox.SelectedIndex;
            if (selectedIndex >= 0 && !ctrlKeyPressed)
            {
                string selectedDirectory = ((FolderListItem)folderListBox.Items[selectedIndex]).Title;
                string currentPath = pathEntry.Text;
                string newPath = Path.Combine(currentPath, selectedDirectory);
                pathEntry.Text = newPath;
                ShowDirectoryContents(newPath);
            }
        }

        // Event handler for the folder list key down
        private void OnFolderListKeyDown(object sender, KeyEventArgs e)
        {
            // Check if Ctrl key is pressed
            ctrlKeyPressed = e.Control;
        }

        // Event handler for the folder list key up
        private void OnFolderListKeyUp(object sender, KeyEventArgs e)
        {
            // Reset the Ctrl key state
            ctrlKeyPressed = false;
        }

        // Event handler for the NavBarArchiveButton click
        private void OnNavBarArchiveButtonClick(object sender, EventArgs e)
        {
            // Call your archive files logic here
            // You can use folderListBox.SelectedItems to get the selected items
            if (folderListBox.SelectedItems.Count > 0)
            {
                // Prompt the user to select a destination for the zip file
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Zip files (*.zip)|*.zip";
                    saveFileDialog.Title = "Сохранить архив";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Create a zip archive
                        using (var zipArchive = ZipFile.Open(saveFileDialog.FileName, ZipArchiveMode.Create))
                        {
                            foreach (var selectedItem in folderListBox.SelectedItems)
                            {
                                // Process each selected item
                                // For example, you can get the title and add it to the zip archive
                                string selectedTitle = ((FolderListItem)selectedItem).Title;
                                string fullPath = Path.Combine(pathEntry.Text, selectedTitle);

                                if (File.Exists(fullPath))
                                {
                                    // Add the file to the archive
                                    zipArchive.CreateEntryFromFile(fullPath, selectedTitle);
                                }
                                else if (Directory.Exists(fullPath))
                                {
                                    {
                                        foreach (var file in Directory.GetFiles(fullPath, "*", SearchOption.AllDirectories))
                                        {
                                            // Calculate the relative path inside the archive
                                            string relativePath = file.Substring(pathEntry.Text.Length + 1);
                                            zipArchive.CreateEntryFromFile(file, relativePath);
                                        }
                                    }
                                }
                            }

                            MessageBox.Show("Архивация успешно произведена!");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Нет выбранных файлов для архивации.");
                    }
                }
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

            folderListBox.KeyDown += OnFolderListKeyDown;
            folderListBox.KeyUp += OnFolderListKeyUp;

            // Set up the icon image for the Home button
            var homeIcon = IconChar.Home;
            var fileIcon = IconChar.Folder;
            var prevIcon = IconChar.LeftLong;
            var archIcon = IconChar.FileZipper;

            // Add padding to icons and set them to corresponding buttons
            var homeIconImage = AddPaddingToIcon(homeIcon, 15, Color.White, 0, FlipOrientation.Normal, 10);
            var fileIconImage = AddPaddingToIcon(fileIcon, 15, Color.White, 0, FlipOrientation.Normal, 10);
            var prevIconImage = AddPaddingToIcon(prevIcon, 15, Color.White, 0, FlipOrientation.Normal, 10);
            var archiveIconImage = AddPaddingToIcon(archIcon, 15, Color.White, 0, FlipOrientation.Normal, 10);

            // Set the icon image for the Home button
            navBarHomeButton.Image = homeIconImage;
            // Set the icon image for the File button
            navBarFileButton.Image = fileIconImage;
            // Set the icon image for the Prev button
            navBarPrevButton.Image = prevIconImage;

            navBarArchiveButton.Image = archiveIconImage;

            // Set the button text for the File button
            navBarFileButton.Text = "Папка"; // Set the appropriate text here
            // Set the button text for the Prev button
            navBarPrevButton.Text = "Назад"; // Set the appropriate text here

            navBarArchiveButton.Text = "Архив";

            // Set the main directory when the form loads
            pathEntry.Text = mainDirectory;
            ShowDirectoryContents(mainDirectory);

            // Align icon and text in the buttons
            navBarHomeButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarFileButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarPrevButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarArchiveButton.TextImageRelation = TextImageRelation.ImageBeforeText;
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
