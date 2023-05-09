using Newtonsoft.Json;
using ClientAPI.API;

namespace ClientAPI
{
    public class Program
    {
        public static async Task Main()
        {
            ClienteServer client = new ClienteServer("http://localhost:5072",  MediaType.ApplicationJson);

            RouteAPI<User> routeApi = new RouteAPI<User>(client, "User");

            // basta fazer as chamadas CRUD usando a instancia routeApi, se tudo estiver correto você 
            var result = await routeApi.GetAsync();

            Thread.Sleep(1000);

            result.ForEach(user => Console.WriteLine(user));





        }
    }
}