using System;

namespace Timetable.Persistence
{
    public class PersistenceException : System.Exception
    {
        public string Param { get; private set; }

        public PersistenceException(string message, string Param, Exception e)
            : base(message, e)
        {
            this.Param = Param;
        }
    }
}


