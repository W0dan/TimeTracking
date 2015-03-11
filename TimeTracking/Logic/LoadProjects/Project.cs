namespace TimeTracking.Logic.LoadProjects
{
    public class Project
    {
        private readonly string _name;

        public Project(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}