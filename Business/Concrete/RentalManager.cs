using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Delete(Rental rental)
        {
            var result = _rentalDal.GetAll(c => c.Id == rental.Id).SingleOrDefault();
            if (result != null)
            {
                _rentalDal.Delete(rental);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }


        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var rentalDate = _rentalDal.GetAll(r => r.CarId == rental.CarId).LastOrDefault();
            if (rentalDate==null || (rentalDate.ReturnDate == null || rentalDate.ReturnDate < rental.RentDate))
            {
                _rentalDal.Add(rental);
                return new SuccessResult("Kiralama hizmeti ayarlandı.");
            }
            else
            {
                return new ErrorResult("İstediğiniz araç "+rentalDate.RentDate+ " tarihinde kiralanmış olup, "+rentalDate.ReturnDate+" tarihinde geri getirilecektir.");
            }
        }

        public IResult Update(Rental rental)
        {
            var result = _rentalDal.GetAll(c=> c.CarId == rental.CarId || c.CustomerId == rental.CustomerId);
            if (result != null)
            {
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.Success);
            }
            else
            {
                return new ErrorResult(Messages.Error);
            }
        }
    }
}
