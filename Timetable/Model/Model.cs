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

        public Persistence.Timetable GetTimetable()
        {
            return _timetable;
        }

        private readonly ITimetableStorage _storage;
        private Persistence.Timetable _timetable;
    }
}
