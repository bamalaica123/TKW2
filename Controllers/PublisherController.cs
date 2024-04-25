using Microsoft.AspNetCore.Mvc;
using TKW2.Data;
using TKW2.Domain;
using TKW2.Models.DTO;
using System.Linq;

namespace TKW2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public PublishersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all publishers
        [HttpGet("get-all-publishers")]
        public IActionResult GetAll()
        {
            var allPublishersDomain = _dbContext.Publishers;
            var allPublishersDTO = allPublishersDomain.Select(publisher => new PublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name,
            }).ToList();
            return Ok(allPublishersDTO);
        }

        // Get publisher by ID
        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById([FromRoute] int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(publisher => publisher.Id == id);
            if (publisherDomain == null)
            {
                return NotFound();
            }

            var publisherDTO = new PublisherDTO
            {
                Id = publisherDomain.Id,
                Name = publisherDomain.Name,
            };

            return Ok(publisherDTO);
        }

        // Add publisher
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherDTO publisherDTO)
        {
            var publisherDomainModel = new Publishers
            {
                Name = publisherDTO.Name,
            };
            _dbContext.Publishers.Add(publisherDomainModel);
            _dbContext.SaveChanges();
            return Ok();
        }

        // Update publisher by ID
        [HttpPut("update-publisher-by-id/{id}")]
        public IActionResult UpdatePublisherById(int id, [FromBody] PublisherDTO publisherDTO)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(publisher => publisher.Id == id);
            if (publisherDomain != null)
            {
                publisherDomain.Name = publisherDTO.Name;

                _dbContext.SaveChanges();
                return Ok(publisherDTO);
            }

            return NotFound();
        }

        // Delete publisher by ID
        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(publisher => publisher.Id == id);
            if (publisherDomain != null)
            {
                _dbContext.Publishers.Remove(publisherDomain);
                _dbContext.SaveChanges();
                return Ok();
            }

            return NotFound();
        }
    }
}
