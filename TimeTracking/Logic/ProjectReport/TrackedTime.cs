using System;

namespace TimeTracking.Logic.ProjectReport
{
    public class TrackedTime
    {
        public TrackedTime(string when, string howlong, string description)
        {
            When = DateTime.ParseExact(when, "dd/MM/yyyy HH:mm:ss", null);
            HowLong = TimeSpan.Parse(howlong);
            Description = description;
        }

        public DateTime When { get; set; }
        public TimeSpan HowLong { get; set; }
        public string Description { get; set; }
    }
}