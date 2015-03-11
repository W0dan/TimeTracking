using System.Linq;

namespace TimeTracking.Logic.ProjectReport
{
    public class ProjectReportHandler
    {
        private readonly Repository _repository = new Repository(ConfigSettings.TimeTrackingFile, "timetracking");

        public Project Handle(string projectName)
        {
            var doc = _repository.Load();

            var projectNode = doc.Descendants("projects").Single()
                .Descendants("project").Single(p => p.Attribute("name").Value == projectName);

            var trackedTimes = projectNode.Descendants("trackedtime").Select(tt=> new TrackedTime(tt.Attribute("when").Value, tt.Attribute("howlong").Value,tt.Attribute("description").Value)).ToList();

            return new Project(projectName, trackedTimes);
        }
    }
}