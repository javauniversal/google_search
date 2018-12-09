using System;
using System.Collections.Generic;

namespace GoogleBook.Search.Models
{
    public class RootObject
    {
        public string error { get; set; }
        public string total { get; set; }
        public string page { get; set; }
        public List<Book> books { get; set; }
    }
}