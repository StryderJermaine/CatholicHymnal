using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CatholicHymnBookUWP.Models;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CatholicHymnBookUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : Page
    {
        DatabaseHelper helper = new DatabaseHelper();

        public CategoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ObservableCollection<Hymns> sortedHymns  = e.Parameter as ObservableCollection<Hymns>;

            if (sortedHymns != null)
            {
                hymnsListBox.ItemsSource = sortedHymns;
            }
        }

        private void hymnListButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected hymn from the clicked button(selected hymn)
            var hymnObject = (sender as Button).DataContext as Hymns;

            // Get the selected hymn's Id
            var selectedHymnId = hymnObject.Id;

            // Pass the Id to the Database helper's GetOneHymn function
            var selectedHymn = helper.GetOneHymn(selectedHymnId);

            // Navigate to the hymn page, passing the selected hymn as a parameter
            Frame.Navigate(typeof(SelectedHymnPage), selectedHymn);
        }
    }
}
