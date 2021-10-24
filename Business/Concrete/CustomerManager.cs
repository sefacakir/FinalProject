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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IResult Add(Customer customer)
        {
            if (customer.CompanyName.Length>0)
            {
                _customerDal.Add(customer);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IResult Delete(Customer customer)
        {
            var result = _customerDal.GetAll(c => c.Id == customer.Id).FirstOrDefault();
            if (result != null)
            {
                _customerDal.Delete(customer);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.GetAll(c => c.Id == id).SingleOrDefault();
            if (result != null)
            {
                return new SuccessDataResult<Customer>(result);
            }
            else
            {
                return new ErrorDataResult<Customer>(result);
            }
        }

        public IResult Update(Customer customer)
        {
            var result = _customerDal.GetAll(c => c.Id == customer.Id).SingleOrDefault();
            if (result != null)
            {
                _customerDal.Update(customer);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }
    }
}
