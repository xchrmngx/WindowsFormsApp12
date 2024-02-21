using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp12
{
    internal class Cosmonaut
    {
        public string Name;
        public int Age;
        public int FlightCount;
        public int TotalFlightDays;

        public Cosmonaut()
        {
            Name = "Имя космонавта";
            Age = 30;
            FlightCount = 0;
            TotalFlightDays = 0;
        }

        public int CalculateTotalHours()
        {
            return TotalFlightDays * 24;
        }

        public void AddFlight(int days)
        {
            FlightCount++;
            TotalFlightDays += days;
        }
    }
}
