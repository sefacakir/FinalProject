using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car,DatabaseContext>, ICarDal
    {
        public void Add(Car car)
        {
            if (car.Description.Length > 1 && car.DailyPrice > 0)
            {
                base.Add(car);
            }
            else
            {
                Console.WriteLine("Sisteme araç ekleme başarısız. İstenen kriterlere uymuyor.");
            }

        }

        public List<CarDetailDto> GetCarDetail()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
                             select new CarDetailDto
                             {
                                 CarId = car.Id,
                                 BrandName = brand.Name,
                                 CarDescription = car.Description,
                                 ColorName = color.Name,
                                 DailyPrice = car.DailyPrice
                             };
                return result.ToList();
            }
        }
    }
}
/*
var addedEntity = context.Entry(car);
addedEntity.State = EntityState.Added;
context.SaveChanges();*/

