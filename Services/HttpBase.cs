using Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services
{
    public class HttpBase
    {
        private readonly HttpClient http;
        public HttpBase()
        {
            this.http = new HttpClient();
        }


        public async Task<ContentData<TData>> Get<TData>(string url) where TData : class, new()
        {
            var request = await this.http.GetAsync(url);
            var contentout = new ContentData<TData>();
            contentout.StatusCode = request.StatusCode;
            if(request.IsSuccessStatusCode)
            {
               contentout.Data = await request.Content.ReadFromJsonAsync<TData>();
            }

            return contentout;
        }


        public async Task<ContentData<TData>> Post<TData, TcontentType>(string url, TcontentType content) where TcontentType : class, new()
        {
            using StringContent jsonContent = new(
              JsonSerializer.Serialize(content), Encoding.UTF8,"application/json");

            var request = await this.http.PostAsync(url, jsonContent);
            var contentout = new ContentData<TData>();
            contentout.StatusCode = request.StatusCode;
            if (request.IsSuccessStatusCode)
            {
                contentout.Data = await request.Content.ReadFromJsonAsync<TData>();
            }

            return contentout;
        }

        public async Task<ContentData<TData>> Put<TData, TcontentType>(string url, TcontentType content) where TcontentType : class, new()
        {
            using StringContent jsonContent = new(
              JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

            var request = await this.http.PutAsync(url, jsonContent);
            var contentout = new ContentData<TData>();
            contentout.StatusCode = request.StatusCode;
            if (request.IsSuccessStatusCode)
            {
                contentout.Data = await request.Content.ReadFromJsonAsync<TData>();
            }

            return contentout;
        }


        public async Task<ContentData<TData>> Delete<TData>(string url) 
        {
            var request = await this.http.DeleteAsync(url);
            var contentout = new ContentData<TData>();
            contentout.StatusCode = request.StatusCode;
            if (request.IsSuccessStatusCode)
            {
                contentout.Data = await request.Content.ReadFromJsonAsync<TData>();
            }

            return contentout;
        }

    }
}
