using Microsoft.AspNetCore.Mvc;
using SP23.P01.Web.Data;
using SP23.P01.Web.Entities;
using System.Security.Principal;

namespace SP23.P01.Web.Controllers
{
    [ApiController]
    [Route("api/stations")]
    public class TrainStationsController : Controller
    {
        private readonly DataContext dataContext;

        public TrainStationsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public ActionResult<TrainStationDto[]> GetAll()
        {
            var trains = dataContext.Set<TrainStation>();

            return Ok(trains.Select(x => new TrainStationDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TrainStationDto> GetById(int id)
        {
            var trains = dataContext.Set<TrainStation>();

            var result = (trains
                .Where(x => x.Id == id)
                .Select(x => new TrainStationDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                }).FirstOrDefault());

            if (result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateTrainStation(TrainStationDto trainStationCreateDto)
        {
            var response = new HttpResponseMessage();

            if (trainStationCreateDto == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound();
            }

            if (trainStationCreateDto.Name != null && trainStationCreateDto.Name.Length > 120)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(trainStationCreateDto.Address?.Trim()))
            {
                response.StatusCode = System.Net.HttpStatusCode.Forbidden;
                return BadRequest(response);
            }

            if (string.IsNullOrEmpty(trainStationCreateDto.Name?.Trim()))
            {
                return BadRequest();
            }

            var stationToCreate = new TrainStation
            {
                Name = trainStationCreateDto.Name,
                Address = trainStationCreateDto.Address
            };

            dataContext.TrainStation.Add(stationToCreate);
            dataContext.SaveChanges();

            var stationToReturn = new TrainStationDto
            {
                Id = stationToCreate.Id,
                Name = trainStationCreateDto.Name,
                Address = trainStationCreateDto.Address
            };

            response.StatusCode = System.Net.HttpStatusCode.Created;
            return CreatedAtAction(nameof(GetById), new
            {
                id = stationToReturn.Id
            }, stationToReturn);
        }

        [HttpPut("{stationId:int}")]
        public ActionResult EditTrainStation([FromRoute] int stationId, [FromBody] TrainStationDto trainStationEditDto)
        {
            var stationToEdit = dataContext.TrainStation.FirstOrDefault(x => x.Id == stationId);

            if (trainStationEditDto == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(trainStationEditDto.Name?.Trim()))
            {
                return BadRequest();
            }

            if (trainStationEditDto.Name != null && trainStationEditDto.Name.Length > 120)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(trainStationEditDto.Address?.Trim()))
            {
                return BadRequest();
            }

            stationToEdit.Name = trainStationEditDto.Name;
            stationToEdit.Address = trainStationEditDto.Address;

            dataContext.SaveChanges();

            var stationToReturn = new TrainStationDto
            {
                Id = stationToEdit.Id,
                Name = trainStationEditDto.Name,
                Address = trainStationEditDto.Address
            };

            return Ok(stationToReturn);
        }

        [HttpDelete("{stationId:int}")]
        public ActionResult DeleteTrainStation([FromRoute] int stationId)
        {
            var stationToDelete = dataContext.TrainStation.FirstOrDefault(x => x.Id == stationId);

            if (stationToDelete == null)
            {
                return NotFound();
            }

            dataContext.TrainStation.Remove(stationToDelete);
            dataContext.SaveChanges();

            return Ok();
        }
    }
}


