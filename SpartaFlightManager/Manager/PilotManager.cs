using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Database.Services;
using System.Diagnostics;
namespace Manager
{

    public class PilotManager
    {
        private IPilotService _service;
        public Pilot SelectedPilot { get; set; }

        public PilotManager(IPilotService service)
        {
            if(service == null)
            {
                throw new ArgumentException("IPilotService object cannot be null");
            }
            _service = service;  
        }
        
        public PilotManager()
        {
            _service = new PilotService();
        }
        public void SetSelectedPilot(object selectedItem)
        {
            SelectedPilot = (Pilot)selectedItem;
        }
        public List<Pilot> RetrieveAll()
        {
            return _service.GetPilotList();
        }
        public int ReturnPilotID(string str)
        {
            return _service.GetPilotIdByDetails(str);
        }

        public void Create(string firstName, string lastName, string title = null,string photoLink = null)
        {
            if(photoLink == null)
            {
                photoLink = "PilotPics/Default.jpg";
            }
            var newPilot = new Pilot() { Title = title, FirstName = firstName, LastName = lastName , PhotoLink = photoLink};
            _service.CreatePilot(newPilot);
        }

        public bool UpdatePhoto(int pilotId,string photoLink = null)
        {
            var updatePilot = _service.GetPilotById(pilotId);
            if (updatePilot == null)
            {
                return false;
            }
            updatePilot.PhotoLink = photoLink;

            try
            {
                _service.SavePilotChanges();
                
            }
            catch (Exception)
            {
                Debug.WriteLine($"Error updating pilot {pilotId}");
                return false;
            }
            return true;
        }
        public bool Update(int pilotId, string firstName, string lastName, string title = null)
        {
            var updatePilot = _service.GetPilotById(pilotId);
            if (updatePilot == null)
            {
                return false;
            }
            updatePilot.FirstName = firstName;
            updatePilot.LastName = lastName;
            updatePilot.Title = title;

            try
            {
                _service.SavePilotChanges();
                SelectedPilot = updatePilot;
            }
            catch (Exception)
            {
                Debug.WriteLine($"Error updating pilot {pilotId}");
                return false;
            }
            return true;
        }

        public bool Delete(int pilotId)
        {
            var delPilot = _service.GetPilotById(pilotId);
            if (delPilot == null)
            {
                return false;
            }
            _service.RemovePilot(delPilot);
            SelectedPilot = null;
            return true;
        }
    }
}
