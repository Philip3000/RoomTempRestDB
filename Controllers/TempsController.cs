using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RoomTempRestDB.Repository;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoomTempRestDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempsController : ControllerBase
    {
        private readonly TempsRepository _repository;
        public TempsController(TempsRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<TempsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<RoomTemp>> Get()
        {
            List<RoomTemp> temps = _repository.GetAll();
            if (temps == null) return NotFound("No measurements exist");
            return Ok(temps);
        }

        // GET api/<TempsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<RoomTemp> Get(int id)
        {
            RoomTemp temp = _repository.GetById(id);
            if (temp == null)
                return NotFound("No temps with this id: "+ id);
            else
                return Ok(temp);
        }
        // Post api/<TempsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Custom routing for ikke at have problemer med HttpGet() requestens default routing
        [HttpPost("add")]
        public ActionResult<RoomTemp> Add([FromBody]RoomTemp value)
        {
            RoomTemp roomTemp = _repository.Add(value);
            if (roomTemp == null) return NotFound("Nothing found");
            return Ok(roomTemp);
        }

       
    }
}
