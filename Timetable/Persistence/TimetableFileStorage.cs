using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Persistence
{
    internal class TimetableFileStorage : ITimetableStorage
    {
        // Monday
        // 0 Szakmai informatika
        // 2 def
        // 6 ...
        //
        // Wednesday
        // ...
        //
        // Tuesday
        // ...

        public Timetable Load(string resourceId)
        {
            var path = Path.Combine(
                System.AppDomain.CurrentDomain.BaseDirectory,
                Path.ChangeExtension(resourceId, "timetable"));
            try
            {
                using (var reader = new StreamReader(path))
                {
                    var days = new Dictionary<DayOfWeek, IList<string>>();
                    while (!reader.EndOfStream)
                    {
                        var (dayOfWeek, lessons) = ReadDay(reader);
                        days.Add(dayOfWeek, lessons);
                    }
                    return new Timetable { Days = days };
                }
            } catch(Exception e)
            {
                throw new PersistenceException("Can not load file", resourceId, e);
            }
        }

        public bool Save(string resourceId, Timetable timetable)
        {
            throw new NotImplementedException();
        }

        private Tuple<DayOfWeek, IList<string>> ReadDay(StreamReader reader)
        {
            string line = reader.ReadLine();
            DayOfWeek day = ParseDayName(line);
            IList<string> lessons = new List<string>();

            line = reader.ReadLine();
            while(!reader.EndOfStream && line.Length != 0)
            {
                var tokens = line.Split(' ');
                if (tokens.Length < 2)
                {
                    throw new FormatException();
                }

                var lessonNumber = uint.Parse(tokens[0]);
                while (lessons.Count != lessonNumber)
                {
                    lessons.Add(null);
                }
                lessons.Add(string.Join(" ", tokens.Skip(1)));

                line = reader.ReadLine();
            }

            return new Tuple<DayOfWeek, IList<string>>(day, lessons);
        }

        private static DayOfWeek ParseDayName(string str)
        {
            switch (str)
            {
                case "Monday":
                    return DayOfWeek.Monday;
                case "Tuesday":
                    return DayOfWeek.Tuesday;
                case "Wednesday":
                    return DayOfWeek.Wednesday;
                case "Thursday":
                    return DayOfWeek.Thursday;
                case "Friday":
                    return DayOfWeek.Friday;
                case "Saturday":
                    return DayOfWeek.Saturday;
                case "Sunday":
                    return DayOfWeek.Sunday;
                default:
                    throw new ArgumentOutOfRangeException(nameof(str), str, "Not a week day");
            }
        }
    }
}
