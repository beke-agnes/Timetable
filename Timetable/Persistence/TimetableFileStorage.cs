using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Persistence
{ // behoztuk az osztály leszármazást, leszármazunk az interface-ből 
    internal class TimetableFileStorage : ITimetableStorage
    {
        // Monday(hétfő)
        // 0 Szakmai informatika
        // 2 def
        // 6 ...(six)
        //
        // Wednesday
        // ...
        //
        // Tuesday
        // ...

            // betöltés függvénye, load string resourceId = fájl neve
        public Timetable Load(string resourceId)
        {
            // path = kombinálja hol kapjuk meg és neve + kiterjesztés
            var path = Path.Combine(
                System.AppDomain.CurrentDomain.BaseDirectory,
                Path.ChangeExtension(resourceId, "timetable"));
            // megprobáljuk és ha nem sikerül megadunk egy exceptiont
            try
            {
                // létrehozunk egy változot az olvasáshoz
                using (var reader = new StreamReader(path))
                {
                    var days = new Dictionary<DayOfWeek, IList<string>>();
                    // végigmegyünk a fájlon ameddig nincs vége
                    while (!reader.EndOfStream)
                    {
                        Tuple<DayOfWeek, IList<string>> temp;
                       
                        temp = ReadDay(reader);
                        days.Add(temp.Item1, temp.Item2);
                    }
                    return new Timetable { Days = days };
                }
                // használjuk a megirt exceptiont
            } catch(Exception e)
            {
                //throw new Exception();
                throw new PersistenceException("Can not load file", resourceId, e);
            }
        }
        // elmenti ha modosítást hajtunk végre
        public bool Save(string resourceId, Timetable timetable)
        {
            throw new NotImplementedException();
        }
        // rendezett lista 
        private Tuple<DayOfWeek, IList<string>> ReadDay(StreamReader reader)
        {
            string line = reader.ReadLine(); // soronként beolvassuk
            DayOfWeek day = ParseDayName(line); // megadtuk, hogy soronként vegye észre
            IList<string> lessons = new List<string>(); // létrehozunk egy listátt az óráknak és hogy hányadik óra
            // soronként olvassuk
            line = reader.ReadLine();
            // addig megy még nincs vége
            while(!reader.EndOfStream && line.Length != 0)
            {
                // sorrokat szétvágjuk szóköz alapján
                var tokens = line.Split(' ');
                if (tokens.Length < 2) 
                {
                    throw new FormatException();
                }
                // megadjuk, hogy hányadik óra 
                var lessonNumber = uint.Parse(tokens[0]);
                // megszámoljuk, hogy hány óra volt 
                //és ez nem egyedik a sorok count-jával és feltöltjuk 0 val
                while (lessons.Count != lessonNumber)
                {
                    lessons.Add(null);
                }
                // összekapcsoljuk a maradék stringet
                lessons.Add(string.Join(" ", tokens.Skip(1)));
                // olvassuk a kövi sort 
                line = reader.ReadLine();
            }
            // visszadunk egy rendezett listát
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
