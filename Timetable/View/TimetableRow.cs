using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.View
{
    class TimetableRow
    {
        public string Monday => GetValue(DayOfWeek.Monday);
        public string Tuesday => GetValue(DayOfWeek.Tuesday);
        public string Wednesday => GetValue(DayOfWeek.Wednesday);
        public string Thursday => GetValue(DayOfWeek.Thursday);
        public string Friday => GetValue(DayOfWeek.Friday);

        private IDictionary<DayOfWeek, string> _data = new Dictionary<DayOfWeek, string>();

        public TimetableRow() {}

        public string this[DayOfWeek i] {
            get
            {
                return _data[i];
            }
            set
            {
                _data[i] = value;
            }
        }

        private string GetValue(DayOfWeek day)
        {
            if (_data.TryGetValue(day, out var lesson))
            {
                return lesson;
            }
            return null;
        }
    }
}
