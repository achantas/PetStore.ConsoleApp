using PetStore.ConsoleApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.ConsoleApp
{
    public interface IPetService
    {
        Task<IEnumerable<IGrouping<string, Pet>>> GetAllAvailablePetsByCategories();
    }

    public class PetService : IPetService
    {
        private readonly HttpClient _client;

        public PetService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<IEnumerable<IGrouping<string, Pet>>> GetAllAvailablePetsByCategories()
        {
            var pets = Enumerable.Empty<Pet>();
            var availablePetsPath = $"pet/findByStatus?status=available";

            HttpResponseMessage response = await _client.GetAsync(availablePetsPath).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                pets = await response.Content.ReadAsAsync<IEnumerable<Pet>>().ConfigureAwait(false);
            }

            return pets.GroupBy(x => x.Category?.Name);
        }
    }
}