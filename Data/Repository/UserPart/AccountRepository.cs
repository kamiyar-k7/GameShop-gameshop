using Data.ShopDbcontext;
using Domain.entities.Cart;
using Domain.entities.UserPart.User;
using Domain.IRepository.UserPart;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository.UserPart;

public class AccountRepository : IAccountRepository
{
    #region Ctor

    private readonly GameShopDbContext _dbContext;
    public AccountRepository(GameShopDbContext gameShopDb)
    {
        _dbContext = gameShopDb;
    }
    #endregion

    #region General

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
    public async Task<User?> FindUserSignIn(User user)
    {
        return await _dbContext.Users.Where(x => x.IsDelete == false).FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
    }

    public async Task AddToDataBase(User user, CancellationToken cancellation)
    {
        await _dbContext.AddAsync(user);
    }

    public bool IsExist(string PhoneNumber, string email)
    {
        return _dbContext.Users.Where(x => x.IsDelete == false).Any(x => x.PhoneNumber == PhoneNumber || x.Email == email);

    }
 

    public async Task<User?> GetUserByIdAsync(int id)
    {

        return await _dbContext.Users.Include(x => x.cart).Include(x => x.Comments).Where(x => x.Id == id).Select(x => new User
        {
            Id = x.Id,
            cart = x.cart,
            Comments = x.Comments,
            Created = x.Created,
            Email = x.Email,
            UserAvatar = x.UserAvatar,
            UserName = x.UserName,



        }).FirstOrDefaultAsync();
    }

    public void Update(User user)
    {
        _dbContext.Attach(user);

        _dbContext.Entry(user).Property(u => u.UserName).IsModified = true;

        _dbContext.Entry(user).Property(u => u.UserAvatar).IsModified = true;



    }

    public bool SuperAdmin(int id)
    {
        if (id != null)
        {

            var user = _dbContext.Users.Find(id).SuperAdmin;
            if (user)
            {

                return user;
            }
        }
        return false;

    }
    #endregion


    //------------------------------------
    #region Admin side

    #region Dashboard
    public int CountUsers()
    {
        return _dbContext.Users.Where(x => x.IsDelete == false).Count();
    }
    public int CountAdmins()
    {
        var adminRoleId = _dbContext.Roles.FirstOrDefault(r => r.RoleUniqueName == "Admin")?.Id;

        // Count users where IsDelete is false and the associated role is "Admin"
        var count = _dbContext.SelectedRole
            .Count(ur => ur.RoleId == adminRoleId && !ur.User.IsDelete);

        return count;
    }
    #endregion


    #region Users
    public async Task<List<User>> GetUsersAsync()
    {
        return await _dbContext.Users.Include(x => x.Comments).Include(x => x.cart).Include(x => x.UserSelectedRoles).Where(x => x.IsDelete == false).Select(x => new User()
        {
            Id = x.Id,
            UserName = x.UserName,
            Email = x.Email,
            UserAvatar = x.UserAvatar,
            UserSelectedRoles = x.UserSelectedRoles,
            cart = x.cart,
            Comments = x.Comments,
            Created = x.Created,
        }).ToListAsync();
    }

    public User? Finduser(int id)
    {
        return _dbContext.Users.Find(id);
    }
    public void UpdateByAdmin(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
    }
    public void DeleteUserAvatar(User user)
    {

        user.UserAvatar = null;
        UpdateByAdmin(user);
    }
    public async Task DeleteUser(int userid)
    {

        var user = Finduser(userid);
        user.IsDelete = true;
        UpdateByAdmin(user);



    }

    #endregion




    #region Admins

    public async Task<List<User>> ListOfAdmins()
    {
        return await _dbContext.Users.Where(x => x.IsDelete == false && x.UserSelectedRoles.Any(x => x.Role.RoleUniqueName == "Admin")).Select(x => new User()
        {
            Created = x.Created,
            Id = x.Id,
            Email = x.Email,
            UserAvatar = x.UserAvatar,
            UserName = x.UserName,


        }).ToListAsync();
    }

    #endregion

    #endregion
}



