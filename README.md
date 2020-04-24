# WeatherApp
Sample weather app built using Xamarin Forms with support for:

- iOS
- Android

App queries OpenWeatherMap.org to get weather forecast for the device's location for a period of 7 days.

### Technology Stack used
- Xamarin Forms 4.4
- MVVM Helpers
- TimeZoneConverter
- Xamarin Essentials
- Xamanimation
- FluentAssertions
- AutoFixture

#### Tests
Using xUnit to mock out the service and test the individual components.

#### Prerequisites
On both Android and iOS Simulators go into settings and setup a Location for the app to automatically read Geo Coordinates.

![File](file.png) ![File2](file2.png)
