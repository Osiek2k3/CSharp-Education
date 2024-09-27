using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using AutoMapper;
using MongoDBAPI.Data;
using MongoDBAPI.Models;
using MongoDBAPI.Models.Dto;
using System.Threading.Tasks;

namespace MongoDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookstoreController : ControllerBase
    {
        private readonly DBContextBookstore _dbContextBookstore;
        private readonly IMapper mapper;

        public BookstoreController(DBContextBookstore dbContextBookstore,IMapper mapper)
        {
            _dbContextBookstore = dbContextBookstore;
            this.mapper = mapper;
        }

        [HttpGet("GetAllBooks")]

        public async Task<IActionResult> GetAllBooks([FromQuery] string title = null, [FromQuery] string author = null, [FromQuery] string sortBy = "title", [FromQuery] bool sortDescending = false)
        {
            var filter = Builders<BookModel>.Filter.Empty;

            if (!string.IsNullOrEmpty(title))
            {
                filter = Builders<BookModel>.Filter.Eq(book => book.title, title);
            }
            if (!string.IsNullOrEmpty(author))
            {
                var authorFilter = Builders<BookModel>.Filter.Eq(book => book.author, author);
                filter = Builders<BookModel>.Filter.And(filter, authorFilter);
            }

            var sortDefinition = Builders<BookModel>.Sort.Ascending(sortBy);
            if (sortDescending)
            {
                sortDefinition = Builders<BookModel>.Sort.Descending(sortBy);
            }

            var books = await _dbContextBookstore.BookModel
                .Find(filter)
                .Sort(sortDefinition)
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("GetIdBook/{id}")]
        public async Task<IActionResult> GetIdBook(string id)
        {
            var filter = Builders<BookModel>.Filter.Eq(book => book.Id, id);

            var book = await _dbContextBookstore.BookModel.Find(filter).FirstOrDefaultAsync();

            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("AddNewBook")]
        public async Task<IActionResult> AddNewBook([FromBody]BookModelDto bookModelDto)
        {
            var newBook = mapper.Map<BookModel>(bookModelDto);
            await _dbContextBookstore.BookModel.InsertOneAsync(newBook);

            return CreatedAtAction(nameof(GetIdBook), new { id = newBook.Id }, newBook);
        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook(string id, [FromBody] BookModelDto bookModelDto)
        {
            var filter = Builders<BookModel>.Filter.Eq(book => book.Id, id);

            var update = Builders<BookModel>.Update
            .Set(book => book.title, bookModelDto.title)
            .Set(book => book.author, bookModelDto.author)
            .Set(book => book.pages, bookModelDto.pages)
            .Set(book => book.rating, bookModelDto.rating)
            .Set(book => book.genres, bookModelDto.genres)
            .Set(book => book.reviews, bookModelDto.reviews);

            var result = await _dbContextBookstore.BookModel.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                return NotFound(); 
            }

            var updatedBook = await _dbContextBookstore.BookModel.Find(filter).FirstOrDefaultAsync();
            return Ok(updatedBook);
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var filter = Builders<BookModel>.Filter.Eq(book => book.Id, id);
            var bookToDelete = await _dbContextBookstore.BookModel.Find(filter).FirstOrDefaultAsync();
            var result = await _dbContextBookstore.BookModel.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                return NotFound(); 
            }

            return Ok(bookToDelete);
        }
    }
}
