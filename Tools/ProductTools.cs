using System.ComponentModel;
using ModelContextProtocol.Server;

internal class ProductTools
{
    [McpServerTool]
    [Description("Returns a random number between min and max.")]
    public int GetRandomNumber(int min, int max)
    {
        return Random.Shared.Next(min, max);
    }
    [McpServerTool]
    [Description("Returns details of products containing 'chicken' in any field from the API.")]
    public async Task<List<Product>?> GetChickenProducts()
    {
        using var httpClient = new HttpClient();
        var url = "https://fake.jsonmockapi.com/products?length=50";
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(json);
        if (products == null) return null;
        var chickenProducts = products.Where(p =>
            (p.ProductName?.ToLower().Contains("chicken") ?? false) ||
            (p.Description?.ToLower().Contains("chicken") ?? false) ||
            (p.Category?.ToLower().Contains("chicken") ?? false)
        ).ToList();
        return chickenProducts;
    }

    [McpServerTool]
    [Description("Returns a list of 10 products from the API as Product objects.")]
    public async Task<List<Product>?> GetTop10ProductList()
    {
        using var httpClient = new HttpClient();
        var url = "https://fake.jsonmockapi.com/products?length=10";
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(json);
        return products;
    }

    [McpServerTool]
    [Description("Returns a single product from the API as a Product object.")]
    public async Task<Product?> GetProduct(int id)
    {
        using var httpClient = new HttpClient();
        var url = $"https://fake.jsonmockapi.com/products/{id}";
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var product = System.Text.Json.JsonSerializer.Deserialize<Product>(json);
        return product;
    }

    [McpServerTool]
    [Description("Returns top 10 products from the API as JSON string.")]
    public async Task<string> GetTop10Products()
    {
        using var httpClient = new HttpClient();
        var url = "https://fake.jsonmockapi.com/products?length=10";
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(json);
        var serialized = System.Text.Json.JsonSerializer.Serialize(products);
        return serialized;
    }
}
