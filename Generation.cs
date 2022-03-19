namespace Pokedex
{
    internal class Generation
    {
        private int id;

        public Generation(int id)
        {
            this.id = id;
        }

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

        public static int GetCount()
        {
            return GetRanges().GetLength(0);
        }

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
    }
}