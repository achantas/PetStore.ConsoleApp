using PetStore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var allAvailablePetsByCategory = Enumerable.Empty<IGrouping<string, Pet>>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://petstore.swagger.io/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var petService = new PetService(client);
                allAvailablePetsByCategory = await petService.GetAllAvailablePetsByCategories();
            }

            PrintPetNamesGroupedByCategory(allAvailablePetsByCategory);
        }
        
        private static void PrintPetNamesGroupedByCategory(IEnumerable<IGrouping<string, Pet>> allAvailablePetsByCategory)
        {
            foreach (var group in allAvailablePetsByCategory)
            {
                Console.WriteLine("Pets from category '" + group.Key + "':");
                foreach (var pet in group.OrderByDescending(x => x.Name))
                    Console.WriteLine(pet.Name);
            }
        }
    }
}