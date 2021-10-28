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
        //    CarManager carManager = new CarManager(new EfCarDal(),new );
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            Rental rental1 = new Rental()
            {
                Id = 9,
                CarId = 1,
                CustomerId = 4,
                RentDate = new DateTime(2021,10,23,19,54,05,043),
                ReturnDate = new DateTime(2021, 10, 24, 12, 0, 0)
            };
            Console.WriteLine(rentalManager.Update(rental1).Message);


            /*foreach (var item in rentalManager.GetAll().Data)
            {
                Console.WriteLine(item.Id+" " + item.CarId+" "+item.CustomerId+" "+item.RentDate+" " + item.ReturnDate);    
            }*/







            //GetAll(carManager);
            //GetById(carManager);
            //CrudOperations(carManager);

            /*CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetAll();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.Description);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }*/

            //BrandManager brandManager = new BrandManager(new EfBrandDal());
            //GetAll(brandManager);
            //GetById(brandManager);
            //CrudOperations(brandManager);

            //ColorManager colorManager = new ColorManager(new EfColorDal());
            //GetAll(colorManager);
            //GetById(colorManager);
            //CrudOperations(colorManager);
        }














        private static void CrudOperations(ColorManager carManager)
        {
            Color car = new Color()
            {
                Name = "Deneme Modeli"
            };
            //GetAll(carManager);
            //Console.WriteLine("\nEkleme işleminden sonra\n");
            //carManager.Add(car);
            //Console.WriteLine("\n Ekleme Başarılı. \n");

            GetAll(carManager);
            car.Id = 1002;
            carManager.Update(car);
            Console.WriteLine("\n Güncelleme Başarılı. \n");
            GetAll(carManager);

            Console.WriteLine("\n Ekleme Başarılı. \n");
            carManager.Delete(car);
            GetAll(carManager);

        }
        private static void GetAll(ColorManager colorManager)
        {
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.Id + "  " + color.Name);
            }
        }
        private static void GetById(ColorManager colorManager)
        {
            Console.WriteLine(colorManager.GetById(4).Data.Name);
        }
        private static void GetById(BrandManager brandManager)
        {
            Console.WriteLine(brandManager.GetById(5).Data.Name);
        }
        private static void GetAll(BrandManager brandManager)
        {
            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine(brand.Id + "  " + brand.Name);
            }
        }
        private static void CrudOperations(BrandManager carManager)
        {
            Brand car = new Brand()
            {
                Name = "Deneme Modelim",
            };
            //carManager.Add(car);
            //GetAll(carManager);
            //Console.WriteLine("\n Ekleme Başarılı. \n");
            car.Id = 11;
            carManager.Update(car);
            Console.WriteLine("\n Güncelleme Başarılı. \n");
            GetAll(carManager);
            carManager.Delete(car);
            GetAll(carManager);

        }
        private static void CrudOperations(CarManager carManager)
        {
            Car car = new Car()
            {
                Id = 12,
                BrandId = 5,
                ColorId = 5,
                Description = "Bu aracın bilgileri güncellenmiştir.",
                DailyPrice = 235,
                ModelYear = 30
            };
            carManager.Add(car);
            carManager.Delete(car);
            carManager.Update(car);
        }
        private static void GetById(CarManager carManager)
        {
            //Console.WriteLine(carManager.GetById(3).Description);
        }
        private static void GetAll(CarManager carManager)
        {
            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.Id + "  " + car.Description);
            }
        }
    }
}
