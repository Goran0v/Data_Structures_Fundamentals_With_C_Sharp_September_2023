using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicTransportManagementSystem
{
    public class PublicTransportRepository : IPublicTransportRepository
    {
        private Dictionary<string, Passenger> passengersById = new Dictionary<string, Passenger>();
        private Dictionary<string, Bus> busesById = new Dictionary<string, Bus>();

        public void RegisterPassenger(Passenger passenger)
        {
            this.passengersById.Add(passenger.Id, passenger);
        }

        public void AddBus(Bus bus)
        {
            this.busesById.Add(bus.Id, bus);
        }

        public bool Contains(Passenger passenger)
        {
            return this.passengersById.ContainsKey(passenger.Id);
        }

        public bool Contains(Bus bus)
        {
            return this.busesById.ContainsKey(bus.Id);
        }

        public IEnumerable<Bus> GetBuses()
        {
            return this.busesById.Values;
        }

        public void BoardBus(Passenger passenger, Bus bus)
        {
            if (!this.passengersById.ContainsKey(passenger.Id) 
                || !this.busesById.ContainsKey(bus.Id) 
                || this.busesById[bus.Id].Passengers.Contains(passenger))
            {
                throw new ArgumentException();
            }

            this.busesById[bus.Id].Passengers.Add(passenger);
        }

        public void LeaveBus(Passenger passenger, Bus bus)
        {
            if (!this.passengersById.ContainsKey(passenger.Id) 
                || !this.busesById.ContainsKey(bus.Id) 
                || !this.busesById[bus.Id].Passengers.Contains(passenger))
            {
                throw new ArgumentException();
            }

            this.busesById[bus.Id].Passengers.Remove(passenger);
        }

        public IEnumerable<Passenger> GetPassengersOnBus(Bus bus)
        {
            if (!this.busesById.ContainsKey(bus.Id))
            {
                throw new ArgumentException();
            }

            return this.busesById[bus.Id].Passengers;
        }

        public IEnumerable<Bus> GetBusesOrderedByOccupancy()
        {
            return this.busesById.Values.OrderBy(b => b.Passengers.Count());
        }

        public IEnumerable<Bus> GetBusesWithCapacity(int capacity)
        {
            return this.busesById.Values.Where(b => b.Capacity >= capacity);
        }
    }
}