using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }
        public IResult Add(Color color)
        {
            var result = _colorDal.GetAll(c => c.Name == color.Name);
            if (result == null)
            {
                _colorDal.Add(color);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IResult Delete(Color color)
        {
            var result = _colorDal.GetAll(c => c.Id == color.Id).SingleOrDefault();
            if (result != null)
            {
                _colorDal.Delete(color);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IDataResult<Color> GetById(int id)
        {
            var result = _colorDal.GetAll(c=> c.Id == id).SingleOrDefault();
            if(result != null)
            {
                return new SuccessDataResult<Color>(result);
            }
            else
            {
                return new ErrorDataResult<Color>(result);
            }
        }

        public IResult Update(Color color)
        {
            var result = _colorDal.GetAll(c => c.Id == color.Id).SingleOrDefault();
            if (result != null)
            {
                _colorDal.Update(color);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }
    }
}
