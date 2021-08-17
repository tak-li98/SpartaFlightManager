using System;
using System.Collections.Generic;

namespace Database.Services
{
    public interface IPilotService
    {
        public List<Pilot> GetPilotList();
        public Pilot GetPilotById(string pilotId);
        public void CreatePilot(Pilot p);
        public void SavePilotChanges();
        public void RemovePilot(Pilot p);
    }
}
