using MoneyPlus.Models;
using Newtonsoft.Json;

namespace MoneyPlus.Services.NewsService;

public class NewsService : INewsService
{
    private readonly IConfiguration _configuration;

    public NewsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public FinanceNews GetFinanceNews()
    {
        string apiKey = _configuration.GetValue<string>("API_KEY");
        string baseUrl = _configuration.GetValue<string>("API_URL");

        using(var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseUrl);

            HttpResponseMessage response = client.GetAsync("?apikey=" + apiKey).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<FinanceNews>(result);
            }
            else
            {
                return new FinanceNews()
                {
                    Data = new List<NewsArticle>(),
                    Pagination = new Pagination()
                };
            }
        }
    }
}
