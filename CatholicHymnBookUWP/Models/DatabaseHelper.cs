using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net;
using System.IO;

namespace CatholicHymnBookUWP.Models
{
    class DatabaseHelper
    {
        // This class contains functions for retrieving a list of all the hymns/prayers from the database
        // and retrieving one hymn/prayer.
        // The CategorySelection function handles the filtering of the list of hymns according to some categories (Entrance songs, Christmas and Easter hymns, etc)

        // string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "HymnBookDatabase.sqlite");

        // connect to the database file
        string path = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "HymnBookDatabase.sqlite");

        public ObservableCollection<Hymns> GetAllHymns()
        {
            //Connect to the database
            using (SQLiteConnection dbConnection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                //get a list of all hymns
                var hymnCollection = dbConnection.Table<Hymns>().ToList();

                //put the list of hymns in an observable collection and return it
                var hymnList = new ObservableCollection<Hymns>(hymnCollection);

                return hymnList;
            }
        }

        public Hymns GetOneHymn(int Id)
        {
            //Connect to the database
            using (SQLiteConnection dbConnection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                //select the specified hymn from the database with the Id parameter that is passed in and return it
                var selectedHymn = dbConnection.Query<Hymns>("select * from Hymns where Id=" + Id).FirstOrDefault();

                return selectedHymn;
            }
        }

        public ObservableCollection<Prayers> GetAllPrayers()
        {
            //Connect to the database
            using (SQLiteConnection dbConnection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                //Get a list of all prayers
                var prayerCollection = dbConnection.Table<Prayers>().ToList();

                //put the list of prayes in an observable collection object and return it
                var prayerList = new ObservableCollection<Prayers>(prayerCollection);

                return prayerList;
            }
        }

        public Prayers GetOnePrayer(int Id)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                //select the specified prayer from the prayers table with the Id parameter that is passed in and return it
                var selectedPrayer = dbConnection.Query<Prayers>("select * from Prayers where Id=" + Id).FirstOrDefault();

                return selectedPrayer ;
            }
        }

        public ObservableCollection<Hymns> CategorySelection(int startPoint, int endPoint)
        {
            //Connect to the database
            using (SQLiteConnection dbConnection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            {
                //get a list of hymns using the startPoint and endPoint parameters
                var selectedHymnCollection = dbConnection.Query<Hymns>($"select * from Hymns where Id between {startPoint} and {endPoint}" ).ToList();
                var selectedHymns = new ObservableCollection<Hymns>(selectedHymnCollection);

                return selectedHymns;
            }
        }
    }
}
