using API.Services;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

public class ContactMeDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
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
    public async Task<IActionResult> ContactMe([FromBody] ContactMeDto contactMeDto)
    {
        await _notificationService.SendMessage(
            $@"
<u><b>Katameros Contact Form</b></u>
<b>Email:</b> {contactMeDto.Email}
<b>Name:</b> {contactMeDto.Name}
<b>Message:</b> 
{contactMeDto.Message}");

        return new OkResult();
    }
}
