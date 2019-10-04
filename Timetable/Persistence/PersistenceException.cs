using System;

namespace Timetable.Persistence
{
    public class PersistenceException : System.Exception
    {
        // Paramal megadjuk a fájl nevét 
        public string Param { get; private set; }
        // message = üzenet, param = paraméter, exception = kivétel kezelés
        public PersistenceException(string message, string Param, Exception e)
            : base(message, e)
        {
            this.Param = Param;
        }
    }
}


