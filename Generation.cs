namespace Pokedex
{
    internal class Generation
    {
        private int id;

        /// <summary>
        /// Generation constructor.
        /// </summary>
        /// <param name="id">The generation id</param>
        public Generation(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Get the range of the genration.
        /// </summary>
        /// <returns>The range of the genration</returns>
        private int[] GetRange()
        {
            var range = new int[2];

            var ranges = GetRanges();

            for (int i = 1; i <= GetCount(); i++)
            {
                if (id == i)
                {
                    range[0] = ranges[i - 1, 0];
                    range[1] = ranges[i - 1, 1];
                }
            }

            return range;
        }

        /// <summary>
        /// Return if the generation pokemons are fully dowloaded.
        /// </summary>
        /// <returns>If the generation pokemons are fully dowloaded</returns>
        public bool IsDowloaded()
        {
            bool flag = true;

            var range = this.GetRange();

            for (int i = range[0]; i <= range[1]; i++)
            {
                if (!Pokemons.Exists(i))
                {
                    flag = false;
                }
            }

            return flag;
        }

        /// <summary>
        /// Get all Pokemons of this generation.
        /// </summary>
        /// <returns>The Pokemons of this generation</returns>
        public List<Pokemon> GetPokemons()
        {
            if (this.IsDowloaded())
            {
                return Pokemons
                    .Get()
                    .Where(p => p.Generation == this.id)
                    .ToList();
            }
            else
            {
                var pokemons = new List<Pokemon>();

                var range = this.GetRange();

                for (int i = range[0]; i <= range[1]; i++)
                {
                    pokemons.Add(new Pokemon(i));
                }

                return pokemons;
            }
        }

        /// <summary>
        /// Get the range of each generation.
        /// </summary>
        /// <returns>The ranges</returns>
        private static int[,] GetRanges()
        {
            return new int[,] {
                {   1, 151 },
                { 152, 251 },
                { 252, 386 },
                { 387, 493 },
                { 494, 649 },
                { 650, 721 },
                { 722, 802 },
                { 803, 898 }
            };
        }

        /// <summary>
        /// Get the generations count.
        /// </summary>
        /// <returns>The generations count</returns>
        public static int GetCount()
        {
            return GetRanges().GetLength(0);
        }

        /// <summary>
        /// Get the generation of a Pokemon.
        /// </summary>
        /// <param name="id">The Pokemon id</param>
        /// <returns>The generation id</returns>
        public static int Get(int id)
        {
            int generation = -1;

            var ranges = GetRanges();

            for (int i = 1; i <= GetCount(); i++)
            {
                if (id >= ranges[i - 1, 0] && id <= ranges[i - 1, 1])
                {
                    generation = i;
                }
            }

            return generation;
        }
    }
}