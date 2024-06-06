using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice1.Dtos.InputDto;
using Practice1.Models;

namespace Practice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly PRN231_Assignment2Context _context;

        public BookController(IMapper mapper, IConfiguration config, PRN231_Assignment2Context context)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var book = await _context.Books.ToListAsync();
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);

                if (book == null) { return NotFound("k tim thay id = " + id); }
                else
                {
                    return Ok(book);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertNewBook(BookInputDto b)
        {
            try
            {
                Book bo = new Book();
                //bo.Title = b.Title;
                //bo.Type = b.Type;
                //bo.PubId = b.PubId;
                //bo.Price = b.Price;
                //bo.Advance = b.Advance;
                //bo.Royalty = b.Royalty;
                //bo.YtdSales = b.YtdSales;
                //bo.Notes = b.Notes;
                //bo.PublishedDate = b.PublishedDate;
                bo = _mapper.Map<Book>(b);
                _context.Books.Add(bo);
                await _context.SaveChangesAsync();
                return Ok(bo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(BookUpdateInputDto b)
        {
            try
            {
                var bo = _context.Books.FirstOrDefault(x => x.BookId == b.BookId);
                if (bo == null) return BadRequest("khong ton tai id = " + b.BookId);
                else
                {
                    _mapper.Map(b, bo);

                    _context.Books.Update(bo);
                    await _context.SaveChangesAsync();
                    return Ok(b);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var bo = _context.Books.FirstOrDefault(x => x.BookId == id);
                if (bo == null) return BadRequest("khong ton tai id = " + id);
                else
                {
                    _context.Books.Remove(bo);
                    await _context.SaveChangesAsync();
                    return Ok("delete ok");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}
