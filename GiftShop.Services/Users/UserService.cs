using GiftShop.Common;
using GiftShop.DataAccess.Entities;
using GiftShop.DataAccess.UnitOfWork;
using GiftShop.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GiftShop.Services.Users
{
    public class UserService : BaseService
    {
        public UserService(GiftShopUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
        }

        public bool Register(User user)
        {
            var check = _unitOfWork.User.Query.FirstOrDefault(u => u.Email == user.Email);
            if (check != null)
                return false;

            user.Password = HashHelper.HashPassword(user.Password);
            _unitOfWork.User.Add(user);
            var role = _unitOfWork.Role.Query.Where(r => r.Id == 1).FirstOrDefault();
            _unitOfWork.UsersRole.Add(new UsersRole { UserId = user.Id, RoleId = 1, Role = role , User = user});

            return _unitOfWork.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var passwordHash = HashHelper.HashPassword(password);
            var user = _unitOfWork.User.Query.FirstOrDefault(u => u.Email == email);

            if(user == null)
                return null;

            string storedPassword = user.Password;

            if (HashHelper.VerifyPassword(password, storedPassword) == false)
            {
                return null;
            }

            return user;

        }

        public bool RemoveRole(User user)
        {
            var userRole = _unitOfWork.UsersRole.Query.FirstOrDefault(ur => ur.UserId == user.Id);
            _unitOfWork.UsersRole.Remove(userRole);

            return _unitOfWork.SaveChanges();
        }

        public bool ChangeRole(User user, int roleId)
        {
            var role = _unitOfWork.Role.Query.FirstOrDefault(r => r.Id == roleId);
            var userRole = new UsersRole { UserId = user.Id, RoleId = roleId, Role = role, User = user};
            _unitOfWork.UsersRole.Add(userRole);

            return _unitOfWork.SaveChanges();
        }

        public IEnumerable<UsersRole> getAllUsers()
        {
            return _unitOfWork.UsersRole.Query.Include(ur => ur.User).ToList();
        } 

        public bool CheckAdmin(string email)
        {
           
            var user = _unitOfWork.User.Query.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return false;
            var priv = _unitOfWork.UsersRole.Query.FirstOrDefault(ur => ur.UserId == user.Id);
            if (priv == null)
                return false;

            return priv.RoleId == 3;
        }

        public bool UpdateUserData(User user)
        {
            _unitOfWork.User.Update(user);
            return _unitOfWork.SaveChanges();
        }

        public User GetUsersByEmail(string email)
        {
            return _unitOfWork.User.Query.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(Guid id)
        {
            return _unitOfWork.User.Query.FirstOrDefault(u => u.Id == id);
        }
    }
}
