using System;
using System.Collections.Generic;
using GoogleBook.Search.Models;

namespace GoogleBook.Search.Interface
{
    public interface IBook
    {
        void IGetListBook(RootObject objects);

        void IErrorBook(string error);

    }
}
