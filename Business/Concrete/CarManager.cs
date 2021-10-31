using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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
        IBrandService _brandService;
        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _brandService = brandService;
            _carDal = carDal;
        }


        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            var result = BusinessRules.Run(
                CheckIfCarCountOfBrandCorrect(car.BrandId),
                CheckIfCarDescriptionExists(car.Description),
                CheckIfCategoryLimitExceded()
            );
            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult(Messages.SuccessAdd);
        }

        [ValidationAspect(typeof(CarValidator))]
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
                return new ErrorResult(Messages.NotFound);
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
                return new ErrorResult(Messages.NotFound);
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
                return new SuccessDataResult<Car>(result, Messages.Success);
            }
            else
            {
                return new ErrorDataResult<Car>(Messages.NotFound);
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
                return new ErrorDataResult<List<Car>>(Messages.NotFound);
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

        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result <= 10)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarCountOfBrandError);
        }

        private IResult CheckIfCarDescriptionExists(string description)
        {
            var result = _carDal.GetAll(c => c.Description == description).Any();
            if (result)
            {
                return new ErrorResult(Messages.ControlOfName); 
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _brandService.GetAll();
            if (result.Data.Count > 12)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
