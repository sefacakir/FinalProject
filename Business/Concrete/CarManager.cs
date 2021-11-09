using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
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


        [SecuredOperation("car.add")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            var result = BusinessRules.Run(
                CheckIfCarCountOfBrandCorrect(car.BrandId),
                CheckIfCarDescriptionExists(car.Description),
                CheckIfCategoryLimitExceded()
            );
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            _carDal.Add(car);
            return new SuccessResult(Messages.SuccessAdd);
        }


        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            var result = _carDal.Get(c => c.Id == car.Id);
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


        [SecuredOperation("admin")]
        public IResult Delete(Car car)
        {
            var result = _carDal.Get(c => c.Id == car.Id);
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

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<CarDetailDto>> GetAll()
        {
            if (DateTime.Now.Hour == 0)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }
            var result = _carDal.GetCarDetail();
            return new SuccessDataResult<List<CarDetailDto>>(result, Messages.Success);
        }


        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
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


        public IDataResult<List<CarDetailDto>> GetCarsByBrandId(int id)
        {
            var temp = _brandService.GetById(id);
            if (temp == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.NotFound);
            }
            var temp2 = _brandService.GetById(id);
            var result = _carDal.GetCarDetail(c=> c.BrandName == temp2.Data.Name).ToList();
            
            return new SuccessDataResult<List<CarDetailDto>>(result);
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
