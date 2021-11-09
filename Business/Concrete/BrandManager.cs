using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IDataResult<Brand> GetById(int id)
        {
            var result = _brandDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Brand>(result, Messages.Success);
            }
            else
            {
                return new ErrorDataResult<Brand>(result, Messages.NotFound);
            }
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            var result = _brandDal.GetAll(c => c.Name == brand.Name).SingleOrDefault();
            if (result == null)
            {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.Success);
            }
            else
            {
                return new ErrorResult(Messages.Error);
            }
        }

        public IResult Delete(Brand brand)
        {
            var result = _brandDal.GetAll(c => c.Id == brand.Id);
            if (result != null)
            {
                _brandDal.Delete(brand);
                return new SuccessResult(Messages.Success);
            }
            else
            {
                return new ErrorResult(Messages.Error);
            }

        }

        public IDataResult<List<Brand>> GetAll()
        {
            var result = _brandDal.GetAll();
            if (result.Count != 0)
            {
                return new SuccessDataResult<List<Brand>>(result, Messages.Success);
            }
            else
            {
                return new ErrorDataResult<List<Brand>>(Messages.Error);
            }
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            var result = _brandDal.GetAll(c => c.Id == brand.Id).SingleOrDefault();
            if (result != null)
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.Success);

            }
            else
            {
                return new ErrorResult(Messages.Error);
            }
        }
    }
}
