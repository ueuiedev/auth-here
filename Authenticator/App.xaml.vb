Imports Authenticator.Core.Helpers
Imports Authenticator.Services

NotInheritable Partial Class App
    Inherits Application

    Private _activationService As Lazy(Of ActivationService)

    Private ReadOnly Property ActivationService As ActivationService
        Get
            Return _activationService.Value
        End Get
    End Property

    Public Sub New()
        InitializeComponent()

        AddHandler EnteredBackground, AddressOf App_EnteredBackground
        AddHandler Resuming, AddressOf App_Resuming
        AddHandler UnhandledException, AddressOf OnAppUnhandledException

        ' Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
        _activationService = New Lazy(Of ActivationService)(AddressOf CreateActivationService)
    End Sub

    Protected Overrides Async Sub OnLaunched(args As LaunchActivatedEventArgs)
        If Not args.PrelaunchActivated Then
            Await ActivationService.ActivateAsync(args)
        End If
    End Sub

    Protected Overrides Async Sub OnActivated(args As IActivatedEventArgs)
        Await ActivationService.ActivateAsync(args)
    End Sub

    Private Sub OnAppUnhandledException(sender As Object, e As Windows.UI.Xaml.UnhandledExceptionEventArgs)
        ' TODO: Please log and handle the exception as appropriate to your scenario
        ' For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
    End Sub

    Private Function CreateActivationService() As ActivationService
        Return New ActivationService(Me, GetType(Views.OverviewPage), New Lazy(Of UIElement)(AddressOf CreateShell))
    End Function

    Private Function CreateShell() As UIElement
        Return New Views.ShellPage()
    End Function

    Private Async Sub App_EnteredBackground(sender As Object, e As EnteredBackgroundEventArgs)
        Dim deferral = e.GetDeferral()
        Await Singleton(Of SuspendAndResumeService).Instance.SaveStateAsync()
        deferral.Complete()
    End Sub

    Private Sub App_Resuming(sender As Object, e As Object)
        Singleton(Of SuspendAndResumeService).Instance.ResumeApp()
     End Sub

    Protected Overrides Async Sub OnBackgroundActivated(args As BackgroundActivatedEventArgs)
        Await ActivationService.ActivateAsync(args)
    End Sub
End Class
