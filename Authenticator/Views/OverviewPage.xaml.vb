Imports Authenticator.ViewModels

Namespace Views
    Public NotInheritable Partial Class OverviewPage
        Inherits Page

        property ViewModel as OverviewViewModel = New OverviewViewModel

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf OverviewPage_Loaded
        End Sub

        Private Async Sub OverviewPage_Loaded(sender As Object, e As RoutedEventArgs)
            Await ViewModel.LoadDataAsync(ListDetailsViewControl.ViewState)
        End Sub
    End Class
End Namespace
