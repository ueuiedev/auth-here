Imports Authenticator.Services

Namespace Activation
    Friend Class SchemeActivationHandler
        Inherits ActivationHandler(Of ProtocolActivatedEventArgs)

        ' By default, this handler expects URIs of the format 'wtsapp:sample?paramName1=paramValue1&paramName2=paramValue2'
        Protected Overrides Async Function HandleInternalAsync(args As ProtocolActivatedEventArgs) As Task
            ' Create data from activation Uri in ProtocolActivatedEventArgs
            Dim data = New SchemeActivationData(args.Uri)
            If data.IsValid Then
                NavigationService.Navigate(data.PageType, data.Parameters)
            End If
            Await Task.CompletedTask
        End Function

        Protected Overrides Function CanHandleInternal(args As ProtocolActivatedEventArgs) As Boolean
            ' If your app has multiple handlers of ProtocolActivationEventArgs
            ' use this method to determine which to use. (possibly checking args.Uri.Scheme)
            Return True
        End Function
    End Class
End Namespace
