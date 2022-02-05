using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;

namespace MapCreationTool
{
    /// <summary>
    /// Interaction logic for UpdateMapPage.xaml
    /// </summary>
    public partial class UpdateMapPage : Page
    {
        public UpdateMapPage()
        {
            InitializeComponent();
        }

        private void btnSelectWorkingDir_Click(object sender, RoutedEventArgs e)
        {
            // Move this to dialogHelper.GetWorkDirectory method
            Forms.FolderBrowserDialog dialog = new Forms.FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.Description = "Select Map WorkDirectory";
            Forms.DialogResult result = dialog.ShowDialog();
            if (result == Forms.DialogResult.OK)
            {
                tbMapWorkingDir.Text = dialog.SelectedPath;
            }

        }

        private void tbMapWorkingDir_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadLuaInfo(tbMapWorkingDir.Text);
        }


        private void LoadLuaInfo(string text)
        {
            // Move this method to custom file class like it was doen for MapCreationHelper
        }
    }
}
