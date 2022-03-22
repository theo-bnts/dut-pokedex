using System.Text.Json;
using System.Net;

namespace Pokedex
{
    internal static class Downloader
    {
        private static string APIBaseURL = "https://tmare.ndelpech.fr/tps/pokemons";

        /// <summary>
        /// Download the list of all Pokemons references.
        /// </summary>
        /// <returns>The list of all Pokemons references</returns>
        public static Reference[] GetReferences()
        {
            using var client = new WebClient();

            string json = client.DownloadString(APIBaseURL);
            return JsonSerializer.Deserialize<Reference[]>(json);
        }

        /// <summary>
        /// Download the data of a Pokemon id.
        /// </summary>
        /// <param name="id">The Pokemon id to download</param>
        /// <returns>The data of the Pokemon</returns>
        public static Data GetData(int id)
        {
            using var client = new WebClient();

            string json = client.DownloadString(APIBaseURL + '/' + id);
            return JsonSerializer.Deserialize<Data>(json);
        }
    }
}
