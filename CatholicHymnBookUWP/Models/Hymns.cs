using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SQLite;
using SQLite.Net.Attributes;

namespace CatholicHymnBookUWP.Models
{
    class Hymns : INotifyPropertyChanged
    {
        // Define the fields(rows) in the hymns' database
        // Property names should be the same as field names in the actual database.

        public string _title;
        public int _id;

        [PrimaryKey]
        [AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("Id"); }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged("Title"); }
        }

        public string Words { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public string SearchText
        {
            get
            {
                return String.Format("{0}|{1}", Id, Title);
            }
        }

    }
}
