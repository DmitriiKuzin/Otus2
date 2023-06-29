using Microsoft.EntityFrameworkCore;
using Otus._2.DAL;
using Otus._2.Dto;
using Otus._2.Mapping;

namespace Otus._2.Services;

public class UserService
{
    private readonly UserContext _context;

    public UserService(UserContext context)
    {
        _context = context;
    }

    public async Task<int> CreateUser(CreateUserDto user)
    {
        var newUser = user.ToDbModel();
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return newUser.Id;
    }

    public async Task<int> UpdateUser(UpdateUserDto user)
    {
        var existingUser = await _context
            .Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if (existingUser == null) throw new Exception("Пользователь не найден");
        user.UpdateDbModel(existingUser);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<UserDto> GetUser(int userId)
    {
        var existingUser = await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (existingUser == null) throw new Exception("Пользователь не найден");
        return existingUser.ToDto();
    }

    public async Task DeleteUser(int userId)
    {
        var existingUser = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (existingUser == null) throw new Exception("Пользователь не найден");
        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
    }
}