using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IResult Delete(User user)
        {
            var result = _userDal.GetAll(c => c.Id == user.Id).FirstOrDefault();
            if (result != null)
            {
                _userDal.Delete(user);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.GetAll(c => c.Id == id).FirstOrDefault();
            if (result != null)
            {
                return new SuccessDataResult<User>(result);
            }
            else
            {
                return new ErrorDataResult<User>(result);
            }
        }

        public IResult Update(User user)
        {
            var result = _userDal.GetAll(c => c.Id == user.Id).SingleOrDefault();
            if (result != null)
            {
                _userDal.Update(user);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }



        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
