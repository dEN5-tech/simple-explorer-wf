using FontAwesome.Sharp;
using System;
using System.Collections.Specialized;
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

        private ListView lastSelectedListView;

        private StringCollection clipboardPaths;



        private readonly string mainDirectory = @"C:\"; // Set your main directory here

        public Simple_explorer()
        {
            InitializeComponent();
            this.Resize += Simple_explorer_Resize;

            clipboardPaths = new StringCollection();
        }


        private void Simple_explorer_Resize(object sender, EventArgs e)
        {
            // Adjust the maximum width based on the form size
            int maxPanelWidth = this.ClientSize.Width;

            // Set the maximum width of the navBarPanel
            this.navBarPanel.MaximumSize = new System.Drawing.Size(maxPanelWidth, this.navBarPanel.Height);
            this.navBarPanel.Width = this.ClientSize.Width;
            this.headerLabel.Width = this.ClientSize.Width;

            int headerHeight = headerLabel.Height;
            int navButtonHeight = navBarPanel.Height;

            // Calculate the desired height for folderListBox
            int desiredHeight = headerHeight + navButtonHeight;

            // Subtract the desired height from the total height of the form
            int newHeight = this.Height - desiredHeight;

            // Set the size and position of folderListBox
            folderListView.Size = new System.Drawing.Size(this.Width / 2 - 10, newHeight - 20);
            folderListView.Location = new Point(0, headerHeight + navButtonHeight);

            // Set the size and position of twoFolderListBox
            twoFolderListView.Size = new System.Drawing.Size(this.Width / 2 - 10, newHeight - 20);
            twoFolderListView.Location = new Point(this.Width / 2, headerHeight + navButtonHeight);

            this.columnHeader1.Width = this.folderListView.Width;
            this.columnHeader2.Width = this.twoFolderListView.Width;
        }

        private void OnFolderListBoxClick(object sender, EventArgs e)
        {
            lastSelectedListView = folderListView;
        }

        private void OnTwoFolderListBoxClick(object sender, EventArgs e)
        {
            lastSelectedListView = twoFolderListView;
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
        private void OnListViewDoubleClick(object sender, EventArgs e)
        {
            if (lastSelectedListView != null)
            {
                int selectedIndex = lastSelectedListView.SelectedIndices.Count > 0 ? lastSelectedListView.SelectedIndices[0] : -1;
                if (selectedIndex >= 0)
                {
                    // Assuming the first column contains the directory name
                    string selectedDirectory = lastSelectedListView.Items[selectedIndex].SubItems[0].Text;

                    // Check if the selected item is of type ListViewItem
                    if (lastSelectedListView.Items[selectedIndex].Tag is ListViewItem selectedItem)
                    {
                        selectedDirectory = selectedItem.Text;
                    }

                    string currentPath = pathEntry.Text;
                    string newPath = Path.Combine(currentPath, selectedDirectory);
                    pathEntry.Text = newPath;
                    ShowDirectoryContents(newPath);
                }
            }
        }




        private void RenderListBoxContents(ListView listView, string path)
        {
            listView.Items.Clear(); // Clear existing items

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
                        listView.Items.Add(new ListViewItem { Text = ".." + parentFolderName});
                    }

                    // Add directories to the listBox with folder icon
                    foreach (string directory in directories)
                    {
                        string folderName = Path.GetFileName(directory);
                        Image folderIcon = GetIconImage(IconChar.Folder);
                        listView.Items.Add(new ListViewItem { Text = folderName});
                    }

                    // Add files to the listBox with file icon
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        Image fileIcon = GetIconImage(IconChar.File);
                        listView.Items.Add(new ListViewItem { Text = fileName});
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

        // Method to show the contents of a directory
        private void ShowDirectoryContents(string path)
        {
            lastSelectedListView.Items.Clear(); // Clear existing items

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
                        lastSelectedListView.Items.Add(new ListViewItem { Text = ".." + parentFolderName});
                    }

                    // Add directories to the lastSelectedListView with folder icon
                    foreach (string directory in directories)
                    {
                        string folderName = Path.GetFileName(directory);
                        Image folderIcon = GetIconImage(IconChar.Folder);
                        lastSelectedListView.Items.Add(new ListViewItem { Text = folderName});
                    }

                    // Add files to the lastSelectedListView with file icon
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        Image fileIcon = GetIconImage(IconChar.File);
                        lastSelectedListView.Items.Add(new ListViewItem { Text = fileName});
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

                        folderListView.Items.Add(new ListViewItem { Text = entryName});
                        twoFolderListView.Items.Add(new ListViewItem { Text = entryName});
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

        // Method to get the icon image for a given FontAwesome icon
        private Image GetIconImage(IconChar icon)
        {
            int iconSize = 16; // Set the desired icon size
            Color iconColor = Color.Black; // Set the desired icon color

            // Add padding to the icon if needed
            var iconWithPadding = AddPaddingToIcon(icon, iconSize, iconColor, 0, FlipOrientation.Normal, 2);

            return iconWithPadding;
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
                lastSelectedListView.MultiSelect = true;
            }
            else
            {
                // If Ctrl key is not pressed, handle single item selection
                lastSelectedListView.MultiSelect = false;
            }

            int selectedIndex = lastSelectedListView.SelectedIndices.Count > 0 ? lastSelectedListView.SelectedIndices[0] : -1;
            if (selectedIndex >= 0 && !ctrlKeyPressed)
            {
                ListViewItem selectedItem = lastSelectedListView.SelectedItems[0];
                string selectedDirectory = selectedItem.Text; // Assuming the first column contains the directory name
                string currentPath = pathEntry.Text;
                string newPath = Path.Combine(currentPath, selectedDirectory);
                pathEntry.Text = newPath;
                ShowDirectoryContents(newPath);
            }
        }

        private void OnTwoFolderListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ctrlKeyPressed)
            {
                // If Ctrl key is pressed, handle multiple item selection
                // You may want to customize this behavior based on your requirements
                lastSelectedListView.MultiSelect = true;
            }
            else
            {
                // If Ctrl key is not pressed, handle single item selection
                lastSelectedListView.MultiSelect = false;
            }

            int selectedIndex = lastSelectedListView.SelectedIndices.Count > 0 ? lastSelectedListView.SelectedIndices[0] : -1;
            if (selectedIndex >= 0 && !ctrlKeyPressed)
            {
                ListViewItem selectedItem = lastSelectedListView.SelectedItems[0];
                string selectedDirectory = selectedItem.Text; // Assuming the first column contains the directory name
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
            if (lastSelectedListView.SelectedItems.Count > 0)
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
                            foreach (var selectedItem in lastSelectedListView.SelectedItems)
                            {
                                // Process each selected item
                                // For example, you can get the title and add it to the zip archive
                                string selectedText = ((ListViewItem)selectedItem).Text;
                                string fullPath = Path.Combine(pathEntry.Text, selectedText);

                                if (File.Exists(fullPath))
                                {
                                    // Add the file to the archive
                                    zipArchive.CreateEntryFromFile(fullPath, selectedText);
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
            int navButtonHeight = navBarPanel.Height;

            // Calculate the desired height for folderListBox
            int desiredHeight = headerHeight + navButtonHeight;

            // Subtract the desired height from the total height of the form
            int newHeight = this.Height - desiredHeight;

            // Set the size and position of folderListBox
            folderListView.Size = new System.Drawing.Size(this.Width / 2 - 10, newHeight - 20);
            folderListView.Location = new Point(0, headerHeight + navButtonHeight);

            // Set the size and position of twoFolderListBox
            twoFolderListView.Size = new System.Drawing.Size(this.Width / 2 - 10, newHeight - 20);
            twoFolderListView.Location = new Point(this.Width / 2, headerHeight + navButtonHeight);

            this.columnHeader1.Width = this.folderListView.Width;
            this.columnHeader2.Width = this.twoFolderListView.Width;



            folderListView.Enter += OnFolderListBoxClick;
            twoFolderListView.Enter += OnTwoFolderListBoxClick;

            

            folderListView.KeyDown += OnFolderListKeyDown;
            folderListView.KeyUp += OnFolderListKeyUp;

            // Set up the icon image for the Home button
            var homeIcon = IconChar.Home;
            var fileIcon = IconChar.Folder;
            var prevIcon = IconChar.LeftLong;
            var archIcon = IconChar.FileZipper;
            var unpackIcon = IconChar.FileDownload; // Add the unpack icon
            var copyIcon = IconChar.Copy;
            var pasteIcon = IconChar.Paste;
            var deleteIcon = IconChar.Remove;


            // Add padding to icons and set them to corresponding buttons
            var homeIconImage = AddPaddingToIcon(homeIcon, 15, Color.White, 0, FlipOrientation.Normal, 10);
            var fileIconImage = AddPaddingToIcon(fileIcon, 15, Color.White, 0, FlipOrientation.Normal, 10);
            var prevIconImage = AddPaddingToIcon(prevIcon, 15, Color.White, 0, FlipOrientation.Normal, 10);
            var archiveIconImage = AddPaddingToIcon(archIcon, 15, Color.White, 0, FlipOrientation.Normal, 10);
            var unpackIconImage = AddPaddingToIcon(unpackIcon, 15, Color.White, 0, FlipOrientation.Normal, 10); // Create the unpack icon
            var copyIconImage = AddPaddingToIcon(copyIcon, 15, Color.White, 0, FlipOrientation.Normal, 10); // Create the unpack icon
            var pasteIconImage = AddPaddingToIcon(pasteIcon, 15, Color.White, 0, FlipOrientation.Normal, 10); // Create the unpack icon
            var deleteIconImage = AddPaddingToIcon(deleteIcon, 15, Color.White, 0, FlipOrientation.Normal, 10); // Create the unpack icon

            // Set the icon image for the Home button
            navBarHomeButton.Image = homeIconImage;
            // Set the icon image for the File button
            navBarFileButton.Image = fileIconImage;
            // Set the icon image for the Prev button
            navBarPrevButton.Image = prevIconImage;
            navBarArchiveButton.Image = archiveIconImage;
            // Set the icon image and text for the Unpack button
            navBarUnpackButton.Image = unpackIconImage;

            navBarCopyButton.Image = copyIconImage;
            navBarPasteButton.Image = pasteIconImage;
            navBarDeleteButton.Image = deleteIconImage;

            navBarUnpackButton.Text = "Unpack"; // Set the appropriate text here

            // Set the button text for the File button
            navBarFileButton.Text = "Dir"; // Set the appropriate text here
                                             // Set the button text for the Prev button
            navBarPrevButton.Text = "Cancel"; // Set the appropriate text here
            navBarArchiveButton.Text = "Achive";
            navBarCopyButton.Text = "Copy";
            navBarPasteButton.Text = "paste";
            navBarDeleteButton.Text = "Delete";


            // Set the main directory when the form loads
            pathEntry.Text = mainDirectory;
            RenderListBoxContents(folderListView, mainDirectory);
            RenderListBoxContents(twoFolderListView, mainDirectory);

            // Align icon and text in the buttons
            navBarHomeButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarFileButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarPrevButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarUnpackButton.TextImageRelation = TextImageRelation.ImageBeforeText; // Align icon and text
            navBarArchiveButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarCopyButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarPasteButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            navBarDeleteButton.TextImageRelation = TextImageRelation.ImageBeforeText;

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

        private void OnNavBarUnpackButtonClick(object sender, EventArgs e)
        {
            // Call your unpack logic here
            // You can use folderListBox.SelectedItems to get the selected items
            if (lastSelectedListView.SelectedItems.Count > 0)
            {
                // Prompt the user to select a destination for the unpacked files
                using (var folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Выберите папку для распаковки файлов";
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string destinationFolder = folderDialog.SelectedPath;

                        foreach (var selectedItem in lastSelectedListView.SelectedItems)
                        {
                            // Process each selected item
                            // For example, you can get the title and extract it to the destination folder
                            string selectedText = ((ListViewItem)selectedItem).Text;
                            string fullPath = Path.Combine(pathEntry.Text, selectedText);

                            if (IsZipArchive(fullPath))
                            {
                                // If the selected item is a zip archive, extract its contents
                                string extractFolder = Path.Combine(destinationFolder, Path.GetFileNameWithoutExtension(selectedText));

                                try
                                {
                                    ZipFile.ExtractToDirectory(fullPath, extractFolder);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error extracting {selectedText}: {ex.Message}");
                                }
                            }
                            else
                            {
                                // If it's not a zip archive, copy or move the item to the destination folder
                                if (File.Exists(fullPath))
                                {
                                    // Copy or move the file to the destination folder
                                    string destinationPath = Path.Combine(destinationFolder, selectedText);
                                    File.Copy(fullPath, destinationPath, true); // Use File.Move if you want to move instead of copy
                                }
                                else if (Directory.Exists(fullPath))
                                {
                                    // Copy or move the entire directory to the destination folder
                                    string destinationPath = Path.Combine(destinationFolder, selectedText);
                                    CopyDirectory(fullPath, destinationPath); // Implement CopyDirectory method
                                }
                            }
                        }

                        MessageBox.Show("Распаковка успешно произведена!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет выбранных файлов для распаковки.");
            }
        }


        // Method to copy a directory and its contents recursively
        private void CopyDirectory(string sourcePath, string destinationPath)
        {
            Directory.CreateDirectory(destinationPath);

            foreach (var file in Directory.GetFiles(sourcePath))
            {
                string fileName = Path.GetFileName(file);
                string destinationFilePath = Path.Combine(destinationPath, fileName);
                File.Copy(file, destinationFilePath);
            }

            foreach (var directory in Directory.GetDirectories(sourcePath))
            {
                string directoryName = Path.GetFileName(directory);
                string destinationDirectoryPath = Path.Combine(destinationPath, directoryName);
                CopyDirectory(directory, destinationDirectoryPath);
            }
        }

        private void OnNavBarCopyButtonClick(object sender, EventArgs e)
        {
            // Copy the selected items in lastSelectedListView to the clipboard
            clipboardPaths.Clear();
            foreach (var selectedItem in lastSelectedListView.SelectedItems)
            {
                string selectedText = ((ListViewItem)selectedItem).Text;
                string fullPath = Path.Combine(pathEntry.Text, selectedText);
                clipboardPaths.Add(fullPath);
            }

            // Optionally, display a message to indicate successful copy
            MessageBox.Show("Выбранные файлы и папки скопированы в буфер обмена.");
        }

        private void OnNavBarPasteButtonClick(object sender, EventArgs e)
        {
                // Paste the copied items from the clipboard to the selected directory in lastSelectedListView
                string destinationDirectory = pathEntry.Text;

                if (clipboardPaths.Count > 0)
                {
                    foreach (string copiedPath in clipboardPaths)
                    {
                        try
                        {
                            if (File.Exists(copiedPath))
                            {
                                // Copy the file to the destination folder
                                string destinationPath = Path.Combine(destinationDirectory, Path.GetFileName(copiedPath));
                                File.Copy(copiedPath, destinationPath, true); // Use File.Move if you want to move instead of copy
                            }
                            else if (Directory.Exists(copiedPath))
                            {
                                // Copy the entire directory to the destination folder
                                string destinationPath = Path.Combine(destinationDirectory, Path.GetFileName(copiedPath));
                                CopyDirectory(copiedPath, destinationPath); // Implement CopyDirectory method
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error pasting {copiedPath}: {ex.Message}");
                        }
                    }

                    // Optionally, display a message to indicate successful paste
                    MessageBox.Show("Файлы и папки успешно вставлены.");
                }
                else
                {
                    // Optionally, display a message if the clipboard is empty
                    MessageBox.Show("Буфер обмена пуст.");
                }

                // Refresh the contents of lastSelectedListView after pasting
                ShowDirectoryContents(destinationDirectory);
            }

        private void navBarDeleteButton_Click(object sender, EventArgs e)
        {
            if (lastSelectedListView.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранные элементы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        foreach (var selectedItem in lastSelectedListView.SelectedItems)
                        {
                            string selectedText = ((ListViewItem)selectedItem).Text;
                            string fullPath = Path.Combine(pathEntry.Text, selectedText);

                            if (File.Exists(fullPath))
                            {
                                File.Delete(fullPath);
                            }
                            else if (Directory.Exists(fullPath))
                            {
                                Directory.Delete(fullPath, true);
                            }
                        }

                        ShowDirectoryContents(pathEntry.Text);
                        MessageBox.Show("Удаление успешно выполнено!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет выбранных элементов для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
