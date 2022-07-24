﻿using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

namespace GestioneSagre.Web.Client;

public class AppBase : ComponentBase, IDisposable
{
    [Inject] private LazyAssemblyLoader AssemblyLoader { get; set; }
    [Inject] private ILogger<App> Logger { get; set; }

    protected readonly List<Assembly> LazyLoadedAssemblies = new();

    protected async Task OnNavigateAsync(NavigationContext args)
    {
        try
        {
            switch (args.Path)
            {
                case "inizio":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.Init.dll" //Configurazione iniziale
                        });
                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }

                case "configurazione":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.Configuration.dll" //Configurazione Prodotti, Categorie, Logo, Menu
                        });
                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }

                case "utenti":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.Users.dll" //Configurazione Operatori (Ruoli e permessi)
                        });
                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }

                case "cassa":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.Cashier.dll" //Gestione dello scontrino, movimenti cassa, prenotazioni
                        });
                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }

                case "stampe":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.Prints.dll" //Stampe prenotazioni, menu, contabilità cassa
                        });
                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }

                case "statistiche":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.Statistics.dll" //Statistiche consumi giornata e totali, riepilogo cassa ed incassi
                        });
                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }

                case "dashboard":
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.Dashboard.dll" //Pannello principale dell'applicazione
                        });
                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }

                default:
                    {
                        var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
                        {
                            "GestioneSagre.Modules.App.dll"
                        });

                        LazyLoadedAssemblies.AddRange(assemblies);
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error Loading spares page: {ex}");
        }
    }

    public void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AppBase()
    {
        this.Dispose(false);
    }
}