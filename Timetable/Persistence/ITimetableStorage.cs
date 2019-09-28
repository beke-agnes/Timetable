using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Persistence
{
    public class Timetable
    {
        public IDictionary<DayOfWeek, IList<string>> Days { get; set; }

        public Timetable()
        {
            Days = new Dictionary<DayOfWeek, IList<string>>()
            {
                {DayOfWeek.Monday, new List<string>() { null } },
                {DayOfWeek.Tuesday, new List<string>()},
                {DayOfWeek.Wednesday, new List<string>()},
                {DayOfWeek.Thursday, new List<string>()},
                {DayOfWeek.Friday, new List<string>()}
            };
        }
    }

    public interface ITimetableStorage
    {
        Timetable Load(string resourceId);
        bool Save(string resourceId, Timetable timetable);
    }
}
