using GestioneSagre.Models.InputModels.InvioEmail;

namespace GestioneSagre.Web.Client.Services.Supporto;

public interface ISupportoService
{
    Task InvioEmailSupporto(InputMailSender input);
}