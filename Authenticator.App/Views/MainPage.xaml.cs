using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Authenticator.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Window_SizeChanged;
            // Call the size changed method once to set the initial state
            Window_SizeChanged(null, null);
        }

        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            // Adjust layout based on window width
            if (Window.Current.Bounds.Width < 700)
            {
                // Only display TOTPListView and expand its width
                Master.Visibility = Visibility.Visible;
                Grid.SetColumn(Master, 0);
                Grid.SetColumnSpan(Master, 2);
                Master.Width = Window.Current.Bounds.Width;

                // Hide content area
                Detail.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Display both TOTPListView and content area in two columns
                Master.Visibility = Visibility.Visible;
                Grid.SetColumn(Master, 0);
                Grid.SetColumnSpan(Master, 1);
                Master.Width = double.NaN; // Reset width to Auto

                Detail.Visibility = Visibility.Visible;
            }
        }
    }
}
