using System;
using System.Linq;
using System.Xml.Linq;

namespace TimeTracking.Logic.TimeTrackingStopped
{
    public class TimeTrackingStoppedHandler
    {
        private readonly Repository _repository = new Repository(ConfigSettings.TimeTrackingFile, "timetracking");

        public void Handle(string projectName, DateTime when, DateTime howLong, string description)
        {
            var doc = _repository.Load();

            if (!doc.Descendants("projects").Any())
            {
                var newProjectsNode = new XElement("projects");
                if (doc.Root == null)
                    throw new NullReferenceException("the root element cannot be null");
                doc.Root.Add(newProjectsNode);
            }

            var projects = doc.Descendants("projects").First();

            var project = (from p in projects.Descendants("project")
                           let projectNameAttr = p.Attribute("name")
                           where projectNameAttr != null && projectNameAttr.Value == projectName
                           select p)
                          .SingleOrDefault();

            if (project == null)
            {
                project = new XElement("project");
                project.Add(new XAttribute("name", projectName));
                projects.Add(project);
            }

            var trackedTime = new XElement("trackedtime");
            trackedTime.Add(new XAttribute("when", when.ToString("dd/MM/yyyy HH:mm:ss")));
            trackedTime.Add(new XAttribute("howlong", howLong.ToString("HH:mm:ss")));
            trackedTime.Add(new XAttribute("description", description));
            project.Add(trackedTime);

            _repository.Save();
        }
    }
}