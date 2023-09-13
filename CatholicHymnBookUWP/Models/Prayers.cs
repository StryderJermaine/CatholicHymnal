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
    class Prayers
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Words { get; set; }
    }
}
