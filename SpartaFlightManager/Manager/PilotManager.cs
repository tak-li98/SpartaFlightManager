using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using System.Diagnostics;
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

        public void Create(string firstName, string lastName, string title = null)
        {
            var newPilot = new Pilot() { Title = title, FirstName = firstName, LastName = lastName };
            using(var db = new SpartaFlightContext())
            {
                db.Pilots.Add(newPilot);
                db.SaveChanges();
            }
        }

        public bool Update(int pilotId,string firstName, string lastName, string title = null)
        {
            using (var db = new SpartaFlightContext())
            {
                var updatePilot = db.Pilots.Where(f => f.PilotId == pilotId).FirstOrDefault();
                if (updatePilot == null)
                {
                    return false;
                }
                updatePilot.FirstName = firstName;
                updatePilot.LastName = lastName;
                updatePilot.Title = title;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error updating pilot {pilotId}");
                    return false;
                }
            }
            return true;
        }

        public bool Delete(int pilotId)
        {
            using (var db = new SpartaFlightContext())
            {
                var delPilot = db.Pilots.Where(p => p.PilotId == pilotId);
                if(delPilot == null)
                {
                    return false;
                }
                foreach (var pilot in delPilot)
                {
                    db.Pilots.RemoveRange(pilot);
                }
                db.SaveChanges();
            }
            return true;
        }
    }
}
