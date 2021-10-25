using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
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
            ValidationTool.Validate(new CarValidator(), car);
            _carDal.Add(car);
            return new SuccessResult(Messages.SuccessAdd);


        }

        public IResult Update(Car car)
        {
            var result = _carDal.GetAll(c => c.Id == car.Id).SingleOrDefault();
            if (result != null)
            {
                _carDal.Update(car);
                return new SuccessResult(Messages.SuccessUpdate);
            }
            else
            {
                return new ErrorResult(Messages.kayitBulunamadi);
            }
        }

        public IResult Delete(Car car)
        {
            var result = _carDal.GetAll(c => c.Id == car.Id).SingleOrDefault();
            if (result != null)
            {
                _carDal.Delete(car);
                return new SuccessResult(Messages.SuccessDelete);
            }
            else
            {
                return new ErrorResult(Messages.kayitBulunamadi);
            }
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 0)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Success);
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.GetAll(c => c.Id == id).SingleOrDefault();
            if (result != null)
            {
                return new SuccessDataResult<Car>(_carDal.GetAll(c => c.Id == id).FirstOrDefault(), Messages.Success);
            }
            else
            {
                return new ErrorDataResult<Car>(Messages.kayitBulunamadi);
            }

        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var result = _carDal.GetCarDetail();
            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id).ToList());
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            var result = _carDal.GetAll(c => c.ColorId == id).ToList();
            if (result.Count != 0)
            {
                return new SuccessDataResult<List<Car>>(result);
            }
            else
            {
                return new ErrorDataResult<List<Car>>(Messages.kayitBulunamadi);
            }
        }

        public IDataResult<Car> Get(Car car)
        {
            var result = _carDal.GetAll(c => c.Id == car.Id).SingleOrDefault();
            if (result != null)
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
