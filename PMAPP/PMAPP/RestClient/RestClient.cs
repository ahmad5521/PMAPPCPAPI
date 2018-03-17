using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PMAPP.Models;
using System.Text;

namespace Plugin.RestClient
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class RestClient<T>
    {
        public string WebServiceUrl;

        public async Task<List<T>> GetAsync()
        {
            var httpClient = new HttpClient();

            var byteArray = Encoding.UTF8.GetBytes(DataStore.getUserName() + ":" + DataStore.getPassword());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var json = await httpClient.GetStringAsync(WebServiceUrl);

            var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

            return taskModels;
        }

        public async Task<T> GetNotListAsync()
        {
            var httpClient = new HttpClient();

            var byteArray = Encoding.UTF8.GetBytes(DataStore.getUserName() + ":" + DataStore.getPassword());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var json = await httpClient.GetStringAsync(WebServiceUrl);

            var taskModels = JsonConvert.DeserializeObject<T>(json);

            return taskModels;
        }

        public async Task<bool> PostAsync(T t)
        {
            var httpClient = new HttpClient();

            var byteArray = Encoding.UTF8.GetBytes(DataStore.getUserName() + ":" + DataStore.getPassword());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var result = await httpClient.PostAsync(WebServiceUrl, httpContent);
                return result.IsSuccessStatusCode;
            }
            catch(Exception err)
            {
                return false;
            }
        }

        public async Task<bool> PostAsyncTwoPar(T t, T t2)
        {
            var httpClient = new HttpClient();

            var byteArray = Encoding.UTF8.GetBytes(DataStore.getUserName() + ":" + DataStore.getPassword());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var result = await httpClient.PostAsync(WebServiceUrl, httpContent);
                return result.IsSuccessStatusCode;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public async Task<bool> PutAsync(int id, T t)
        {
            var httpClient = new HttpClient();

            var byteArray = Encoding.UTF8.GetBytes(DataStore.getUserName() + ":" + DataStore.getPassword());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync(WebServiceUrl + id, httpContent);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var httpClient = new HttpClient();

            var byteArray = Encoding.UTF8.GetBytes(DataStore.getUserName() + ":" + DataStore.getPassword());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response = await httpClient.DeleteAsync(WebServiceUrl + id);

            return response.IsSuccessStatusCode;
        }

        
    }
}
