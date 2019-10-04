using System;

namespace Timetable.Persistence
{
    public class PersistenceException : System.Exception
    {
        // Paramal megadjuk a f�jl nev�t 
        public string Param { get; private set; }
        // message = �zenet, param = param�ter, exception = kiv�tel kezel�s
        public PersistenceException(string message, string Param, Exception e)
            : base(message, e)
        {
            this.Param = Param;
        }
    }
}


