using System.Net.Http.Json;
using GestioneSagre.Models.ViewModels.Versioni;

namespace GestioneSagre.Web.Client.Services.Versioni;

public class VersioneService : IVersioneService
{
    private HttpClient httpClient;

    private string Versione = "08da4afe-6f26-d449-753a-8e553407828b";

    public VersioneViewModel testoVersione { get; set; }

    public VersioneService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<VersioneViewModel> GetVersione()
    {
        testoVersione = await httpClient.GetFromJsonAsync<VersioneViewModel>($"/api/versione/{Versione}");

        return testoVersione;
    }
}