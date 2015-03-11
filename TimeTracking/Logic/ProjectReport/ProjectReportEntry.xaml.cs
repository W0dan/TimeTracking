using System.Windows.Controls;

namespace TimeTracking.Logic.ProjectReport
{
    /// <summary>
    /// Interaction logic for ProjectReportEntry.xaml
    /// </summary>
    public partial class ProjectReportEntry : UserControl
    {
        public ProjectReportEntry(TrackedTime trackedTime)
        {
            InitializeComponent();

            WhenLabel.Content = trackedTime.When.ToString("dd/MM/yyyy HH:mm:ss");
            HowLongLabel.Content = trackedTime.HowLong.TotalHours.ToString("0.00");
            DescriptionLabel.Content = trackedTime.Description;
        }
    }
}
