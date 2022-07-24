using GestioneSagre.Application.Magazzino.Interfaces;
using GestioneSagre.Models.InputModels.Categorie;
using GestioneSagre.Models.InputModels.Prodotti;
using GestioneSagre.Models.ViewModels;
using GestioneSagre.Models.ViewModels.Prodotti;
using GestioneSagre.Web.Server.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Web.Server.Controllers;

public class ProdottiController : BaseController
{
    private readonly IProdottiService prodottoService;
    public ProdottiController(IProdottiService prodottoService)
    {
        this.prodottoService = prodottoService;
    }

    /// <summary>
    /// Mostra l'elenco dei prodotti
    /// </summary>
    /// <response code="200">Lista dei prodotti caricata con successo</response>
    /// <response code="400">Lista dei prodotti non caricata causa errori</response>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProdottiAsync()
    {
        try
        {
            ListViewModel<ProdottoViewModel> prodotti = await prodottoService.GetProdottiAsync();

            ProdottoListViewModel viewModel = new()
            {
                Prodotti = prodotti
            };

            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Permette la creazione di un nuovo prodotto
    /// </summary>
    /// <response code="200">Creazione di un nuovo prodotto terminata con successo</response>
    /// <response code="400">Creazione di un nuovo prodotto non terminata causa errori</response>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaCreateInputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProdottoAsync([FromBody] ProdottoCreateInputModel inputModel)
    {
        try
        {
            bool bRes = await prodottoService.IsProdottoAvailableAsync(inputModel.Prodotto, 0);

            if (!bRes)
            {
                return BadRequest("Prodotto già presente");
            }
            else
            {
                ProdottoDetailViewModel prodotto = await prodottoService.CreateProdottoAsync(inputModel);
                return Ok(prodotto);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Permette la modifica di un prodotto
    /// </summary>
    /// <response code="200">Modifica del prodotto terminata con successo</response>
    /// <response code="400">Modifica del prodotto non terminata causa errori</response>
    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(CategoriaEditInputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditProdottoAsync([FromBody] ProdottoEditInputModel inputModel)
    {
        try
        {
            ProdottoDetailViewModel viewModel = await prodottoService.EditProdottoAsync(inputModel);

            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //DeleteProdottoAsync (valutare se implementare questa funzione)
}