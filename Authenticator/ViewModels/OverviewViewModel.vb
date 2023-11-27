﻿Imports Authenticator.Core.Models
Imports Authenticator.Core.Services

Imports CommunityToolkit.Mvvm.ComponentModel
Imports Microsoft.Toolkit.Uwp.UI.Controls

Namespace ViewModels
    Public Class OverviewViewModel
        Inherits ObservableObject

        Private _selected As SampleOrder

        Public Property Selected As SampleOrder
            Get
                Return _selected
            End Get
            Set
                [SetProperty](_selected, value)
            End Set
        End Property

        Public Property SampleItems As ObservableCollection(Of SampleOrder) = new ObservableCollection(Of SampleOrder)

        Public Sub New()
        End Sub

        Public Async Function LoadDataAsync(viewState As ListDetailsViewState) As Task
            SampleItems.Clear()

            Dim data = Await SampleDataService.GetListDetailsDataAsync()

            For Each item As SampleOrder In data
                SampleItems.Add(item)
            Next

            If viewState = ListDetailsViewState.Both Then
                Selected = SampleItems.FirstOrDefault()
            End If
        End Function
    End Class
End Namespace
