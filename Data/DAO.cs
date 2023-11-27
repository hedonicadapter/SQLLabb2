using Newtonsoft.Json.Linq;

namespace BlazorApp2.Data;

public static class DAO
{
    private static readonly HttpClient OpenLibrary = new()
    {
        BaseAddress = new Uri("https://openlibrary.org")
    };

    public static async Task<JObject?> GetBookByISBN(string isbn)
    {
        var res = await OpenLibrary.GetAsync($"/isbn/{isbn}.json");

        if (!res.IsSuccessStatusCode) return null;
        
        return await GetJSONData(res);
    }

    public static async Task<JObject?> GetAuthors(string? authorURL)
    {
        if (string.IsNullOrEmpty(authorURL)) return null;
        
        var res = await OpenLibrary.GetAsync($"{authorURL}.json");
        
        if (!res.IsSuccessStatusCode) return null;
        
        return await GetJSONData(res);
    }
    
    public static async Task<JObject?> FindAuthor(string authorFirstname, string authorLastname)
    {
        if (string.IsNullOrEmpty(authorFirstname) && string.IsNullOrEmpty(authorLastname)) return null;
        
        var res = await OpenLibrary.GetAsync($"/search/authors.json?q={authorFirstname}%20{authorLastname}");
        
        if (!res.IsSuccessStatusCode) return null;
        
        return await GetJSONData(res);
    }

    private static async Task<JObject?> GetJSONData(HttpResponseMessage res)
    {
        string data = await res.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(data);

        return json;
    }
}