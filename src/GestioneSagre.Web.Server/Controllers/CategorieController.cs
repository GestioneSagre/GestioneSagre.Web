using GestioneSagre.Application.Magazzino.Interfaces;
using GestioneSagre.Models.InputModels.Categorie;
using GestioneSagre.Models.ViewModels;
using GestioneSagre.Models.ViewModels.Categorie;
using GestioneSagre.Models.ViewModels.Feste;
using GestioneSagre.Web.Server.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestioneSagre.Web.Server.Controllers;

public class CategorieController : BaseController
{
    private readonly ICategorieService categoriaService;
    private readonly IProdottiService prodottoService;
    public CategorieController(ICategorieService categoriaService, IProdottiService prodottoService)
    {
        this.categoriaService = categoriaService;
        this.prodottoService = prodottoService;
    }

    /// <summary>
    /// Mostra l'elenco delle categorie
    /// </summary>
    /// <response code="200">Lista delle categorie caricata con successo</response>
    /// <response code="400">Lista delle categorie non caricata causa errori</response>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCategorieAsync()
    {
        try
        {
            ListViewModel<CategoriaViewModel> categorie = await categoriaService.GetCategorieAsync();

            CategoriaListViewModel viewModel = new()
            {
                Categorie = categorie
            };

            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Permette la creazione di una nuova categoria
    /// </summary>
    /// <response code="200">Creazione di una nuova categoria terminata con successo</response>
    /// <response code="400">Creazione di una nuova categoria non terminata causa errori</response>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(CategoriaCreateInputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategoriaAsync([FromBody] CategoriaCreateInputModel inputModel)
    {
        try
        {
            bool bRes = await categoriaService.IsCategoriaAvailableAsync(inputModel.CategoriaVideo, 0);

            if (!bRes)
            {
                return BadRequest("Categoria già presente");
            }
            else
            {
                CategoriaDetailViewModel categoria = await categoriaService.CreateCategoriaAsync(inputModel);
                return Ok(categoria);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Permette la modifica di una categoria
    /// </summary>
    /// <response code="200">Modifica della categoria terminata con successo</response>
    /// <response code="400">Modifica della categoria non terminata causa errori</response>
    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(CategoriaEditInputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditCategoriaAsync([FromBody] CategoriaEditInputModel inputModel)
    {
        try
        {
            CategoriaDetailViewModel viewModel = await categoriaService.EditCategoriaAsync(inputModel);

            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Permette la cancellazione di una categoria, solo se alla categoria non vi sono associati prodotti
    /// </summary>
    /// <response code="200">Cancellazione della categoria terminata con successo</response>
    /// <response code="400">Cancellazione della categoria non terminata causa errori</response>
    [AllowAnonymous]
    [HttpDelete]
    [ProducesResponseType(typeof(CategoriaDeleteInputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategoriaAsync([FromBody] CategoriaDeleteInputModel inputModel)
    {
        try
        {
            CategoriaViewModel dettaglioCategoria = await categoriaService.GetCategoriaAsync(inputModel.CategoriaVideo, inputModel.GuidFesta);

            int IdCategoria = dettaglioCategoria.Id;
            int bConta = await prodottoService.CountProdottiByCategoriaAsync(IdCategoria);

            if (bConta > 0)
            {
                return BadRequest("Non è possibile cancellare la categoria, vi sono prodotti associati");
            }
            else
            {
                await categoriaService.DeleteCategoriaAsync(inputModel);
                return Ok();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}