using Domain.entities.Cart;
using Domain.entities.UserPart.User;


namespace Domain.IRepository.UserPart
{
    public interface IAccountRepository
    {
        Task<User?> FindUserSignIn(User user);
        Task SaveChanges();
        Task AddToDataBase(User user, CancellationToken cancellation);
        bool IsExist(string PhoneNumbers, string email);
        Task<User?> GetUserByIdAsync(int id);
        void Update(User user);

        bool SuperAdmin(int id);
      

        #region Admin Side
        int CountUsers();
        int CountAdmins();
        Task<List<User>> GetUsersAsync();
        Task<List<User>> ListOfAdmins();
        void UpdateByAdmin(User user);
        User? Finduser(int id);
        void DeleteUserAvatar(User user);
        Task DeleteUser(int userid);
        #endregion
    }
}
