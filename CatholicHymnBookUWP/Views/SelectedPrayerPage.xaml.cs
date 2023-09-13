using CatholicHymnBookUWP.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CatholicHymnBookUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectedPrayerPage : Page
    {
        public SelectedPrayerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Create an object of the Prayers class and assign the selected prayer from the navigation parameter 'e'
            Prayers selectedPrayer = e.Parameter as Prayers;

            // Display the prayer iff the selected prayer has been set/obtained
            if (selectedPrayer != null)
            {
                titleTextBlock.Text = selectedPrayer.Title;
                wordsTextBlock.Text = selectedPrayer.Words;
            }
        }
    }
}
