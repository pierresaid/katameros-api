using API.Services;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

public class ContactMeDto
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Message { get; set; }
}

[Route("[controller]")]
public class ContactController
{
    private readonly NotificationService _notificationService;
    public ContactController(NotificationService emailService)
    {
        _notificationService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> ContactMe([FromBody] ContactMeDto? contactMeDto)
    {
        var email = contactMeDto?.Email;
        var name = contactMeDto?.Name;
        var message = contactMeDto?.Message;

        // Mirrors the contact form's own rules (all fields required, email format,
        // message 5-5000 chars), so this only rejects direct API posts the form
        // could never have sent. The name/email caps are far above any real value.
        if (string.IsNullOrEmpty(name) || name.Length > 500
            || string.IsNullOrEmpty(email) || email.Length > 500 || !email.Contains('@')
            || string.IsNullOrEmpty(message) || message.Length < 5 || message.Length > 5000)
        {
            return new BadRequestResult();
        }

        var res = await _notificationService.SendMessage(
            $@"
<u><b>Katameros Contact Form</b></u>
<b>Email:</b> {EscapeHtml(email)}
<b>Name:</b> {EscapeHtml(name)}
<b>Message:</b>
{EscapeHtml(message)}");

        return res ? new OkResult() : new StatusCodeResult(500);
    }

    // Telegram parse_mode=HTML rejects the whole message on unescaped <, > or &.
    private static string EscapeHtml(string text) =>
        text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
}
