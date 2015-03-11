using System;
using System.Linq;
using System.Xml.Linq;
using TimeTracking.Logic.LoadProjects;

namespace TimeTracking.Logic.SaveProject
{
    internal class SaveProjectHandler
    {
        private readonly Repository _repository = new Repository(ConfigSettings.TimeTrackingFile, "timetracking");

        public void Handle(Project project)
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

            var projectNode = new XElement("project");
            projectNode.Add(new XAttribute("name", project.Name));
            projects.Add(projectNode);

            _repository.Save();
        }
    }
}