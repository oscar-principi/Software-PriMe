using Backend.Interfaces;
using Backend.Modelos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IEmailService _emailService;

    public ContactController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var enviado = await _emailService.SendEmailAsync(dto.name, dto.email, dto.motivo, dto.mensaje);

        if (enviado)
            return Ok(new { message = "Mensaje enviado correctamente ✅" });

        return StatusCode(500, new { error = "No se pudo enviar el mensaje ❌" });
    }
}
