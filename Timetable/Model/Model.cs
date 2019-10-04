using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Persistence;

namespace Timetable.Model
{
    public class Model
    {
        public Model(ITimetableStorage storage)
        {
            _storage = storage;
            try
            {
                _timetable = _storage.Load("default");
            }
            catch
            {
                _timetable = new Persistence.Timetable();
            }
        }

        public string this[DayOfWeek day, int lesson]
        {
            get
            {
                if (_timetable.Days.TryGetValue(day, out var lessons))
                {
                    return lessons.Count <= lesson ? null : lessons[lesson];
                }
                return null;
            }
            set
            {
                var lessons = GetOrCreateDay(day);
                while (lessons.Count < lesson)
                {
                    lessons.Add(null);
                }
                lessons.Add(value);
            }
        }

        public DayOfWeek LongestDay()
        {
            return _timetable.Days
                .Aggregate((maxDay, dayLessons) =>
                    maxDay.Value.Count < dayLessons.Value.Count
                    ? dayLessons : maxDay)
                .Key;
        }

        public int DayLength(DayOfWeek day)
        {
            return _timetable.Days[day].Count;
        }

        private IList<string> GetOrCreateDay(DayOfWeek day)
        {
            if (_timetable.Days.TryGetValue(day, out var lessons))
            {
                return lessons;
            }
            else
            {
                var newLessons = new List<string>();
                _timetable.Days.Add(day, newLessons);
                return newLessons;
            }
        }

        private readonly ITimetableStorage _storage;
        private Persistence.Timetable _timetable;
    }
}
