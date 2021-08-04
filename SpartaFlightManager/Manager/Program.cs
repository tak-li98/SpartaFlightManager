using System;
using Manager;
using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Manager
{
    class Program
    {
        public static void Main(string[] args)
        { 
            FlightManager _flightManager = new FlightManager();
            _flightManager.ReturnInfoFromFlight();
            //CREATE
            //_flightManager.Create(Status.SCHEDULED, DateTime.Now.AddDays(3),"LHR","DAL");
            //READ
            //flightManager.RetrieveAll().ForEach(i => Console.WriteLine(i.ToString()));
            //UPDATE
            //_flightManager.Update(23, Status.DELAYED, DateTime.Now.AddDays(4));
            //DELETE
            //using (var db = new SpartaFlightContext())
            //{
            //    foreach (var item in db.Flights)
            //    {
            //        if (item.FlightDate.ToShortDateString() == "07/08/2021")
            //        {
            //            _flightManager.Delete(item.FlightId);
            //        }
                    
            //    }
            //}
        }
    }
}
