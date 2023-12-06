using System;
using System.IO;
using System.Windows.Forms;

namespace simple_explorer
{
    public partial class simple_explorer : Form
    {

        public simple_explorer()
        {
            InitializeComponent();
        }



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

        private void OnListboxDoubleClick(object sender, EventArgs e)
        {
            int selectedIndex = listBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string selectedDirectory = listBox.Items[selectedIndex].ToString();
                string currentPath = pathEntry.Text;
                string newPath = Path.Combine(currentPath, selectedDirectory);
                pathEntry.Text = newPath;
                ShowDirectoryContents(newPath);
            }
        }

        private void ShowDirectoryContents(string path)
        {
            listBox.Items.Clear(); // Clear existing contents

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                foreach (string directory in directories)
                {
                    listBox.Items.Add(Path.GetFileName(directory));
                }

                foreach (string file in files)
                {
                    listBox.Items.Add(Path.GetFileName(file));
                }
            }
            catch (Exception ex)
            {
                listBox.Items.Add($"Error: {ex.Message}");
            }
        }

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
    }
}
