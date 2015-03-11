using System.IO;
using System.Xml.Linq;

namespace TimeTracking
{
    public class Repository
    {
        private readonly string _xmlFilename;
        private readonly string _rootElement;

        private XDocument _doc;

        public Repository(string xmlFilename, string rootElement)
        {
            _xmlFilename = xmlFilename;
            _rootElement = rootElement;
        }

        public XDocument Load()
        {
            if (!File.Exists(_xmlFilename))
                using (var fs = File.Create(_xmlFilename))
                using (var sw = new StreamWriter(fs))
                    sw.WriteLine("<" + _rootElement + "></" + _rootElement + ">");

            return _doc ?? (_doc = XDocument.Load(_xmlFilename));
        }

        public void Save()
        {
            if (_doc != null)
                _doc.Save(_xmlFilename);
        }
    }
}
