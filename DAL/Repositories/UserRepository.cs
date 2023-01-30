using DAL.Abstractions;
using DAL.Utility;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _db;

    public UserRepository(DatabaseContext db)
    {
        _db = db;
    }

    public List<User> GetAllUsers()
        => _db.Users.Include(u => u.Diaries).ToList();

    public User? Get(int id)
        => GetAllUsers().Find(u => u.Id == id);

    public void Create(string username, string password, string email)
    {
        var user = new User(username, password, email);

        // todo: hash password (sha vs bcrypto)
        var hashSumOfPassword = Enctyption.Sha256HashSumOf(password, user.Salt);
        user.Password = hashSumOfPassword;

        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public bool Update(int id, string? newName, string? newPassword, string? newEmail)
    {
        var user = Get(id);
        bool isUpdated = false;

        if (user is not null)
        {
            if (newName is not null)
            {
                user.Username = newName;
                isUpdated = true;
            }

            if (newPassword is not null)
            {
                user.Password = Enctyption.Sha256HashSumOf(newPassword, user.Salt);
                isUpdated = true;
            }

            if (newEmail is not null)
            {
                user.Email = newEmail;
                isUpdated = true;
            }

            if (isUpdated)
            {
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

        return isUpdated;
    }

    public bool Delete(int id)
    {
        bool isSuccess = false;

        var user = Get(id);
        if (user is not null)
        {
            _db.Users.Remove(user); // todo: where to check whether or not diary exists in DAL or BLL?
            _db.SaveChanges();
            isSuccess = true;
        }

        return isSuccess;
    }
}