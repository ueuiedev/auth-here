Imports Authenticator.Core.Models

Namespace Views
    Public NotInheritable Partial Class OverviewDetailControl
        Inherits UserControl

        Public Property ListMenuItem As SampleOrder
            Get
                Return TryCast(GetValue(ListMenuItemProperty), SampleOrder)
            End Get
            Set
                SetValue(ListMenuItemProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ListMenuItemProperty As DependencyProperty = DependencyProperty.Register("ListMenuItem", GetType(SampleOrder), GetType(OverviewDetailControl), New PropertyMetadata(Nothing, AddressOf OnListMenuItemPropertyChanged))

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Shared Sub OnListMenuItemPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim control = TryCast(d, OverviewDetailControl)
            control.ForegroundElement.ChangeView(0, 0, 1)
        End Sub
    End Class
End Namespace
