using Model;
using System;
using System.Threading.Tasks;
using ClientModel = Model.Entities.Client;

namespace Client
{
    class ClientService
    {
        public static ClientModel CurrentClient { get; private set; }

        private async static Task<ClientModel> FindClient(string name)
        {
            using (var repository = new LibraryRepository())
            {
                return await repository.FindClient(name);
            }
        }

        private static ClientModel CreateClient(string clientName)
        {
            using (var repository = new LibraryRepository())
            {
                return repository.CreateClient(clientName);
            }
        }

        public async static void Login()
        {
            Console.WriteLine("Enter your name: ");
            var clientName = Console.ReadLine();
            var client = FindClient(clientName);
            if (client != null)
                CurrentClient = await client;
            else
                CurrentClient = CreateClient(clientName);
        }
    }
}
