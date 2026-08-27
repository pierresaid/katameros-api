namespace API.Services;

public class NotificationService
{
    // Telegram rejects sendMessage calls whose text exceeds 4096 characters.
    private const int TelegramMessageLimit = 4096;

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

        foreach (var chunk in SplitMessage(message))
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "chat_id", _chatId },
                { "parse_mode", "HTML" },
                { "text", chunk }
            });
            var res = await client.PostAsync("sendMessage", content);

            if (!res.IsSuccessStatusCode)
                return false;
        }

        return true;
    }

    // Splits on raw markup length: Telegram's limit applies to the parsed text,
    // which is never longer than the raw markup, so every chunk fits.
    private static IEnumerable<string> SplitMessage(string message)
    {
        while (message.Length > TelegramMessageLimit)
        {
            var cut = TelegramMessageLimit;
            // Keep &amp;/&lt;/&gt; entities whole across the cut.
            var amp = message.LastIndexOf('&', cut - 1, 6);
            if (amp >= 0 && message.IndexOf(';', amp, cut - amp) < 0)
                cut = amp;
            yield return message[..cut];
            message = message[cut..];
        }

        if (message.Length > 0)
            yield return message;
    }
}
