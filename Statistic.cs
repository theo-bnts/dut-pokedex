namespace Pokedex
{
    internal class Statistic
    {
        public string name { get; set; }
        public int stat { get; set; }
        public int count { get; set; } = 1;

        /// <summary>
        /// Static constructor.
        /// </summary>
        /// <param name="name">The statistic name</param>
        /// <param name="stat">The statistic value</param>
        public Statistic(string name, int stat)
        {
            this.name = name;
            this.stat = stat;
        }
    }
}
