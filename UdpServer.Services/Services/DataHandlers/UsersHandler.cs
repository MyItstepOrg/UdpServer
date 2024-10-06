using System.Net;
using UdpServer.Core.Data.Dto;
using UdpServer.Services.Services.DataAccess;

namespace UdpServer.Services.Services.DataHandlers;
public class UsersHandler(UsersService users, ChatService chats)
{
    private readonly UsersService users = users;
    private readonly ChatService chats = chats;
    public void RegUser(int id, IPEndPoint ip)
    {
        UserDto newUser = new()
        {
            Id = id,
            Username = "newUser"
        };
        if (users.Get(id) is null)
        {
            users.Add(newUser);
            var mainChat = chats.Find(c => c.Name == "Main");
            mainChat.UsersList.Add(newUser);
            chats.SaveChanges();
            users.SaveChanges();
            Console.WriteLine($"New user {newUser.Username + "#" + newUser.Id} registrated!");
        }
        else
        {
            users.SaveChanges();
            Console.WriteLine($"{users.Get(id)} connected");
        }
    }
}
