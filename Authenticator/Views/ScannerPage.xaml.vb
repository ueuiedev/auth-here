﻿Imports Authenticator.ViewModels

Namespace Views
    Public NotInheritable Partial Class ScannerPage
        Inherits Page

        property ViewModel as ScannerViewModel = New ScannerViewModel

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Async Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)
            Await cameraControl.InitializeCameraAsync()
        End Sub

        Protected Async Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
            MyBase.OnNavigatedFrom(e)
            Await cameraControl.CleanupCameraAsync()
        End Sub
    End Class
End Namespace
