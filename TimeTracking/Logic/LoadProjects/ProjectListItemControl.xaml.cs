using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TimeTracking.Logic.ProjectReport;
using TimeTracking.Logic.TimeTrackingStopped;

namespace TimeTracking.Logic.LoadProjects
{
    public partial class ProjectListItemControl : UserControl
    {
        private readonly string _projectName;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private TimeTrackerStatus _timerRunning;
        private DateTime _elapsedTime;

        public ProjectListItemControl(Project project)
        {
            _projectName = project.Name;
            InitializeComponent();

            ProjectName.Content = _projectName;

            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _timer.Tick += TimerTick;
        }

        void TimerTick(object sender, EventArgs e)
        {
            _elapsedTime = _elapsedTime.AddSeconds(1);
            ElapsedTime.Content = _elapsedTime.ToString("HH:mm:ss");
        }

        private void StartStopButtonClick(object sender, MouseButtonEventArgs e)
        {
            ElapsedTime.Background = new SolidColorBrush(Colors.White);
            if (_timerRunning == TimeTrackerStatus.Stopped)
            {
                _timerRunning = TimeTrackerStatus.Running;
                _elapsedTime = new DateTime();
                ElapsedTime.Content = _elapsedTime.ToString("HH:mm:ss");
                StartStopButton.Content = "Stop";
                StartStopButton.Background = new SolidColorBrush(Colors.Red);
                _timer.Start();
            }
            else
            {
                _timer.Stop();
                _timerRunning = TimeTrackerStatus.Stopped;

                //request for description
                var description = InputWindow.GetText("Description", "your description comes here !", t => t.Length >= 3);
                new TimeTrackingStoppedHandler().Handle(_projectName, DateTime.Now, _elapsedTime, description);

                StartStopButton.Content = "Start";
                StartStopButton.Background = new SolidColorBrush(Colors.Green);
            }
        }

        private void PauseButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (_timerRunning == TimeTrackerStatus.Running)
            {
                _timerRunning = TimeTrackerStatus.Paused;
                ElapsedTime.Background = new SolidColorBrush(Colors.LightGray);
                _timer.Stop();
            }
            else if (_timerRunning == TimeTrackerStatus.Paused)
            {
                _timerRunning = TimeTrackerStatus.Running;
                ElapsedTime.Background = new SolidColorBrush(Colors.White);
                _timer.Start();
            }
        }

        private void ReportButtonClick(object sender, MouseButtonEventArgs e)
        {
            var report = new ProjectReportHandler().Handle(_projectName);

            var reportWindow = new ProjectReportWindow(report);

            reportWindow.ShowDialog();
        }
    }
}
