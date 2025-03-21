using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        private Dictionary<string, Deliverer> deliverersById = new Dictionary<string, Deliverer>();
        private Dictionary<string, Package> packagesById = new Dictionary<string, Package>();
        private Dictionary<string, string> packagesByDeliverer = new Dictionary<string, string>();

        public void AddDeliverer(Deliverer deliverer)
        {
            this.deliverersById.Add(deliverer.Id, deliverer);
        }

        public void AddPackage(Package package)
        {

            this.packagesById.Add(package.Id, package);
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if (!this.deliverersById.ContainsKey(deliverer.Id) || !this.packagesById.ContainsKey(package.Id))
            {
                throw new ArgumentException();
            }

            packagesByDeliverer.Add(package.Id, deliverer.Id);
        }

        public bool Contains(Deliverer deliverer)
        {
            return this.deliverersById.ContainsKey(deliverer.Id);
        }

        public bool Contains(Package package)
        {
            return this.packagesById.ContainsKey(package.Id);
        }

        public IEnumerable<Deliverer> GetDeliverers()
        {
            return this.deliverersById.Values;
        }

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
        {
            var deliverersByPackageId = new Dictionary<string, int>();

            foreach (var package in this.packagesByDeliverer)
            {
                if (!deliverersByPackageId.ContainsKey(package.Value))
                {
                    deliverersByPackageId.Add(package.Value, 0);
                }

                deliverersByPackageId[package.Value] += 1;
            }

            return deliverersByPackageId
                .OrderByDescending(d => d.Value)
                .ThenBy(d => this.deliverersById[d.Key].Name)
                .Select(d => this.deliverersById[d.Key]);
        }

        public IEnumerable<Package> GetPackages()
        {
            return this.packagesById.Values;
        }

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver()
        {
            return packagesById.Values
                .OrderByDescending(p => p.Weight)
                .ThenBy(p => p.Receiver);
        }

        public IEnumerable<Package> GetUnassignedPackages()
        {
            return packagesById.Where(p => !this.packagesByDeliverer.ContainsKey(p.Key)).Select(p => p.Value);
        }
    }
}
