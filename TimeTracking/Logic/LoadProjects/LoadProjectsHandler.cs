using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TimeTracking.Logic.LoadProjects
{
    public class LoadProjectsHandler
    {
        private readonly Repository _repository = new Repository(ConfigSettings.TimeTrackingFile, "timetracking");

         public IEnumerable<Project> Handle()
         {
             var doc = _repository.Load();

             if (!doc.Descendants("projects").Any())
             {
                 var newProjectsNode = new XElement("projects");
                 if (doc.Root == null)
                     throw new NullReferenceException("the root element cannot be null");
                 doc.Root.Add(newProjectsNode);
             }

             var projects = doc.Descendants("projects").First().Descendants().ToList();

             return (from xElement in projects
                     where xElement.Name == "project"
                     select new { prjName = xElement.Attribute("name"), prjDesc = xElement.Attribute("description") }
                     into xProject where xProject != null 
                     select new Project(xProject.prjName.Value, xProject.prjDesc?.Value ?? "")).ToList();
         }
    }
}