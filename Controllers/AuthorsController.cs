using Microsoft.AspNetCore.Mvc;
using TKW2.Data;
using TKW2.Domain;
using TKW2.Models.DTO;
using System.Linq;

namespace TKW2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AuthorsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all authors
        [HttpGet("get-all-authors")]
        public IActionResult GetAll()
        {
            var allAuthorsDomain = _dbContext.Authors;
            var allAuthorsDTO = allAuthorsDomain.Select(author => new AuthorDTO
            {
                Id = author.Id,
                FullName = author.FullName,
            }).ToList();
            return Ok(allAuthorsDTO);
        }

        // Get author by ID
        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById([FromRoute] int id)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(author => author.Id == id);
            if (authorDomain == null)
            {
                return NotFound();
            }

            var authorDTO = new AuthorDTO
            {
                Id = authorDomain.Id,
                FullName = authorDomain.FullName,
            };

            return Ok(authorDTO);
        }

        // Add author
        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorDTO authorDTO)
        {
            var authorDomainModel = new Author
            {
                FullName = authorDTO.FullName,
            };
            _dbContext.Authors.Add(authorDomainModel);
            _dbContext.SaveChanges();
            return Ok();
        }

        // Update author by ID
        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorDTO authorDTO)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(author => author.Id == id);
            if (authorDomain != null)
            {
                authorDomain.FullName = authorDTO.FullName;

                _dbContext.SaveChanges();
                return Ok(authorDTO);
            }

            return NotFound();
        }

        // Delete author by ID
        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(author => author.Id == id);
            if (authorDomain != null)
            {
                _dbContext.Authors.Remove(authorDomain);
                _dbContext.SaveChanges();
                return Ok();
            }

            return NotFound();
        }
    }
}
