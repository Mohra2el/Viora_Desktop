using Microsoft.Extensions.Logging;
using Viora.Pages;
using Viora.Services;
using Viora.ViewModels;

namespace Viora;

/// <summary>
/// MauiProgram is the entry point for the MAUI application.
/// It registers all pages, view models, and services with dependency injection.
/// </summary>
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                // Register custom fonts (place .ttf files in Resources/Fonts/)
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // ── Services (Singleton = one instance for app lifetime) ──────────────
        builder.Services.AddSingleton<ILocalizationService, LocalizationService>();
        builder.Services.AddSingleton<IVoiceGuidanceService, VoiceGuidanceService>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<IAIService, AIService>();

        // ── ViewModels ────────────────────────────────────────────────────────
        builder.Services.AddTransient<WelcomeViewModel>();
        builder.Services.AddTransient<SignInViewModel>();
        builder.Services.AddTransient<SignUpViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<VoiceRequestViewModel>();
        builder.Services.AddTransient<ChatAssistanceViewModel>();
        builder.Services.AddTransient<UploadFileViewModel>();
        builder.Services.AddTransient<ImageAnalysisViewModel>();

        // ── Pages ─────────────────────────────────────────────────────────────
        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<SignInPage>();
        builder.Services.AddTransient<SignUpPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<VoiceRequestPage>();
        builder.Services.AddTransient<ChatAssistancePage>();
        builder.Services.AddTransient<UploadFilePage>();
        builder.Services.AddTransient<ImageAnalysisPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
