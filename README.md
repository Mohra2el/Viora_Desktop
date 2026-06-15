# Viora — Accessibility Platform (.NET MAUI)

> A bilingual (English / Arabic) desktop & mobile app for visually impaired users.
> Built with .NET MAUI 8, CommunityToolkit.Mvvm, and the Anthropic Claude API.

---

## ✅ Prerequisites

| Tool | Version | Download |
|------|---------|----------|
| Visual Studio 2022 | 17.8 or later | https://visualstudio.microsoft.com/ |
| .NET SDK | 8.0 | https://dotnet.microsoft.com/download/dotnet/8 |
| MAUI workload | included in VS | (see step 2) |
| Windows App SDK | 1.3+ | installed automatically by VS |

---

## 🚀 Setup — Step by Step

### 1. Install Visual Studio 2022

During installation, select the workload:
- ✅ **.NET Multi-platform App UI development**

This installs MAUI, the Windows App SDK, and Android/iOS tooling.

---

### 2. Verify MAUI is installed (terminal)

```bash
dotnet workload list
# Should show: maui
```

If missing:
```bash
dotnet workload install maui
```

---

### 3. Clone / copy the project

```
Viora/
├── Viora.csproj
├── MauiProgram.cs
├── App.xaml / App.xaml.cs
├── Converters/
│   └── ValueConverters.cs
├── Models/
│   └── UserModel.cs
├── Pages/
│   ├── WelcomePage.xaml + .cs
│   ├── SignInPage.xaml + .cs
│   ├── SignUpPage.xaml + .cs
│   ├── HomePage.xaml + .cs
│   ├── VoiceRequestPage.xaml + .cs
│   ├── ChatAssistancePage.xaml + .cs
│   ├── UploadFilePage.xaml + .cs
│   └── ImageAnalysisPage.xaml + .cs
├── ViewModels/
│   ├── BaseViewModel.cs
│   ├── WelcomeViewModel.cs
│   ├── SignInViewModel.cs
│   ├── SignUpViewModel.cs
│   ├── HomeViewModel.cs
│   ├── VoiceRequestViewModel.cs
│   ├── ChatAssistanceViewModel.cs
│   └── FeatureViewModels.cs        ← UploadFile + ImageAnalysis VMs
├── Services/
│   ├── LocalizationService.cs
│   ├── VoiceGuidanceService.cs
│   ├── AuthService.cs
│   └── AIService.cs
└── Resources/
    ├── Styles/
    │   ├── Colors.xaml
    │   └── Styles.xaml
    ├── Fonts/          ← add OpenSans-Regular.ttf & OpenSans-Semibold.ttf
    ├── Images/
    ├── AppIcon/        ← add appicon.svg & appiconfg.svg
    └── Splash/         ← add splash.svg
```

---

### 4. Add fonts

Download **Open Sans** from https://fonts.google.com/specimen/Open+Sans

Place these two files in `Resources/Fonts/`:
- `OpenSans-Regular.ttf`
- `OpenSans-Semibold.ttf`

---

### 5. Add placeholder icons (minimum)

MAUI requires at least a placeholder for AppIcon and Splash.
Create `Resources/AppIcon/appicon.svg` with:
```xml
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100">
  <rect width="100" height="100" fill="#000"/>
  <text x="50" y="70" font-size="60" text-anchor="middle" fill="white">V</text>
</svg>
```
Copy the same file to:
- `Resources/AppIcon/appiconfg.svg`
- `Resources/Splash/splash.svg`

---

### 6. Set your Anthropic API key

Open `Services/AIService.cs` and replace:
```csharp
private const string ApiKey = "YOUR_ANTHROPIC_API_KEY";
```
with your actual key from https://console.anthropic.com/

> **Security tip:** In production, load the key from
> `SecureStorage.Default.GetAsync("api_key")` rather than hardcoding it.

---

### 7. Open and run in Visual Studio

1. Open `Viora.csproj` (or the folder) in **Visual Studio 2022**
2. In the toolbar, select the target framework:
   - **Windows Machine** → `net8.0-windows10.0.19041.0`
   - Android emulator → `net8.0-android`
3. Press **F5** to build and run

---

## 🌐 Language Support

| Feature | English | Arabic |
|---------|---------|--------|
| UI layout | LTR | RTL (auto-flipped) |
| Voice guidance | ✅ | ✅ |
| All labels | ✅ | ✅ |

Language is selected on the **Welcome page** (press 1 or 2) and persisted via `Preferences`.

---

## 🔊 Voice Guidance Architecture

Every page calls `VoiceGuidanceService.SpeakAsync()` on `OnAppearing()`.
Every interactive element (button, field) announces itself when focused.

```
User taps button
   → Command in ViewModel
   → await Voice.SpeakAsync("label")  ← TTS via TextToSpeech.Default
   → navigation / action
```

To add voice to a new element:
```csharp
[RelayCommand]
public async Task MyButtonFocused()
    => await Voice.SpeakAsync("My button description");
```

---

## 🤖 AI Features

| Feature | Endpoint |
|---------|----------|
| Chat assistance | `POST /v1/messages` (text) |
| Image OCR + description | `POST /v1/messages` (vision) |

The `AIService` class is the single point of integration.
Replace with a different provider by implementing `IAIService`.

---

## 📦 NuGet Packages Used

| Package | Purpose |
|---------|---------|
| `Microsoft.Maui.Controls` 8.0.3 | Core MAUI framework |
| `CommunityToolkit.Mvvm` 8.2.2 | `[ObservableProperty]`, `[RelayCommand]` source generators |

---

## 🛠 Adding a New Page

1. Create `Pages/MyPage.xaml` + `Pages/MyPage.xaml.cs`
2. Create `ViewModels/MyViewModel.cs` (extend `BaseViewModel`)
3. Register both in `MauiProgram.cs`:
   ```csharp
   builder.Services.AddTransient<MyViewModel>();
   builder.Services.AddTransient<MyPage>();
   ```
4. Add localization strings to both dictionaries in `LocalizationService.cs`
5. Call `await Voice.SpeakAsync(...)` in `OnPageAppeared()`

---

## 🧪 Mock vs Real Backend

`AuthService` is a **mock** — any non-empty email/password works.
Replace the body of `SignInAsync` / `SignUpAsync` with real HTTP calls
when your backend is ready. The interface `IAuthService` stays the same.
