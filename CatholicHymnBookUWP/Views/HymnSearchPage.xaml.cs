using CatholicHymnBookUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CatholicHymnBookUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HymnSearchPage : Page
    {
        ObservableCollection<Hymns> hymnsList = new ObservableCollection<Hymns>();
        DatabaseHelper helper = new DatabaseHelper();

        public HymnSearchPage()
        {
            this.InitializeComponent();

            // Get all hymns
            hymnsList = helper.GetAllHymns();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Check if any text has been entered
            // Display the hymn list
            // Display the progress ring
            // Filter the list if there is text
            // Hide the progress ring after results are displayed
            // Display only the placeholder text if there is no text
            if (searchTextBox.Text != "")
            {
                hymnsListView.Visibility = Visibility.Visible;
                loadingProgressBar.Visibility = Visibility.Visible;
                // Filter the list to match the search string
                hymnsListView.ItemsSource = hymnsList.Where(w => w.Id.ToString().ToUpper().Contains(searchTextBox.Text.ToUpper()) || w.Title.ToUpper().Contains(searchTextBox.Text.ToUpper()));

                loadingProgressBar.Visibility = Visibility.Collapsed;
            }
            else if (searchTextBox.Text == "")
            {
                searchTextBox.PlaceholderText = "Search by number of title";
                hymnsListView.Visibility = Visibility.Collapsed;
            }
        }

        private void searchTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            searchTextBox.Focus(FocusState.Keyboard);
        }

        private void searchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Show the placeholder text
            searchTextBox.PlaceholderText = "Search number or title";
        }

        private void hymnListButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected hymn from the clicked button(selected hymn)
            var hymn = (sender as Button).DataContext as Hymns;

            // Get the selected hymn's Id
            var selectedHymnId = hymn.Id;

            // Pass the Id to the Database helper's GetOneHymn function
            var selectedHymn = helper.GetOneHymn(selectedHymnId);

            // Navigate to the hymn page, passing the selected hymn as a parameter
            Frame.Navigate(typeof(SelectedHymnPage), selectedHymn);
        }

        private void hymnsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
