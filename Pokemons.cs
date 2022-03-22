using System.Text.Json;
using System.Net;

namespace Pokedex
{
    internal static class Pokemons
    {
        private static List<Pokemon> pokemons;

        /// <summary>
        /// Dowload each pokemon from the API.
        /// </summary>
        public static void Download()
        {
            pokemons = new List<Pokemon>();

            var tasks = new List<Task>();

            for (int id = 1; id <= Generation.GetCount(); id++)
            {
                var generation = new Generation(id);

                var task = Task.Run(() =>
                    generation
                        .GetPokemons()
                        .ForEach(pokemons.Add)
                 );

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            SortById();

            Track();
        }

        /// <summary>
        /// Sort the list of pokemons by id.
        /// </summary>
        public static void SortById()
        {
            pokemons.Sort((p1, p2) => p1.Id.CompareTo(p2.Id));
        }

        /// <summary>
        /// Track possible changements in the Pokemons data.
        /// </summary>
        private static void Track()
        {
            var thread = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(TimeSpan.FromMinutes(1));

                        var references = Downloader.GetReferences();

                        foreach (var reference in references)
                        {
                            int pokemonIndex = pokemons.FindIndex(p => p.Id == reference.id);

                            if (pokemons[pokemonIndex].LastEdit < reference.lastEdit)
                            {
                                pokemons[pokemonIndex] = new Pokemon(reference.id);
                            }
                        }
                    }
                }
            );

            thread.Start();
        }

        /// <summary>
        /// Return if a pokemon id exists or not.
        /// </summary>
        /// <param name="id">The pokemon id</param>
        /// <returns>If the pokemon id exists</returns>
        public static bool Exists(int id)
        {
            return pokemons.Exists(p => p.Id == id);
        }

        /// <summary>
        /// Get the list of Pokemons.
        /// </summary>
        /// <returns>The list of Pokemons</returns>
        public static List<Pokemon> Get()
        {
            return pokemons;
        }
    }
}
