using GestioneSagre.Models.ViewModels.Versioni;

namespace GestioneSagre.Web.Client.Services.Versioni;

public interface IVersioneService
{
    Task<VersioneViewModel> GetVersione();
}