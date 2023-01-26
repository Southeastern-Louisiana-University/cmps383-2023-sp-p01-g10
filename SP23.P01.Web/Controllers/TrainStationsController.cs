using Microsoft.AspNetCore.Mvc;
using SP23.P01.Web.Data;
using SP23.P01.Web.Entities;

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
                //result = NotFound();
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
                return BadRequest(response);
            }

            if (trainStationCreateDto.Name != null && trainStationCreateDto.Name.Length > 120)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(response);
            }

            if (string.IsNullOrEmpty(trainStationCreateDto.Address?.Trim()))
            {
                response.StatusCode = System.Net.HttpStatusCode.Forbidden;
                return BadRequest(response);
            }
            
            if (string.IsNullOrEmpty(trainStationCreateDto.Name?.Trim()))
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(response);
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
            return Ok(response);
        }

        [HttpPut("{stationId:int}")]
        public ActionResult EditTrainStation([FromRoute] int stationId, [FromBody] TrainStationDto trainStationEditDto)
        {
            var response = new HttpResponseMessage();

            var stationToEdit = dataContext.TrainStation.FirstOrDefault(x => x.Id == stationId);

            if (trainStationEditDto == null)
            {
                // response
                return BadRequest(response);
            }

            if (string.IsNullOrEmpty(trainStationEditDto.Name?.Trim()))
            {
                // response
            }

            if (trainStationEditDto.Name != null && trainStationEditDto.Name.Length > 120)
            {
                // response
            }

            if (string.IsNullOrEmpty(trainStationEditDto.Address?.Trim()))
            {
                // response
                return BadRequest(response);
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

            return Ok(response);
        }

        [HttpDelete("{stationId:int}")]
        public ActionResult DeleteTrainStation([FromRoute] int stationId)
        {
            var response = new HttpResponseMessage();

            var stationToDelete = dataContext.TrainStation.FirstOrDefault(x => x.Id == stationId);

            if (stationToDelete == null)
            {
                // response
                return BadRequest(response);
            }

            dataContext.TrainStation.Remove(stationToDelete);
            dataContext.SaveChanges();

            return Ok(response);
        }
    }
}


