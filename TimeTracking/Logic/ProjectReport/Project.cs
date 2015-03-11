using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTracking.Logic.ProjectReport
{
    public class Project
    {
        private readonly string _name;
        private readonly List<TrackedTime> _trackedTimes;
        private readonly TimeSpan _totalTime;

        public Project(string name, IEnumerable<TrackedTime> trackedTimes)
        {
            _name = name;
            _trackedTimes = trackedTimes.ToList();
            _totalTime = _trackedTimes.Any()
                ? _trackedTimes.Select(tt => tt.HowLong).Aggregate((x, y) => x.Add(y)) 
                : new TimeSpan(0);
        }

        public TimeSpan TotalTime
        {
            get { return _totalTime; }
        }

        public IEnumerable<TrackedTime> TrackedTimes
        {
            get { return _trackedTimes; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}