using Study2019.Data.DAL;
using Study2019.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.Data.BLL
{
    public static class Db
    {
        public static bool ValidateUser(string login, string password)
        {
            var user = GetUser(login);
            if (user != null)
            {
                var salt = Convert.FromBase64String(user.Salt);
                var passhash = Hash.GenerateSaltedHash(password, salt);
                var oldHash = Convert.FromBase64String(user.PasswordHash);
                return Hash.CompareByteArrays(passhash, oldHash);
            }
            return false;
        }

        public static UserDTO GetUser(string Login, int? id = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(Login))
                return null;
            try
            {
                using (var ctx = new DataContext())
                {
                    var user = ctx.Users.Where(x => x.Id == id || x.LoginName == Login).Select(x => new UserDTO
                    {
                        Birtdate = x.BirtDate,
                        Description = x.Description,
                        Id = x.Id,
                        LoginName = x.LoginName,
                        Nickname = x.Nickname,
                        PasswordHash = x.PasswordHash,
                        RegDate = x.RegDate,
                        Salt = x.Salt,
                        SharedProfile = x.IsShared

                    }).FirstOrDefault();

                    if (user != null)
                        return user;

                    throw new Exception($"Not found User with ID:{id}");
                }
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }


        }

        public static int CreateUpdateUser(UserDTO user)
        {
            try
            {
                using (var ctx = new DataContext())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.Id == user.Id) ?? ctx.Users.Add(new Data.DAL.Entities.User());

                    if (ctx.Users.Any(x => x.LoginName == user.LoginName && x.Id != dbUser.Id))
                        throw new Exception($"User with loginName :{user.LoginName} exist");

                    dbUser.BirtDate = user.Birtdate;
                    dbUser.Description = user.Description;
                    dbUser.Id = user.Id;
                    dbUser.LoginName = user.LoginName;
                    dbUser.Nickname = user.Nickname;
                    dbUser.PasswordHash = user.PasswordHash;
                    dbUser.RegDate = user.RegDate;
                    dbUser.Salt = user.Salt;
                    dbUser.IsShared = user.SharedProfile;




                    ctx.SaveChanges();

                    return dbUser.Id;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }


    }
}
