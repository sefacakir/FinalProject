using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IResult Add(Rental rental)
        {
            var rentalDate = _rentalDal.GetAll(r => r.CarId == rental.CarId).LastOrDefault();
            if (rentalDate.ReturnDate == null || rentalDate.ReturnDate < rental.RentDate)
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
            _rentalDal.Update(rental);
            return new SuccessResult();
        }
    }
}
