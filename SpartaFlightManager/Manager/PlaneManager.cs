using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Manager
{
    public class PlaneManager
    {
        public List<Plane> RetrieveAll()
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Planes.OrderBy(i => i.Capacity).ToList();
            }
        }

        public int ReturnCapacity(string model)
        {
            using (var db = new SpartaFlightContext())
            {
                return (int)db.Planes.Where(p => p.PlaneModel == model).FirstOrDefault().Capacity;
            }
        }

        public int ReturnPlaneID(string str)
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Planes.Where(p => p.PlaneModel == str).FirstOrDefault().PlaneId;
            }
        }
        public Plane SelectedPlane { get; set; }
        public void SetSelectedPlane(object selectedItem)
        {
            SelectedPlane = (Plane)selectedItem;
        }
    }
}
