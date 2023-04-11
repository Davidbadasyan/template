namespace Common.Application.Helpers;

public class HttpClientHelper
{
    private readonly HttpClient _httpClient;

    public HttpClientHelper(IHttpClientFactory httpClientFactory, string baseAddress)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(baseAddress);
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<T> GetAsync<T>(string requestUrl, Dictionary<string, string> queryParams = null)
    {
        var response = await _httpClient.GetAsync(requestUrl);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(responseString);

        return result;
    }

    public async Task<string> PostAsync<T>(string requestUrl, T requestBody)
    {
        var httpContent =
            new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestUrl, httpContent);
        var responseString = await response.Content.ReadAsStringAsync();

        return responseString;
    }
}