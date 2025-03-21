using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        private Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
        private Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

        public void AddAirline(Airline airline)
        {
            this.airlines.Add(airline.Id, airline);
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!this.airlines.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }

            this.airlines[airline.Id].Flights.Add(flight);
            this.flights.Add(flight.Id, flight);
        }

        public bool Contains(Airline airline)
        {
            return this.airlines.ContainsKey(airline.Id);
        }

        public bool Contains(Flight flight)
        {
            return this.flights.ContainsKey(flight.Id);
        }

        public void DeleteAirline(Airline airline)
        {
            if (!this.airlines.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }

            foreach (var flight in this.airlines[airline.Id].Flights)
            {
                this.flights.Remove(flight.Id);
            }

            this.airlines.Remove(airline.Id);
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        {
            return this.airlines
                .Values
                .OrderByDescending(a => a.Rating)
                .ThenByDescending(a => a.Flights.Count())
                .ThenBy(a => a.Name);
        }

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
        {
            return this.airlines
                .Values
                .Where(a => a.Flights
                .Any(f => f.Origin == origin 
                && f.Destination == destination
                && !f.IsCompleted));
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return this.flights.Values;
        }

        public IEnumerable<Flight> GetCompletedFlights()
        {
            return this.flights.Values.Where(f => f.IsCompleted);
        }

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
        {
            return this.flights
                .Values
                .OrderBy(f => f.IsCompleted)
                .ThenBy(f => f.Number);
        }

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (!this.flights.ContainsKey(flight.Id) || !this.airlines.ContainsKey(airline.Id)) 
            {
                throw new ArgumentNullException();
            }

            this.flights[flight.Id].IsCompleted = true;
            return this.flights[flight.Id];
        }
    }
}
