using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Persistence
{
    public class Timetable
    {
        //Létrehoztunk egy szótárat ami tartalmazz egy
        // kulcsot(napok) és egy hozzá való értéket(órák) hányadik és milyen
        public IDictionary<DayOfWeek, IList<string>> Days { get; set; } 
        //létrehoztunk egy konstruktort, beálítjuk az értékeket
        public Timetable()
        {   // Milyen napok lehetnek
            Days = new Dictionary<DayOfWeek, IList<string>>()
            {
                {DayOfWeek.Monday, new List<string>()},
                {DayOfWeek.Tuesday, new List<string>()},
                {DayOfWeek.Wednesday, new List<string>()},
                {DayOfWeek.Thursday, new List<string>()},
                {DayOfWeek.Friday, new List<string>()}
            };
        }
    }
    // csináltunk egy interface-t, hogy milyen függvényeink vannak
    public interface ITimetableStorage
    { //betöltés
        Timetable Load(string resourceId);
        // mentés
        bool Save(string resourceId, Timetable timetable);
    }
}
