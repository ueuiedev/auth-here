﻿Imports Authenticator.Helpers
Imports Authenticator.Services

Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input

Namespace ViewModels
    ' TODO: Add other settings as necessary. For help see https://github.com/microsoft/TemplateStudio/blob/main/docs/UWP/pages/settings.md
    Public Class SettingsViewModel
        Inherits ObservableObject

        Private _elementTheme As ElementTheme = ThemeSelectorService.Theme

        Public Property ElementTheme As ElementTheme
            Get
                Return _elementTheme
            End Get

            Set
                [SetProperty](_elementTheme, value)
            End Set
        End Property

        Private _versionDescription As String

        Public Property VersionDescription As String
            Get
                Return _versionDescription
            End Get

            Set
                [SetProperty](_versionDescription, newValue := value)
            End Set
        End Property

        Private _switchThemeCommand As ICommand

        Public ReadOnly Property SwitchThemeCommand As ICommand
            Get
                If _switchThemeCommand Is Nothing Then
                    _switchThemeCommand = New RelayCommand(Of ElementTheme)(Async Sub(param)
                        Await ThemeSelectorService.SetThemeAsync(param)
                    End Sub)
                End If

                Return _switchThemeCommand
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Async Function InitializeAsync() As Task
            VersionDescription = GetVersionDescription()
            Await Task.CompletedTask
        End Function

        Private Function GetVersionDescription() As String
            Dim appName = "AppDisplayName".GetLocalized()
            Dim package = Windows.ApplicationModel.Package.Current
            Dim packageId = package.Id
            Dim version = packageId.Version

            Return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}"
        End Function
    End Class
End Namespace
