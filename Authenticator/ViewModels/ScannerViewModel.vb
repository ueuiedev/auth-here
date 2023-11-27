Imports Authenticator.EventHandlers
Imports Authenticator.Helpers

Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input

Namespace ViewModels
    Public Class ScannerViewModel
        Inherits ObservableObject

        Private _photoTakenCommand As ICommand

        Private _photo As BitmapImage

        Public Property Photo As BitmapImage
            Get
                Return _photo
            End Get
            Set
                  [SetProperty](_photo, value)
            End Set
        End Property

        Public ReadOnly Property PhotoTakenCommand As ICommand
            Get
                If _photoTakenCommand Is Nothing Then
                    _photoTakenCommand = New RelayCommand(Of CameraControlEventArgs)(AddressOf OnPhotoTaken)
                End If

                Return _photoTakenCommand
            End Get
        End Property

        Private Sub OnPhotoTaken(args As CameraControlEventArgs)
            If Not String.IsNullOrEmpty(args.Photo) Then
                Photo = New BitmapImage(New Uri(args.Photo))
            End If
        End Sub
    End Class
End Namespace
