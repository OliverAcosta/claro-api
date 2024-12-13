using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace claro_api.Controllers
{
    public class BooksController:BaseController
    {
        public readonly BooksService booksService;
        public BooksController(BooksService bookService)
        {
            this.booksService = bookService;
        }

        [HttpGet("get")]
        public ActionResult GetBooks()
        {
            try
            {
                var contentData = this.booksService.GetBooksAsList();
                if (contentData.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(contentData.Data);
                }

                return StatusCode((int)contentData.StatusCode, null);
            }
            catch (Exception)
            {

                return StatusCode(500, null);
            }            
        }


        [HttpGet("get/{id}")]
        public ActionResult GetBooks(int id)
        {
            try
            {
                var contentData = this.booksService.GetBook(id);
                if (contentData.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(contentData.Data);
                }

                return StatusCode((int)contentData.StatusCode, null);
            }
            catch (Exception)
            {

                return StatusCode(500, null);
            }
        }


        [HttpPost("add")]
        public ActionResult Add(Books book)
        {
            try
            {
                var contentData = this.booksService.AddBooks(book);
                if (contentData.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(contentData.Data);
                }

                return StatusCode((int)contentData.StatusCode, null);
            }
            catch (Exception)
            {

                return StatusCode(500, null);
            }
        }


        [HttpPut("update/{id}")]
        public ActionResult Update(int id, Books book)
        {
            try
            {
                var contentData = this.booksService.UpdateBook(id, book);
                if (contentData.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(contentData.Data);
                }

                return StatusCode((int)contentData.StatusCode, null);
            }
            catch (Exception)
            {

                return StatusCode(500, null);
            }
        }


        [HttpDelete("delete/{id}")]
        public ActionResult deleteBook(int id)
        {
            try
            {
                var contentData = this.booksService.DeleteBook(id);
                if (contentData.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(contentData.Data);
                }

                return StatusCode((int)contentData.StatusCode, null);
            }
            catch (Exception ex)
            {

                return StatusCode(500, null);
            }
        }

    }
}
