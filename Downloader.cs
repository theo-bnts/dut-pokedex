using System.Text.Json;
using System.Net;

namespace Pokedex
{
    internal static class Downloader
    {
        private static string APIBaseURL = "https://tmare.ndelpech.fr/tps/pokemons";
        
        public static Reference[] GetReferences()
        {
            using var client = new WebClient();

            string json = client.DownloadString(APIBaseURL);
            return JsonSerializer.Deserialize<Reference[]>(json);
        }

        public static Data GetData(int id)
        {
            using var client = new WebClient();

            string json = client.DownloadString(APIBaseURL + '/' + id);
            return JsonSerializer.Deserialize<Data>(json);
        }
    }
}
