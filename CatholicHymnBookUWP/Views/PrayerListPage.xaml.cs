using CatholicHymnBookUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CatholicHymnBookUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrayerListPage : Page
    {
        DatabaseHelper helper = new DatabaseHelper();
        public PrayerListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Prepare to store the list of prayers from the database
            ObservableCollection<Prayers> prayersList = new ObservableCollection<Prayers>();

            // Get the list of prayers
            prayersList = helper.GetAllPrayers();

            // Display the list
            prayersListView.ItemsSource = prayersList;

        }

        private void prayerListButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected prayer from the clicked button(selected prayer)
            var prayer = (sender as Button).DataContext as Prayers;

            // Get the selected prayer's Id
            var selectedPrayerId = prayer.Id;

            // Pass the Id to the Database helper's GetOnePrayer function
            var selectedPrayer = helper.GetOnePrayer(selectedPrayerId);

            // Navigate to the selectedprayer page, passing the selected hymn as a parameter
            Frame.Navigate(typeof(SelectedPrayerPage), selectedPrayer);
        }

        private void prayersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //(sender as ListView).SelectedItem = null;
        }
    }
}
