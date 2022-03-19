namespace Pokedex
{
    internal class Type
    {
        private string name;

        public string Name
        {
            get { return name; }
        }

        public Type(string name)
        {
            this.name = name.ToLower();
        }

        public List<Pokemon> GetPokemons()
        {
            return Pokemons
                .Get()
                .Where(p => p.Types.Contains(this.name))
                .ToList();
        }

        public List<Statistic> GetStatistics()
        {
            var statistics = this.GetPokemons()
                .SelectMany(p => p.Statistics)
                .ToList();

            var averageStatistics = new List<Statistic>();

            foreach (var statistic in statistics)
            {
                if (averageStatistics.Any(s => s.name == statistic.name))
                {
                    var averageStatistic = averageStatistics.Find(s => s.name == statistic.name);

                    averageStatistic.stat += statistic.stat;
                    averageStatistic.count++;
                }
                else
                {
                    averageStatistics.Add(statistic);
                }
            }

            foreach (var averageStatistic in averageStatistics)
            {
                averageStatistic.stat /= averageStatistic.count;
            }

            return averageStatistics;
        }

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
