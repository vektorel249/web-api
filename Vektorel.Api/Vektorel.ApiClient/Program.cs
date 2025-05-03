using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vektorel.ApiClient;

internal class Program
{
    static void Main(string[] args)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:12001");
        httpClient.DefaultRequestHeaders.Add("va-code", "123");

        // GetCategories(httpClient);
        // GetCategoryProducts(httpClient, 1);

        var newProduct = new Product
        {
            ProductName = "Hoparlör",
            CategoryId = 9,
            UnitPrice = 120,
            UnitsInStock = 32
        };
        CreateProduct(httpClient, newProduct);
        Console.ReadLine();
    }

    static async void GetCategories(HttpClient httpClient)
    {
        var response = await httpClient.GetAsync("api/categories");
        if (response.HasFailed())
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine(error);
            return;
        }
        //response.EnsureSuccessStatusCode(); // 2xx gelmek zorunda yoksa exception fırlat
        var content  = await response.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<List<Category>>(content);
        foreach (var category in categories) 
        {
            Console.WriteLine($"{category.CategoryId} - { category.CategoryName }");
        }
    }

    static async void GetCategoryProducts(HttpClient httpClient, int id)
    {
        var response = await httpClient.GetAsync($"api/products/category/{id}");
        if (response.HasFailed())
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine(error);
            return;
        }
        //response.EnsureSuccessStatusCode(); // 2xx gelmek zorunda yoksa exception fırlat
        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<Product>>(content);
        foreach (var category in products)
        {
            Console.WriteLine($"{category.ProductID} - {category.ProductName}");
        }
    }

    public static async void CreateProduct(HttpClient httpClient, Product product)
    {
        var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("api/products/create", content);
        if (response.HasFailed())
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine(error);
            return;
        }

        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }
}


public class Category
{
    [JsonPropertyName("categoryID")]
    public int CategoryId { get; set; }
    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; }
}

public class Product
{
    [JsonPropertyName("productID")]
    public int ProductID { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("unitsInStock")]
    public int? UnitsInStock { get; set; }

    [JsonPropertyName("unitPrice")]
    public double? UnitPrice { get; set; }

    [JsonPropertyName("categoryId")]
    public int? CategoryId { get; set; }
}