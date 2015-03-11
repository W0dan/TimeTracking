using System.Windows;

namespace TimeTracking.Logic.ProjectReport
{
    /// <summary>
    /// Interaction logic for ProjectReportWindow.xaml
    /// </summary>
    public partial class ProjectReportWindow : Window
    {
        public ProjectReportWindow(Project project)
        {
            InitializeComponent();

            ProjectnameLabel.Content = project.Name;

            TotalNumberOfHoursLabel.Content = string.Format("Total number of hours: {0:0.00}",
                                                            project.TotalTime.TotalHours);

            foreach (var trackedTime in project.TrackedTimes)
            {
                Entries.Children.Add(new ProjectReportEntry(trackedTime));
            }
        }
    }
}
