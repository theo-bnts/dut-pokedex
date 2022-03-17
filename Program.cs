using System.Text.Json;
using System.Net;

using Pokedex;

using (var client = new WebClient())
{
    var APIBaseURL = "https://tmare.ndelpech.fr/tps/pokemons";
    
    var pokemonReferencesJson = client.DownloadString(APIBaseURL);
    var pokemonReferences = JsonSerializer.Deserialize<Reference[]>(pokemonReferencesJson);
    
    foreach (var pokemonReference in pokemonReferences)
    {
        var pokemonJson = client.DownloadString(pokemonReference.url);
        var pokemon = JsonSerializer.Deserialize<Data>(pokemonJson);
        
        Console.WriteLine(pokemon.name.fr);
    }
    
    Thread.Sleep(10000);
}