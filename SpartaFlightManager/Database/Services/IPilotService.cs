using System;
using System.Collections.Generic;

namespace Database.Services
{
    public interface IPilotService
    {
        public List<Pilot> GetPilotList();
        public Pilot GetPilotById(int pilotId);
        public int GetPilotIdByDetails(string pilotStr);
        public void CreatePilot(Pilot p);
        public void SavePilotChanges();
        public void RemovePilot(Pilot p);
    }
}
