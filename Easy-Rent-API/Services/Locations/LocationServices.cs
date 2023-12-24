﻿using Easy_Rent_API.Context;
using Easy_Rent_API.DTO.Locations;
using Easy_Rent_API.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Easy_Rent_API.Services.Locations
{
    public class LocationServices : ILocationsServices

    {
        private readonly EasyRentContext _context;

        public LocationServices(EasyRentContext context)
        {
            _context = context;
        }
        void ILocationsServices.AddLocation(InsertLocation model)
        {
            Location location = new Location();
            location.region = model.region;
            location.country = model.country;
            location.postalCode = model.postalCode;
            location.city = model.city; 
            location.streetName = model.streetName;

            _context.Add(location);
            _context.SaveChanges();
           
        }

        IEnumerable ILocationsServices.GetAllLocations()
        {
            return _context.locations.ToList();
        }

        Location ILocationsServices.GetLocationById(ulong id)
        {
            Location found = _context.locations.FirstOrDefault(l => l.Id == id);
            if (found == null) throw new Exception($"Location with id{id} not found");

            return found;
        }

        void ILocationsServices.RemoveLocation(ulong id)
        {
            Location found = _context.locations.FirstOrDefault(l => l.Id == id);
            if (found == null) throw new Exception($"Location with id{id} not found");

            _context.locations.Remove(found);
            _context.SaveChanges();
        }

        void ILocationsServices.UpdateLocation(ulong id, InsertLocation model)
        {
            Location found = _context.locations.FirstOrDefault(l => l.Id == id);
            if (found == null) throw new Exception($"Location with id{id} not found");

            found.city = model.city;
            found.streetName = model.streetName;
            found.country = model.country;
            found.region = model.region;
            found.postalCode = model.postalCode;

            _context.Update(found);
            _context.SaveChanges();
        }
    }
}
