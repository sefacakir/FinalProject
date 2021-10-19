using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            //GetCarsByBrandId(carManager);
            //GetAll(carManager);
            //GetCarsByColorId(carManager);

            carManager.Add(new Car {BrandId=1,ColorId=1,DailyPrice=1000,ModelYear=12,Description="10 numara araç" });
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }
        }
        private static void GetCarsByColorId(CarManager carManager)
        {
            foreach (var car in carManager.GetCarsByColorId(1))
            {
                Console.WriteLine(car.Id + "  " + car.ColorId + "  " + car.BrandId + "  " + car.DailyPrice + "  " + car.ModelYear + "  " + car.Description);
            }
        }

        private static void GetCarsByBrandId(CarManager carManager)
        {
            foreach (var car in carManager.GetCarsByBrandId(1))
            {
                Console.WriteLine(car.Id + "  " + car.ColorId + "  " + car.BrandId + "  " + car.DailyPrice + "  " + car.ModelYear + "  " + car.Description);
            }
        }

        private static void GetAll(CarManager carManager)
        {
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Id + "  " + car.ColorId + "  " + car.BrandId + "  " + car.DailyPrice + "  " + car.ModelYear + "  " + car.Description);
            }
        }
    }
}
