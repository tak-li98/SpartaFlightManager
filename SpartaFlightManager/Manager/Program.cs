using System;
using Manager;
namespace Manager
{
    class Program
    {
        public static void Main(string[] args)
        { 
            FlightManager _flightManager = new FlightManager();
            //CREATE
            _flightManager.Create(Status.SCHEDULED, DateTime.Now.AddDays(3),"LHR");
            //READ
            //flightManager.RetrieveAll().ForEach(i => Console.WriteLine(i.ToString()));
            //UPDATE
            //_flightManager.Update(23, Status.DELAYED, DateTime.Now.AddDays(4));
            //DELETE
            //_flightManager.Delete(23);
        }
    }
}
