using System;
using GoogleBook.Search.Models;
using GoogleBook.Search.Repositories;
using GoogleBook.Search.View;

namespace GoogleBook.Search.Controllers
{
    public class  ControllerSearch
    {
        private SearchBook search;
        private RestClient restClient;

        public ControllerSearch(SearchBook search)
        {
            this.search = search;
            restClient = new RestClient();
        }

        public async void callToServiceSearchBook (String query) 
        {
            var formatUrl = String.Format("{0}{1}{2}", search.Resources.GetString(Resource.String.url_base), "search/", query);
            var result = await restClient.Get<RootObject>(formatUrl);
            if (result != null) 
            {
                this.search.IGetListBook(result);
            }
            else 
            {
                this.search.IErrorBook(result.error);
            }
        }

    }
}
