
using Microsoft.Extensions.Configuration;
using Models;
using System.Collections.Generic;

namespace Services
{
    public class BooksService
    {
        public readonly HttpBase http;
        private readonly string url;
        public BooksService(HttpBase http, IConfigurationRoot configuration) 
        { 
            this.http = http;
            this.url = configuration.GetSection("urlService").Value;
        }

        public ContentData<Books> AddBooks(Books obj)
        {
           return this.http.Post<Books, Books>(string.Format("{0}{1}", this.url, "/api/v1/Books"), obj).Result;
        }

        public ContentData<Books> UpdateBook(int id, Books obj)
        {
            return this.http.Put<Books, Books>(string.Format("{0}{1}{2}", this.url, "/api/v1/Books/", id), obj).Result;
        }
        public ContentData<Books> DeleteBook(int id)
        {
            return http.Delete<Books>(string.Format("{0}{1}{2}", this.url, "/api/v1/Books/", id)).Result;
        }

        public ContentData<Books> GetBook(int id)
        {
           return http.Get<Books>(string.Format("{0}{1}{2}", this.url, "/api/v1/Books/", id)).Result;
        }

        public ContentData<List<Books>> GetBooksAsList()
        {
            return this.http.Get<List<Books>>(string.Format("{0}{1}", this.url, "/api/v1/Books")).Result;
        }
    }
}
