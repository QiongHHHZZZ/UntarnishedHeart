using System.Reflection;
using Dalamud.Plugin;

namespace UntarnishedHeart;

public sealed class Plugin : IAsyncDalamudPlugin
{
    public Plugin
    (
        IDalamudPluginInterface pi
    )
    {
        if (Instance != null || IsDisposed || IsInitialized)
            return;

        Version  ??= Assembly.GetExecutingAssembly().GetName().Version;
        Instance ??= this;

        PluginInterface = pi;
    }

    public const string PLUGIN_NAME = "Untarnished Heart";

    public static Version?             Version  { get; private set; }
    public static IAsyncDalamudPlugin? Instance { get; private set; }

    private bool IsDisposed    { get; set; }
    private bool IsInitialized { get; set; }

    private IDalamudPluginInterface PluginInterface { get; init; }

    public async Task LoadAsync
    (
        CancellationToken cancellationToken
    )
    {
        await Service.InitAsync(PluginInterface, cancellationToken);
        IsInitialized = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (Instance == null || IsDisposed || !IsInitialized)
            return;
        IsDisposed = true;

        await Service.UninitAsync();
    }
}
