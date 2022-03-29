namespace Pokedex
{
    internal class Type
    {
        private string name;

        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Type constructor.
        /// </summary>
        /// <param name="name">The name of the type</param>
        public Type(string name)
        {
            this.name = name.ToLower();
        }

        /// <summary>
        /// Get all Pokemons of this type.
        /// </summary>
        /// <returns>The list of Pokemons</returns>
        public List<Pokemon> GetPokemons()
        {
            return Pokemons
                .Get()
                .Where(p => p.Types.Contains(this.name))
                .ToList();
        }

        /// <summary>
        /// Get a random pokemon of this type and of a specific generation.
        /// </summary>
        /// <param name="generation">The generation of the Pokemon</param>
        /// <returns></returns>
        public Pokemon GetRandomPokemon(int generation)
        {
            var pokemons = GetPokemons().Where(p => p.Generation == generation).ToArray();

            var random = new Random();
            var index = random.Next(0, pokemons.Count());

            return pokemons[index];
        }

        /// <summary>
        /// Get all statistics for this type.
        /// </summary>
        /// <returns>The list of statistics</returns>
        public List<Statistic> GetStatistics()
        {
            var statistics = this.GetPokemons()
                .SelectMany(p =>
                    {
                        var pokemonStatistics = p.Statistics;
                        
                        // Add height and weight to the statistics
                        pokemonStatistics.Insert(0, new Statistic("height", p.Height));
                        pokemonStatistics.Insert(1, new Statistic("weight", p.Weight));
                        
                        return pokemonStatistics;
                    }
                )
                .ToList();

            var averageStatistics = new List<Statistic>();

            foreach (var statistic in statistics)
            {
                // If the statistic already exist in averageStatistics
                if (averageStatistics.Any(s => s.name == statistic.name))
                {
                    var averageStatistic = averageStatistics.Find(s => s.name == statistic.name);

                    // Add the current stat to the averageStatistic
                    averageStatistic.stat += statistic.stat;
                    averageStatistic.count++;
                }
                else
                {
                    // Add new statistic in averageStatistics
                    averageStatistics.Add(statistic);
                }
            }

            foreach (var averageStatistic in averageStatistics)
            {
                averageStatistic.stat /= averageStatistic.count;
            }

            return averageStatistics;
        }

        /// <summary>
        /// Get a list of all types.
        /// </summary>
        /// <returns>The list of types</returns>
        public static List<Type> GetAll()
        {
            var types = new List<Type>();

            var pokemons = Pokemons.Get();

            foreach (var pokemon in pokemons)
            {
                foreach (string typeName in pokemon.Types)
                {
                    if (!types.Any(t => t.name == typeName))
                    {
                        types.Add(
                            new Type(typeName)
                        );
                    }
                }
            }

            return types;
        }
    }
}
