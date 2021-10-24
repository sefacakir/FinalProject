using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {
            if (car.Description.Length < 2 || car.DailyPrice < 1)
            {
                return new ErrorResult();
            }
            else
            {
                _carDal.Add(car);
                return new SuccessResult();
            }

        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            if(DateTime.Now.Hour == 0)
            {
                return new ErrorDataResult<List<Car>>("Sistem bakımda.");
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),"Ürünler veritabanından çekildi.");
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.GetAll(c => c.Id == id).FirstOrDefault());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var result = _carDal.GetCarDetail();
            return new ErrorDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id).ToList());
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id).ToList());
        }

        public IDataResult<Car> Get(Car car)
        {
            var result = _carDal.GetAll(c => c.Id == car.Id).SingleOrDefault();
            if (result!=null)
            {
            return new SuccessDataResult<Car>(_carDal.GetAll(c => c.Id == car.Id).FirstOrDefault());
            }
            else
            {
                return new ErrorDataResult<Car>("Kayıt bulunamadı.");
            }
        }
    }
}
