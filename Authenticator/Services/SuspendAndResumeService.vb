﻿Imports System.Reflection

Imports Authenticator.Activation
Imports Authenticator.Helpers

Imports Windows.Storage

Namespace Services

    ' The SuspendAndResumeService allows you to save the App data before the App is being suspended (or enters in background state).
    ' In case the App is terminated during suspension, the data is restored during App launch by this ActivationHandler.
    ' In case the App is resumed without being terminated no data should be lost, a resume event is fired that allows you to refresh App data that might
    ' be outdated (e.g data from online feeds)
    ' Documentation:
    '     * How to implement and test: https://github.com/microsoft/TemplateStudio/blob/main/docs/UWP/features/suspend-and-resume.md
    '     * Application Lifecycle: https://docs.microsoft.com/windows/uwp/launch-resume/app-lifecycle
    Friend Class SuspendAndResumeService
        Inherits ActivationHandler(Of LaunchActivatedEventArgs)

        Private Const StateFilename As String = "SuspendAndResumeState"

        ' TODO: Subscribe to the OnBackgroundEntering and OnDataRestored events from your current Page to save and restore the current App data.
        ' Only one Page should subscribe to OnBackgroundEntering and OnDataRestored at a time, as the App will navigate to that Page on resume.
        Public Event OnBackgroundEntering As EventHandler(Of SuspendAndResumeArgs)

        Public Event OnDataRestored As EventHandler(Of SuspendAndResumeArgs)

        ' TODO: Subscribe to the OnResuming event from the current Page
        ' if you need to refresh online data when the App resumes without being terminated.
        Public Event OnResuming As EventHandler


        ' This method saves the application state before entering background state. It fires the event OnBackgroundEntering to collect
        ' state data from the current subscriber and saves it to the local storage.
        Public Async Function SaveStateAsync() As Task(Of Boolean)
            If OnBackgroundEnteringEvent Is Nothing Then
                Return False
            End If

            Try
                Dim suspensionState = New SuspensionState() With {
                    .SuspensionDate = DateTime.Now
                }

                Dim target As Type = Nothing

                If OnBackgroundEnteringEvent IsNot Nothing Then
                    target = OnBackgroundEnteringEvent.Target.GetType
                End If

                Dim onBackgroundEnteringArgs = New SuspendAndResumeArgs(suspensionState, target)

                RaiseEvent OnBackgroundEntering(Me, onBackgroundEnteringArgs)

                Await ApplicationData.Current.LocalFolder.SaveAsync(StateFilename, onBackgroundEnteringArgs)
                Return True
            Catch ex As Exception
                ' TODO: Save state can fail in rare conditions, please handle exceptions as appropriate to your scenario.
                Return False
            End Try
        End Function

        ' This method allows subscribers to refresh data that might be outdated when the App is resuming from suspension.
        ' If the App was terminated during suspension this event will not fire, data restore is handled by the method HandleInternalAsync.
        Public Sub ResumeApp()
            RaiseEvent OnResuming(Me, EventArgs.Empty)
        End Sub

        Public Async Function RestoreSuspendAndResumeData() As Task
            Dim saveState = Await GetSuspendAndResumeData()

            If saveState IsNot Nothing Then
                RaiseEvent OnDataRestored(Me, saveState)
            End If
        End Function

        ' This method restores application state when the App is launched after termination, it navigates to the stored Page passing the recovered state data.
        Protected Overrides Async Function HandleInternalAsync(args As LaunchActivatedEventArgs) As Task
            Dim saveState = Await GetSuspendAndResumeData()
            If saveState?.Target IsNot Nothing AndAlso GetType(Page).IsAssignableFrom(saveState.Target) Then
                NavigationService.Navigate(saveState.Target, saveState.SuspensionState)
            End If
        End Function

        Protected Overrides Function CanHandleInternal(args As LaunchActivatedEventArgs) As Boolean
            ' Application State must only be restored if the App was terminated during suspension.
            Return args.PreviousExecutionState = ApplicationExecutionState.Terminated
        End Function

        Public Async Function GetSuspendAndResumeData() As Task(Of SuspendAndResumeArgs)
            Dim saveState = Await ApplicationData.Current.LocalFolder.ReadAsync(Of SuspendAndResumeArgs)(StateFilename)

            If saveState?.Target IsNot Nothing AndAlso GetType(Page).IsAssignableFrom(saveState.Target) Then
                Return saveState
            End If

            Return Nothing
        End Function

    End Class
End Namespace
