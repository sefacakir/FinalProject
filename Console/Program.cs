using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryDal());

            Car car1 = new Car { Id = 5, ModelYear = 11, DailyPrice = 500, ColorId = 3, BrandId = 2, Description = "Son eklenilen araç" };
            carManager.Add(car1);
            foreach (var car in carManager.GetAll()) 
            {
                Console.WriteLine(car.Id+"  "+ car.ColorId + "  "+car.BrandId+ "  "+car.DailyPrice+ "  "+car.ModelYear+ "  "+car.Description);
            }
            

        }
    }
}
