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
    public class EfCarDal : IEntityRepository<Car>, ICarDal
    {
        public void Add(Car entity)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                if(entity.Description.Length>1 && entity.DailyPrice > 0)
                {
                    var addedEntity = context.Entry(entity);
                    addedEntity.State = EntityState.Added;
                    context.SaveChanges();
                    Console.WriteLine("Sisteme yeni bir araç eklendi.");
                }
                else
                {
                    Console.WriteLine("Sisteme yeni araç ekleme işlemi başarısız.");
                }
            }
        }

        public void Delete(Car entity)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                if (filter == null)
                {
                    return context.Set<Car>().ToList();
                }
                else
                {
                    return context.Set<Car>().Where(filter).ToList();
                }
            }
        }

        public Car GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Car entity)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
