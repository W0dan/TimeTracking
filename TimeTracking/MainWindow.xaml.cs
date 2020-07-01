using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
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

            Line lastLine = null;
            foreach (var project in projects)
            {
                var pli = new ProjectListItemControl(project);
                StackPanel.Children.Add(pli);
                lastLine = new Line();
                StackPanel.Children.Add(lastLine);
            }

            if (lastLine != null)
                StackPanel.Children.Remove(lastLine);

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
            var projectName = InputWindow.GetText("New project", "Enter the name for your project here !", text => text.Length >= 3 && text.Length <= 50);
            var projectDescription = InputWindow.GetText("Project description", "Enter a description for your project", text => true);

            //add project to stackpanel
            var project = new Project(projectName, projectDescription);
            var pli = new ProjectListItemControl(project);
            StackPanel.Children.Insert(StackPanel.Children.Count - 1, pli);

            //save new project
            new SaveProjectHandler().Handle(project);
        }
    }
}
