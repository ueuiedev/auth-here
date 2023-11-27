Imports Authenticator.ViewModels

Namespace Views
    ' TODO: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    Public NotInheritable Partial Class SettingsPage
        Inherits Page

        property ViewModel as SettingsViewModel = New SettingsViewModel

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Async Sub OnNavigatedTo(e As NavigationEventArgs)
            Await ViewModel.InitializeAsync()
        End Sub
    End Class
End Namespace
