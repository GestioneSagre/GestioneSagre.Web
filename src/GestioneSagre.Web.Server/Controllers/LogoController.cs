﻿using GestioneSagre.Models.InputModels.Logo;
using GestioneSagre.Upload.UploadLogo.Interfaces;
using GestioneSagre.Web.Server.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Web.Server.Controllers;

public class LogoController : BaseController
{
    private readonly ILogoService logoService;
    public LogoController(ILogoService logoService)
    {
        this.logoService = logoService;
    }

    /// <summary>
    /// Permette l'upload dell'immagine per il logo della festa.
    /// </summary>
    /// <response code="200">Caricamento terminato con successo</response>
    /// <response code="400">Caricamento non terminato causa errori</response>
    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(LogoEditInputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> EditLogoAsync([FromForm] LogoEditInputModel inputModel)
    {
        try
        {
            await logoService.UploadLogoAsync(inputModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}