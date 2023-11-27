using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Authenticator.App.Views
{
    public sealed partial class MasterControl : UserControl
    {
        public MasterControl()
        {
            this.InitializeComponent();
        }
        
        private void TOTPList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
