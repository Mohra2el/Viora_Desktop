using Viora.Pages;
using Viora.Services;

namespace Viora;

/// <summary>
/// App is the root application class.
/// It sets the initial page to WelcomePage and wires up global navigation.
/// </summary>
public partial class App : Application
{
    private readonly ILocalizationService _localization;

    public App(ILocalizationService localization)
    {
        InitializeComponent();
        _localization = localization;

        // Start with the Welcome page wrapped in a NavigationPage
        // so we can push/pop pages throughout the app
        MainPage = new NavigationPage(new WelcomePage(
            IPlatformApplication.Current!.Services.GetRequiredService<ViewModels.WelcomeViewModel>()
        ))
        {
            BarBackgroundColor = Colors.Black,
            BarTextColor = Colors.White
        };
    }
}
