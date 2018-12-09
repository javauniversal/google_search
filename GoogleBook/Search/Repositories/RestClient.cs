using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoogleBook.Search.Repositories
{
    public class RestClient
    {
        public async Task<T> Get<T>(string url)
        {
            try 
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK) 
                {
                    var jsonstring = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonstring);
                }
            } 
            catch (Exception exception)
            {
                throw exception;
            }

            return default(T);
        }
    }
}
