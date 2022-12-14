[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/banner-direct-single.svg)](https://stand-with-ukraine.pp.ua)

[![](https://img.shields.io/nuget/vpre/UToolKit)](https://www.nuget.org/packages/UToolKit/)
[![](https://img.shields.io/nuget/dt/UToolKit)](https://www.nuget.org/packages/UToolKit/)

# UToolKit
Library that contains useful tools for WPF application.  
It is recommended to use this library with [Fody.PropertyChanged](https://github.com/Fody/PropertyChanged).

Main features:
- **ObservableObject** (`INotifyPropertyChanged` implementation)
- **IRefreshableCommand** (inherits `ICommand`, allows to call `ICommand.CanExecuteChanged` manually)
- **RelayCommand** (`IRefreshableCommand` implementation)
- **RelayAsyncCommand** (`IRefreshableCommand` async implementation)

Converters:
- **InverseBooleanConverter**
- **BooleanToVisibilityConverter**
- **BooleanToHiddenVisibilityConverter**
- **InverseBooleanToHiddenVisibilityConverter**
- **InverseBooleanToVisibilityConverter**

Extensions:
- **HyperlinkExtensions**
  * `IsExternal` - if `true` Hyperlink will execute `Process.Start` using `Hyperlink.NavigateUri` after click.
- **WindowExtensions**
  * `CloseCommand` - command which executes on windows closing. If `ICommand.CanExecute()` returns false - windows closing will be cancalled.
  * `CloseCommandParameter` - parameter for `CloseCommand`.
  * `PlacementStorageStrategy` - windows placement storage strategy.
  There are 2 different implemented strategies, `RegistryStorage` and `SettingsStorage`.
  It is possible to implement custom strategy using `IWindowPlacementStorage`.  
- **RoutedCommand bindings** - allows to bind `ICommand` to `RoutedCommand`.

Services:
- **IWindowService** - (implementation: **WindowService**)
  * `IsActive` - indicates whether the window is active.
  * `IsVisible` - indicates whether the window is visible.
  * `Activate()` - attempts to brind the window to the foreground and activates it.
  * `Close()` - closes window.
  * `Close(bool dialogResult)` - closes window with specified dialog result.
  * `Hide()` - hides window.
  * `Show()` - shows window.
- **ITextBoxService** - (implementation: **TextBoxService**)
  * event `TextChanged` - informs that the text has changed.
  * event `SelectionChanged` - informs that the selection has changed.
  * `Text` - allows to get or set text to `TextBox` (will not break bindings).
  * `CaretIndex` - gets current caret index.
  * `SelectionLength` - gets selected text length.
  * `SelectedText` - gets or sets selected text in text box (will not break bindings).
  * `Select(int start, int length)` - selects a range of text in text box.
  * `SelectAll()` - selects all text in text box.
- **IPasswordSupplier** - (implementation: **PasswordSupplier**)
  * event `PasswordChanged` - occurs when the password of the `PasswordBox` changes.
  * `Password` - allows to get or set password from/to `PasswordBox`.
  * `SecurePassword` - gets secure password from `PasswordBox`.
  * `Clear()` - clears all password.

## Getting started.
Use one of the follwing methods to install and use this library:

- **Package Manager:**

    ```batch
    PM> Install-Package UToolKit
    ```

- **.NET CLI:**

    ```batch
    > dotnet add package UToolKit
    ```
----
First you need to include namespace to your code or markup.  

For **XAML** it can look like:
```XAML
<Window xmlns:tk="https://github.com/nullsoftware/UToolKit" />
```

And for **C#**:
```C#
using NullSoftware;
using NullSoftware.Services;
using NullSoftware.ToolKit;
using NullSoftware.ToolKit.Converters;
using NullSoftware.ToolKit.Extensions;
```
----
How to set `PlacementStorageStrategy`:  

```XAML
<Window xmlns:tk="https://github.com/nullsoftware/UToolKit" 
        xmlns:prop="clr-namespace:ExampleProject.Properties"
        tk:WindowExtensions.PlacementStorageStrategy="{tk:RegistryStorage}" />
        
<!--also there is possible to specify name or other registry storage properties-->
<!--tk:WindowExtensions.PlacementStorageStrategy="{tk:RegistryStorage NameFormat=Placement, Hive=CurrentUser, Key='SOFTWARE\MyCompany\MyApp'}"-->
```
or
```XAML
<Window xmlns:tk="https://github.com/nullsoftware/UToolKit" 
        xmlns:prop="clr-namespace:ExampleProject.Properties"
        tk:WindowExtensions.PlacementStorageStrategy="{tk:SettingsStorage Settings={x:Static prop:Settings.Default}}" />
```

How to use `RoutedCommandHandlers`:
```XAML
<Window xmlns:tk="https://github.com/nullsoftware/UToolKit">
    <tk:RoutedCommandHandlers.Commands>
        <tk:RoutedCommandHandler RoutedCommand="ApplicationCommands.Create" Command="{Binding CreateCommand}"/>
        <tk:RoutedCommandHandler RoutedCommand="ApplicationCommands.Open" Command="{Binding OpenCommand}"/>
    </tk:RoutedCommandHandlers.Commands>
    
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition/>
        </Grid.RowDefinitions>
        
        <Menu Grid.Row="0">
            <MenuItem Header="_File">
                <MenuItem Header="_New" Command="ApplicationCommands.Create" />
                <MenuItem Header="_Open" Command="ApplicationCommands.Open" />
            </MenuItem>
        </Menu>
    </Grid>
</Window>
```
