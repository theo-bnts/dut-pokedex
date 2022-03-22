using System.Text.Json;
using System.Net;

namespace Pokedex
{
    internal class Pokemon
    {
        private int generation;
        private Data data;

        public int Generation
        {
            get
            {
                return this.generation;
            }
        }

        public int Id
        {
            get
            {
                return this.data.id;
            }
        }

        public List<string> Types
        {
            get
            {
                return this.data.types
                    .ConvertAll(t => t.ToLower())
                    .ToList();
            }
        }

        public int Height
        {
            get
            {
                return this.data.height;
            }
        }

        public int Weight
        {
            get
            {
                return this.data.weight;
            }
        }

        public List<Statistic> Statistics
        {
            get
            {
                return this.data.stats;
            }
        }

        public int LastEdit
        {
            get
            {
                return this.data.lastEdit;
            }
        }

        /// <summary>
        /// Pokemon constructor.
        /// </summary>
        /// <param name="id">The Pokemon id</param>
        public Pokemon(int id)
        {
            this.data = Downloader.GetData(id);

            this.generation = Pokedex.Generation.Get(id);
        }

        /// <summary>
        /// Return a string to display the Pokemon in the console.
        /// </summary>
        /// <returns>The text to display</returns>
        public string CondensedDisplay()
        {
            return $"[{this.data.id}] {this.data.name.fr}";
        }
    }
}
