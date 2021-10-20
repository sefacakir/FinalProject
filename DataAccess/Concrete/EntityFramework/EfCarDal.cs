using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
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
    }
}
/*
var addedEntity = context.Entry(car);
addedEntity.State = EntityState.Added;
context.SaveChanges();*/

