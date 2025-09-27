namespace API.Services;

public static class HttpQuery
{
    public static string CreateQuery(string uri, Dictionary<string, string> queryParams)
    {
        string query;
        using (var content = new FormUrlEncodedContent(queryParams))
        {
            query = uri + "?" + content.ReadAsStringAsync().Result;
        }
        return query;
    }
}

public class NotificationService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _chatId;

    public NotificationService(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
        _chatId = _configuration["TelegramChatId"];

    }
    public async Task<bool> SendMessage(string message)
    {
        var client = _clientFactory.CreateClient("telegram");

        var query = HttpQuery.CreateQuery("sendMessage", new Dictionary<string, string>()
            {
                { "chat_id", _chatId},
                { "parse_mode", "HTML" },
                { "text", message}
            });
        var res = await client.GetAsync(query);

        return res.IsSuccessStatusCode;
    }
}
