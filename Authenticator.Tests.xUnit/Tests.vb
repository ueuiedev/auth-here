Imports Authenticator.ViewModels

Imports Xunit

' TODO: Add appropriate tests
Public Class Tests
    <Fact>
    Public Sub TestMethod1()
    End Sub

    ' TODO: Add tests for functionality you add to OverviewViewModel.
    <Fact>
    Public Sub TestOverviewViewModelCreation()
        ' This test is trivial. Add your own tests for the logic you add to the ViewModel.
        Dim vm = new OverviewViewModel()
        Assert.NotNull(vm)
    End Sub

    ' TODO: Add tests for functionality you add to ScannerViewModel.
    <Fact>
    Public Sub TestScannerViewModelCreation()
        ' This test is trivial. Add your own tests for the logic you add to the ViewModel.
        Dim vm = new ScannerViewModel()
        Assert.NotNull(vm)
    End Sub

    ' TODO: Add tests for functionality you add to SettingsViewModel.
    <Fact>
    Public Sub TestSettingsViewModelCreation()
        ' This test is trivial. Add your own tests for the logic you add to the ViewModel.
        Dim vm = new SettingsViewModel()
        Assert.NotNull(vm)
    End Sub
End Class
