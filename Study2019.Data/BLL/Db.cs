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
                    var dbUser = ctx.Users.Where(x => x.LoginName == Login || (id.HasValue && x.Id == id)).FirstOrDefault();
                    var user = AutoMapper.Mapper.Map<DTO.UserDTO>(dbUser);

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

                    AutoMapper.Mapper.Map(user, dbUser);
                    if (!user.RegDate.Equals(DateTime.MinValue))
                        dbUser.RegDate = user.RegDate;
                    if (user.ImageAvatarId is int imgId && !dbUser.Avatars.Any(x => x.ImageId == imgId))
                        dbUser.Avatars.Add(new DAL.Entities.Avatar { ImageId = imgId });

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

        public static DTO.ImageDTO GetImage(int id)
        {
            using (var ctx = new DataContext())
                return AutoMapper.Mapper.Map<DTO.ImageDTO>(ctx.Images.Find(id));
        }

        public static int CreateImage(DTO.ImageDTO image)
        {
            using (var ctx = new DataContext())
            {
                var dbImg = ctx.Images.Add(AutoMapper.Mapper.Map<DAL.Entities.Image>(image));
                ctx.SaveChanges();
                return dbImg.Id;
            }

        }

        public static int CreateUpdatePost(PostDTO post)
        {
            using (var ctx = new DataContext())
            {
                if (ctx.Posts.Find(post.Id) is DAL.Entities.Post dbPost)
                {
                    AutoMapper.Mapper.Map(post, dbPost);
                }
                else
                {
                    dbPost = ctx.Posts.Add(AutoMapper.Mapper.Map<DAL.Entities.Post>(post));
                }

                dbPost.PostImages.Clear();
                var i = 0;
                post.PostImages.Select(x => new DAL.Entities.PostImage()
                {
                    ImageId = x,
                    OrderNum = ++i
                }).ToList().ForEach(dbPost.PostImages.Add);

                ctx.SaveChanges();

                return dbPost.Id;
            }
        }

        public static PostDTO GetPost(int id)
        {
            using (var ctx = new DataContext())
                return AutoMapper.Mapper.Map<DTO.PostDTO>(ctx.Posts.Find(id));

        }

        public static List<PostDTO> GetPosts(int? lastId = null)
        {
            using (var ctx = new DataContext())
            {
                var posts = ctx.Posts.AsNoTracking().AsQueryable();
                if (lastId.HasValue)
                    posts = posts.Where(x => x.Id < lastId);


                 return posts.OrderByDescending(x=>x.Id).Take(3).Select(AutoMapper.Mapper.Map<DTO.PostDTO>)
                  .ToList();
                
                
            }
        }
    }
}
