using Model;
using System;

using ClientModel = Model.Entities.Client;

namespace Client
{
    class ClientService
    {
        public static ClientModel CurrentClient { get; private set; }

        private static ClientModel FindClient(string name)
        {
            using (var repository = new LibraryRepository())
            {
                return repository.FindClient(name);
            }
        }

        private static ClientModel CreateClient(string clientName)
        {
            using (var repository = new LibraryRepository())
            {
                return repository.CreateClient(clientName);
            }
        }

        public static void Login()
        {
            Console.WriteLine("Enter your name: ");
            var clientName = Console.ReadLine();
            var client = FindClient(clientName);
            if (client != null)
                CurrentClient = client;
            else
                CurrentClient = CreateClient(clientName);
        }
    }
}
