using ItemRest.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItemRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IItemsRepository _repository;
        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<MembersController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Item>> Get()
        {
            int a =_repository.GetAll().Count;
            List<Item> listOfItems = _repository.GetAll();
            if(listOfItems.Count < 1)
            {
                return NoContent();
            }
            return Ok(listOfItems);
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Item> Get(int id)
        {
            Item? foundItem = _repository.GetById(id);
            if (foundItem == null)
            {
                return NotFound();
            }
            return Ok(foundItem);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Item> Post([FromBody] Item newItem)
        {
            try
            {
                Item createdItem = _repository.Add(newItem);
                int a = _repository.GetAll().Count;
                return Created($"api/Item/{createdItem.Id}", createdItem);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                                   ex is ArgumentOutOfRangeException ||
                                                   ex is ArgumentException)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Item> Put(int id, [FromBody] Item newData)
        {
            try
            {
                Item? updated = _repository.Update(id, newData);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                                   ex is ArgumentOutOfRangeException ||
                                                   ex is ArgumentException)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Item> Delete(int id)
        {
            Item? deleted = _repository.Delete(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }

        //sorting
        [HttpGet("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Item> Get([FromQuery] string name = null, [FromQuery] string sort_by = null)
        {
            return _repository.GetAllSort(name, sort_by);
        }
    }
}
