using ConsoleTableExt;

namespace Pokedex
{
    internal class ConsoleManager
    {
        public static void DisplayPanel()
        {
            int answer;

            do
            {
                var strings = new string[] {
                    "What do you want to do?",
                    "\t1. Display Pokemons",
                    "\t2. Display Pokemons of a type",
                    "\t3. Display Pokemons of a generation",
                    "\t4. Display a Pokemon of each generation",
                    "\t5. Display stats on each type of Pokemon"
                };

                string text = string.Join(Environment.NewLine, strings);

                Console.Clear();
                Console.WriteLine(text);

                // Substract the 0 ascii code to the pressed key one
                answer = Console.ReadKey().KeyChar - '0';
            }
            while (answer < 1 || answer > 5);

            Console.Clear();

            switch (answer)
            {
                case 1:
                    DisplayPokemons();
                    break;

                case 2:
                    DisplayPokemonsOfType();
                    break;

                case 3:
                    DisplayPokemonsOfGeneration();
                    break;

                case 4:
                    DisplayAPokemonsOfEachGeneration();
                    break;
                case 5:
                    DisplayStatsOnEachType();
                    break;
            }

            Console.WriteLine("\nPress any key to go back on the menu...");
            Console.ReadKey();
            DisplayPanel();
        }

        private static void DisplayPokemons(List<Pokemon> pokemons)
        {
            if (pokemons.Count == 0)
            {
                Console.WriteLine("No Pokemon to display");
            }
            else
            {
                List<string> strings = new List<string>();

                pokemons.ForEach(pokemon =>
                    {
                        string displayText = pokemon.CondensedDisplay();
                        strings.Add(displayText);
                    }
                );

                string displayText = string.Join(Environment.NewLine, strings);

                Console.WriteLine(displayText);
            }
        }

        private static void DisplayPokemon(Pokemon pokemon)
        {
            DisplayPokemons(new List<Pokemon>() { pokemon });
        }

        private static void DisplayPokemons()
        {
            var pokemons = Pokemons.Get();

            DisplayPokemons(pokemons);
        }

        private static void DisplayPokemonsOfType()
        {
            Console.WriteLine("Type of Pokemon:");
            string typeString = Console.ReadLine();

            var type = new Type(typeString);
            var typePokemons = type.GetPokemons();

            Console.WriteLine();
            DisplayPokemons(typePokemons);
        }

        private static void DisplayPokemonsOfGeneration()
        {
            Console.WriteLine("Generation of Pokemon:");
            int generationId = int.Parse(Console.ReadLine());

            var generation = new Generation(generationId);
            var generationPokemons = generation.GetPokemons();

            Console.WriteLine();
            DisplayPokemons(generationPokemons);
        }

        private static void DisplayAPokemonsOfEachGeneration()
        {
            var random = new Random();

            for (int id = 1; id <= Generation.GetCount(); id++)
            {
                var generation = new Generation(id);
                var generationPokemons = generation.GetPokemons();

                int index = random.Next(generationPokemons.Count);

                var pokemon = generationPokemons[index];

                Console.WriteLine($"{(id > 1 ? '\n' : null)}Generation {id}:");
                DisplayPokemon(pokemon);
            }
        }

        private static void DisplayStatsOnEachType()
        {
            var types = Type.GetAll();

            var rows = new List<List<object>>();

            foreach (var type in types)
            {
                var statistics = type.GetStatistics();

                if (types.IndexOf(type) == 0)
                {
                    var titles = statistics
                        .Select(statistic => Tools.NormalizeCase(statistic.name))
                        .OfType<object>()
                        .ToList();

                    titles.Insert(0, "Type");

                    rows.Add(titles);
                }

                var values = statistics
                    .Select(statistic => statistic.stat)
                    .OfType<object>()
                    .ToList();

                values.Insert(0, Tools.NormalizeCase(type.Name));

                rows.Add(values);
            }

            ConsoleTableBuilder
                .From(rows)
                .WithFormat(ConsoleTableBuilderFormat.MarkDown)
                .ExportAndWriteLine();
        }
    }
}
