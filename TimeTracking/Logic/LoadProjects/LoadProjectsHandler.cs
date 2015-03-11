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
                     select xElement.Attribute("name") 
                     into xAttribute where xAttribute != null 
                     select new Project(xAttribute.Value)).ToList();
         }
    }
}