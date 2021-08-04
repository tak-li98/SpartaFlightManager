using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Manager
{
    public class PilotManager
    {
        public Pilot SelectedPilot { get; set; }

        public void SetPilot(object selectedItem)
        {
            SelectedPilot = (Pilot)selectedItem;
        }
        public List<Pilot> RetrieveAll()
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Pilots.ToList();
            }
        }
    }
}
