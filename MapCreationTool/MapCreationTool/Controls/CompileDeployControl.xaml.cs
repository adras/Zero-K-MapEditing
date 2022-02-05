using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MapCreationTool.Controls
{
    public class ActionTypes
    {
        public static string compile = "Compile";
        public static string compileDeploy = "Compile & Deploy folder";
        public static string compileDeploy7z = "Compile & Deploy 7z";
        public static string deployFolder = "Deploy folder";
        public static string deploy7z = "Deploy 7z";

        string actionType;

        public ActionTypes(string actionType)
        {
            this.actionType = actionType;
        }

        public static List<string> GetList()
        {
            return new List<string>
            {
                compile,
                compileDeploy,
                compileDeploy7z,
                deployFolder,
                deploy7z
            };
        }
    }


    /// <summary>
    /// Interaction logic for CompileDeployControl.xaml
    /// </summary>
    public partial class CompileDeployControl : UserControl
    {
        public delegate void ExecuteAction(object sender, ActionTypes actionType);
        public event ExecuteAction OnExecuteAction;

        public CompileDeployControl()
        {
            InitializeComponent();
            List<string> items = ActionTypes.GetList();
            cbxAction.ItemsSource = items;
            cbxAction.SelectedIndex = 0;
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            ActionTypes actionType = new ActionTypes((string)cbxAction.SelectedValue);
            OnExecuteAction?.Invoke(this, actionType);
        }
    }
}
