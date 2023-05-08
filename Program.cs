using ClientAPI.API;

namespace ClientAPI
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            ClienteServer client = new ClienteServer("http://localhost:5072",  MediaType.ApplicationJson);

            ConnectionAPI<User> connection = new ConnectionAPI<User>(client, "UserController");

            // basta fazer as chamadas CRUD usando a instancia connection, se tudo estiver correto você 

            // EX...
            var user = new User
            {
                Id = 0,
                UserName = "TylerDurder",
                Email = "TylerDuder@mail.com",
                Password = "#Tylerduder021"
            };
            
            await connection.CreateAsync(user);
        }
    }
}