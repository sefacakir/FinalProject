using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryDal : ICarDal
    {
        List<Car> cars;
        public InMemoryDal()
        {
            cars = new List<Car>()
            {
                new Car(){BrandId = 1,ColorId = 1,DailyPrice = 100,Description = "1. kalitede",Id = 1,ModelYear = 10},
                new Car(){BrandId = 2,ColorId = 2,DailyPrice = 200,Description = "2. kalitede",Id = 2,ModelYear = 10},
                new Car(){BrandId = 3,ColorId = 3,DailyPrice = 300,Description = "3. kalitede",Id = 3,ModelYear = 10},
                new Car(){BrandId = 4,ColorId = 4,DailyPrice = 400,Description = "4. kalitede",Id = 4,ModelYear = 10}
            };

        }

        public void Add(Car car)
        {
            cars.Add(car);   
        }

        public void Delete(Car car)
        {
            cars.Remove(car);
        }

        public List<Car> GetAll()
        {
            return cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return cars.SingleOrDefault(c => c.Id == id);
        }

        public void Update(Car car)
        {
            Car carToUpdate = cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.Id = car.Id;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;

        }
    }
}