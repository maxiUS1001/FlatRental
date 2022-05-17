using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model
{
    public static class DataWorker
    {
        public static void AddFlat(string? metro, string? district, string? microdistrict, 
            int? numberOfRooms , string? rentalType, decimal? area, string? toilet, string? balcony, int? floor, 
            decimal? price, string? description, string? image)
        {
            Flat flat = new Flat();
            flat.Metro = metro;
            flat.District = district;
            flat.Мicrodistrict = microdistrict;
            flat.NumberOfRooms = numberOfRooms;
            flat.RentalType = rentalType;
            flat.Area = area;
            flat.Toilet = toilet;
            flat.Balcony = balcony;
            flat.Floor = floor;
            flat.Price = price;
            flat.Description = description;

            FLAT_RENTALContext.GetContext().Flats.Add(flat);
            FLAT_RENTALContext.GetContext().SaveChanges();
        }
    }
}
