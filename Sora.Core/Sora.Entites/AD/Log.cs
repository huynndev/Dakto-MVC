using Sora.Entites.Interfaces;
using System;

namespace Sora.Entites.AD
{
    public class Log : Auditable
    {
        public int Id { get; set; }

        public string LogMessage { get; set; }
    }
}
