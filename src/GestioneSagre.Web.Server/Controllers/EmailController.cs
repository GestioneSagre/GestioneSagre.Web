using GestioneSagre.Email.Services.Interfaces;
using GestioneSagre.Models.InputModels.InvioEmail;
using GestioneSagre.Tools.Options;
using GestioneSagre.Web.Server.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GestioneSagre.Web.Server.Controllers;

public class EmailController : BaseController
{
    private readonly IOptionsMonitor<SmtpOptions> smtpOptionsMonitor;
    private readonly ILogger<EmailController> logger;
    private readonly IEmailSenderService emailService;

    public EmailController(ILogger<EmailController> logger, IEmailSenderService emailService, IOptionsMonitor<SmtpOptions> smtpOptionsMonitor)
    {
        this.logger = logger;
        this.emailService = emailService;
        this.smtpOptionsMonitor = smtpOptionsMonitor;
    }

    /// <summary>
    /// Servizio di invio email
    /// </summary>
    /// <response code="200">Email inviata con successo</response>
    /// <response code="400">Email non inviata causa errori</response>
    [AllowAnonymous]
    [HttpPost("InvioEmailSupporto")]
    [ProducesResponseType(typeof(InputMailSupportoSender), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> InvioEmailSupportoAsync(InputMailSupportoSender model)
    {
        try
        {
            var options = this.smtpOptionsMonitor.CurrentValue;
            var customOptions = new InputMailOptionSender
            {
                DestinatarioNominativo = "Supporto Gestione Sagre",
                DestinatarioEmail = "supporto.gestionesagre@aepserver.it",
                Oggetto = "Richiesta di supporto",
                Host = options.Host,
                Port = options.Port,
                Security = options.Security,
                Username = options.Username,
                Password = options.Password
            };

            await emailService.SendEmailSupportoAsync(model, customOptions);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}