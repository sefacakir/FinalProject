using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<Brand> GetById(int id);
        IDataResult<List<Brand>> GetAll();
        IResult Add(Brand color);
        IResult Delete(Brand color);
        IResult Update(Brand color);
    }
}
