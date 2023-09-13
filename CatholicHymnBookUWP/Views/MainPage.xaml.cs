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
using Windows.UI.Core;
using CatholicHymnBookUWP.Models;
using Windows.System.Profile;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CatholicHymnBookUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // DatabaseHelper object
        DatabaseHelper dbHelper = new DatabaseHelper();

        // RadioButton object
        RadioButton rb = new RadioButton();

        public MainPage()
        {
            this.InitializeComponent();

            // Load the hymn list page on first load 
            ContentFrame.Navigate(typeof(Views.HymnListPage));

            // Set the title of the page
            TitleTextBlock.Text = "Hymns";

            //Highlight 'Hymns' on the menu 
            MenuListBox.SelectedItem = MenuListBox.Items[0];

            // Subscribe to the HardwareButton OnBackRequested event to handle back navigation
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            // Show the back button for Desktop
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void MenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Navigate to the page selected on the menu
            // Set the title of the page
            // Show/Hide searchIcon if the page is not HymnListPage
            // Show/Hide categoryIcon if the page is not HymnListPage 
            // Close the menu
            if (Hymns.IsSelected)
            {
                ContentFrame.Navigate(typeof(Views.HymnListPage));
                TitleTextBlock.Text = "Hymns"; 
                searchIcon.Visibility = Visibility.Visible;         
                categoriesIcon.Visibility = Visibility.Visible;
                // Clear the category selectiom
                rb.IsChecked = false;
                MenuSplitView.IsPaneOpen = false;          
            }
            else if (Prayers.IsSelected)
            {
                ContentFrame.Navigate(typeof(Views.PrayerListPage));
                TitleTextBlock.Text = "Prayers";              
                searchIcon.Visibility = Visibility.Collapsed;                             
                categoriesIcon.Visibility = Visibility.Collapsed;
                MenuSplitView.IsPaneOpen = false;
            }
            else if (Rosary.IsSelected)
            {
                ContentFrame.Navigate(typeof(Views.RosaryPage));
                TitleTextBlock.Text = "Rosary";
                searchIcon.Visibility = Visibility.Collapsed;
                categoriesIcon.Visibility = Visibility.Collapsed;
                MenuSplitView.IsPaneOpen = false;
            }
            else if (Stations.IsSelected)
            {
                ContentFrame.Navigate(typeof(Views.StationsPage));
                TitleTextBlock.Text = "Stations of the Cross";
                searchIcon.Visibility = Visibility.Collapsed;
                categoriesIcon.Visibility = Visibility.Collapsed;
                MenuSplitView.IsPaneOpen = false;
            }
            else if (About.IsSelected)
            {
                ContentFrame.Navigate(typeof(Views.AboutPage));
                TitleTextBlock.Text = "About this app";
                searchIcon.Visibility = Visibility.Collapsed;
                categoriesIcon.Visibility = Visibility.Collapsed;
                MenuSplitView.IsPaneOpen = false;
            }

        }

        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Toggle the opening and closing of the menu
            MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
        }
        
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            // Handle the phone's hardware back button press
            e.Handled = true;
       
            // Check if ContentFrame can navigate backwards
            var navigationState = ContentFrame.CanGoBack;
            switch (navigationState)
            {
                // If ContentFrame can navigate backwards, check if the menu is opened
                case true:
                    var menuState = MenuSplitView.IsPaneOpen;
                    switch (menuState)
                    {
                        // if the menu is opened, close it and do nothing else
                        case true:
                            MenuSplitView.IsPaneOpen = false;
                            break;

                        // if the menu is closed, handle back navigation
                        case false:
                            // Make sure ContentFrame's BackStack is not empty
                            if (ContentFrame.BackStack.Count > 0)
                            {
                                // Get the most recent page navigated from and get its name
                                var previousPage = ContentFrame.BackStack[ContentFrame.BackStackDepth - 1];
                                string previousPageName = previousPage.SourcePageType.Name;

                                // Navigate to the most recent page in ContentFrame's BackStack
                                // If the most recent page is a selected hymn or the about page, navigate to the            hymnlist  page
                                // If the most recent page is a selected prayer, navigate to the prayerlist page 
                                // Set the title after navigating backwards
                                // Show searchIcon where necessary
                                // Show categoryIcon where necessary
                                // Set the correct selection on the menu
                                // Clear the BackStack so the app can exit on successive back presses
                                if (previousPageName == "HymnListPage" || previousPageName == "SelectedHymnPage" || previousPageName == "AboutPage" || previousPageName == "HymnSearchPage" || previousPageName == "CategoryPage")
                                {
                                    ContentFrame.Navigate(typeof(Views.HymnListPage));
                                    TitleTextBlock.Text = "Hymns";                                 
                                    searchIcon.Visibility = Visibility.Visible;                                   
                                    categoriesIcon.Visibility = Visibility.Visible;
                                    // Clear the category selectiom
                                    rb.IsChecked = false;
                                    MenuListBox.SelectedItem = MenuListBox.Items[0];
                                    ContentFrame.BackStack.Clear();
                                }
                                else if (previousPageName == "PrayerListPage" || previousPageName == "SelectedPrayerPage")
                                {
                                    ContentFrame.Navigate(typeof(Views.PrayerListPage));
                                    TitleTextBlock.Text = "Prayers";
                                    MenuListBox.SelectedItem = MenuListBox.Items[1];
                                    ContentFrame.BackStack.Clear();
                                }
                                else if (previousPageName == "StationsPage")
                                {
                                    ContentFrame.Navigate(typeof(Views.StationsPage));
                                    TitleTextBlock.Text = "Stations of the Cross";
                                    MenuListBox.SelectedItem = MenuListBox.Items[2];
                                    ContentFrame.BackStack.Clear();
                                }
                                else if (previousPageName == "RosaryPage")
                                {
                                    ContentFrame.Navigate(typeof(Views.RosaryPage));
                                    TitleTextBlock.Text = "Rosary";
                                    MenuListBox.SelectedItem = MenuListBox.Items[3];
                                    ContentFrame.BackStack.Clear();
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                // If ContentFrame can't navigate backwards, check if the menu is opened
                case false:
                    var menuState_2 = MenuSplitView.IsPaneOpen;
                    switch (menuState_2)
                    {
                        // If the menu is opened, close it and do nothing else
                        case true:
                            MenuSplitView.IsPaneOpen = false;
                            break;

                        // If the menu is closed, app can exit
                        case false:
                           
                            App.Current.Exit();
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
        
        private void searchIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Navigate to the search page
            ContentFrame.Navigate(typeof(Views.HymnSearchPage));

            // Hide the search icon
            searchIcon.Visibility = Visibility.Collapsed;

            // Hide the categories icon
            categoriesIcon.Visibility = Visibility.Collapsed;
        }

        private void categoriesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {   
            // Link to the hymn category flyout and display it 
            Flyout HymnCategoryFlyout = Resources["HymnCategoryFlyout"] as Flyout;

            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                HymnCategoryFlyout.ShowAt((FrameworkElement)sender);
            }
        }

        private void HymnCategory_Checked(object sender, RoutedEventArgs e)
        {
            // Handle the radio button that is checked
            rb = sender as RadioButton;

            // Get the name of the selected category (checkbox) and use that to determine the numbers that should be passed to the CategorySelection function 
            // Navigate to the Category page with the selected hymns in the category, iff the category selection is valid (not empty)
            // Hide the flyout menu
            if (rb != null)
            {
                string categoryName = rb.Name.ToString();
                string categoryContent = rb.Content.ToString();

                switch (categoryName)
                {
                    case "catOneRB":
                        var categoryOne = dbHelper.CategorySelection(1, 16);
                        if (categoryOne != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryOne);
                            hymnCatFlyout.Hide();
                        }                     
                        break;

                    case "catTwoRB":
                        var categoryTwo = dbHelper.CategorySelection(17, 20);
                        if (categoryTwo != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwo);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThreeRB":
                        var categoryThree = dbHelper.CategorySelection(21, 24);
                        if (categoryThree != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThree);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFourRB":
                        var categoryFour = dbHelper.CategorySelection(25, 41);
                        if (categoryFour != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryFour);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFiveRB":
                        var categoryFive = dbHelper.CategorySelection(42, 47);
                        if (categoryFive != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryFive);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catSixRB":
                        var categorySix = dbHelper.CategorySelection(48, 51);
                        if (categorySix != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categorySix);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catSevenRB":
                        var categorySeven = dbHelper.CategorySelection(52, 53);
                        if (categorySeven != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categorySeven);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catEightRB":
                        var categoryEight = dbHelper.CategorySelection(54, 69);
                        if (categoryEight != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryEight);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catNineRB":
                        var categoryNine = dbHelper.CategorySelection(70, 74);
                        if (categoryNine != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryNine);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTenRB":
                        var categoryTen = dbHelper.CategorySelection(75, 78);
                        if (categoryTen != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTen);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catElevenRB":
                        var categoryEleven = dbHelper.CategorySelection(79, 81);
                        if (categoryEleven != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryEleven);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwelveRB":
                        var categoryTwelve = dbHelper.CategorySelection(82, 88);
                        if (categoryTwelve != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwelve);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirteenRB":
                        var categoryThirteen = dbHelper.CategorySelection(89, 92);
                        if (categoryThirteen != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirteen);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFourteenRB":
                        var categoryFourteen = dbHelper.CategorySelection(93, 111);
                        if (categoryFourteen != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryFourteen);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFifteenRB":
                        var categoryFifteen = dbHelper.CategorySelection(112, 119);
                        if (categoryFifteen != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryFifteen);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catSixteenRB":
                        var categorySixteen = dbHelper.CategorySelection(120, 133);
                        if (categorySixteen != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categorySixteen);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catSeventeenRB":
                        var categorySeventeen = dbHelper.CategorySelection(134, 156);
                        if (categorySeventeen != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categorySeventeen);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catEighteenRB":
                        var categoryEighteen = dbHelper.CategorySelection(157, 167);
                        if (categoryEighteen != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryEighteen);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catNineteenRB":
                        var categoryNineteen = dbHelper.CategorySelection(168, 187);
                        if (categoryNineteen != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryNineteen);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentyRB":
                        var categoryTwenty = dbHelper.CategorySelection(188, 207);
                        if (categoryTwenty != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwenty);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentyOneRB":
                        var categoryTwentyOne = dbHelper.CategorySelection(208, 215);
                        if (categoryTwentyOne != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentyOne);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentyTwoRB":
                        var categoryTwentyTwo = dbHelper.CategorySelection(216, 230);
                        if (categoryTwentyTwo != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentyTwo);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentyThreeRB":
                        var categoryTwentyThree = dbHelper.CategorySelection(231, 261);
                        if (categoryTwentyThree != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentyThree);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentyFourRB":
                        var categoryTwentyFour = dbHelper.CategorySelection(262, 276);
                        if (categoryTwentyFour != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentyFour);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentyFiveRB":
                        var categoryTwentyFive = dbHelper.CategorySelection(277, 299);
                        if (categoryTwentyFive != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentyFive);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentySixRB":
                        var categoryTwentySix = dbHelper.CategorySelection(300, 302);
                        if (categoryTwentySix != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentySix);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentySevenRB":
                        var categoryTwentySeven = dbHelper.CategorySelection(303, 309);
                        if (categoryTwentySeven != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentySeven);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentyEightRB":
                        var categoryTwentyEight = dbHelper.CategorySelection(310, 312);
                        if (categoryTwentyEight != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentyEight);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catTwentyNineRB":
                        var categoryTwentyNine = dbHelper.CategorySelection(313, 319);
                        if (categoryTwentyNine != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryTwentyNine);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtyRB":
                        var categoryThirty = dbHelper.CategorySelection(320, 327);
                        if (categoryThirty != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirty);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtyOneRB":
                        var categoryThirtyOne = dbHelper.CategorySelection(328, 333);
                        if (categoryThirtyOne != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtyOne);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtyTwoRB":
                        var categoryThirtyTwo = dbHelper.CategorySelection(334, 338);
                        if (categoryThirtyTwo != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtyTwo);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtyThreeRB":
                        var categoryThirtyThree = dbHelper.CategorySelection(339, 339);
                        if (categoryThirtyThree != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtyThree);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtyFourRB":
                        var categoryThirtyFour = dbHelper.CategorySelection(340, 342);
                        if (categoryThirtyFour != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtyFour);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtyFiveRB":
                        var categoryThirtyFive = dbHelper.CategorySelection(343, 346);
                        if (categoryThirtyFive != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtyFive);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtySixRB":
                        var categoryThirtySix = dbHelper.CategorySelection(347, 358);
                        if (categoryThirtySix != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtySix);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtySevenRB":
                        var categoryThirtySeven = dbHelper.CategorySelection(360, 371);
                        if (categoryThirtySeven != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtySeven);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtyEightRB":
                        var categoryThirtyEight = dbHelper.CategorySelection(372, 389);
                        if (categoryThirtyEight != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtyEight);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catThirtyNineRB":
                        var categoryThirtyNine = dbHelper.CategorySelection(390, 406);
                        if (categoryThirtyNine != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryThirtyNine);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFortyRB":
                        var categoryForty = dbHelper.CategorySelection(407, 416);
                        if (categoryForty != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryForty);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFortyOneRB":
                        var categoryFortyOne = dbHelper.CategorySelection(417, 422);
                        if (categoryFortyOne != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryFortyOne);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFortyTwoRB":
                        var categoryFortyTwo = dbHelper.CategorySelection(423, 435);
                        if (categoryFortyTwo != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryFortyTwo);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFortyThreeRB":
                        var categoryFortyThree = dbHelper.CategorySelection(436, 448);
                        if (categoryFortyThree != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryFortyThree);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    case "catFortyFourRB":
                        var categoryFortyFour = dbHelper.CategorySelection(449, 458);
                        if (categoryFortyFour != null)
                        {
                            ContentFrame.Navigate(typeof(Views.CategoryPage), categoryFortyFour);
                            hymnCatFlyout.Hide();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
