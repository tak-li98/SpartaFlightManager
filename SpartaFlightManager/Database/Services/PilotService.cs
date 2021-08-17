using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public class PilotService : IPilotService
    {
        private readonly SpartaFlightContext _context;

        public PilotService(SpartaFlightContext context)
        {
            _context = context;
        }

        public PilotService()
        {
            _context = new SpartaFlightContext();
        }

        public List<Pilot> GetPilotList()
        {
            return _context.Pilots.OrderBy(i => i.FirstName).ToList();
        }

        public Pilot GetPilotById(int pilotId)
        {
            return _context.Pilots.Where(p => p.PilotId == pilotId).FirstOrDefault();
        }

        public int GetPilotIdByDetails(string pilotStr)
        {
            return _context.Pilots.Where(p => pilotStr.Contains(p.FirstName) && pilotStr.Contains(p.LastName) && pilotStr.Contains(p.Title)).FirstOrDefault().PilotId;
        }

        public void CreatePilot(Pilot p)
        {
            _context.Pilots.Add(p);
            _context.SaveChanges();
        }

        public void SavePilotChanges()
        {
            _context.SaveChanges();
        }

        public void RemovePilot(Pilot p)
        {
            _context.Pilots.Remove(p);
            _context.SaveChanges();
        }

        
    }
}
