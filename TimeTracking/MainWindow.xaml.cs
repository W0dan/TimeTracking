using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimeTracking.Logic.LoadProjects;
using TimeTracking.Logic.SaveProject;

namespace TimeTracking
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var projects = new LoadProjectsHandler().Handle();

            foreach (var project in projects)
            {
                var pli = new ProjectListItemControl(project);
                StackPanel.Children.Add(pli);
            }

            var newProjectLabel = new Label
                                 {
                                     Content = "New project",
                                     HorizontalAlignment = HorizontalAlignment.Stretch,
                                     VerticalAlignment = VerticalAlignment.Stretch,
                                     HorizontalContentAlignment = HorizontalAlignment.Center,
                                     VerticalContentAlignment = VerticalAlignment.Center,
                                     Background = new SolidColorBrush(Colors.LimeGreen)
                                 };
            newProjectLabel.MouseUp += NewProjectLabelMouseUp;
            StackPanel.Children.Add(newProjectLabel);
        }

        private void NewProjectLabelMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //show dialog to input new project name
            var projectName = InputWindow.GetText("New project", "Enter the name for your project here !", t => t.Length >= 3 && t.Length <= 40);

            //add project to stackpanel
            var project = new Project(projectName);
            var pli = new ProjectListItemControl(project);
            StackPanel.Children.Insert(StackPanel.Children.Count - 1, pli);

            //save new project
            new SaveProjectHandler().Handle(project);
        }
    }
}
