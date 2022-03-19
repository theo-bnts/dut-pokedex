namespace Pokedex
{
    internal class Data
    {
        public int id { get; set; }
        public StringTranslations name { get; set; }
        public List<string> types { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public StringTranslations genus { get; set; }
        public StringTranslations description { get; set; }
        public List<Statistic> stats { get; set; }
        public int lastEdit { get; set; }
    }
}
