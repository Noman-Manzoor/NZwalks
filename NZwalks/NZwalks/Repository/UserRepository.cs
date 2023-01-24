using Microsoft.EntityFrameworkCore;
using NZwalks.Data;
using NZwalks.Models.Domain;

namespace NZwalks.Repository
{
    public class UserRepository : IUserRepository
    {
        public NZwalksDbContext _nZwalksDbContext { get; }

        public UserRepository(NZwalksDbContext nZwalksDbContext)
        {
            _nZwalksDbContext = nZwalksDbContext;
        }

        //private List<User> users = new List<User>()
        //   {
        // new User()
        // {
        //      Id = Guid.NewGuid(),
        //      Email = "basicuser@gmail.com",
        //      FirstName = "Noman",
        //      LastName = "manzoor",
        //      Password = "12",
        //      UserName = "BasicUser",
        //      Roles = new List<string>{"basic"}
        // },
        //new User()
        // {
        //      Id = Guid.NewGuid(),
        //      Email = "adminuser@gmail.com",
        //      FirstName = "usman",
        //      LastName = "manzoor",
        //      Password = "34",
        //      UserName = "AdminUser",
        //      Roles = new List<string>{ "Admin" }
        // },
        //};


        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await _nZwalksDbContext.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Trim() == username.ToLower().Trim() && x.Password == password);


            var userRoles = await _nZwalksDbContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await _nZwalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    user.Roles.Add(role.Name);
                }
            }
            user.Password = null;
            return user;
        }
    }
}

