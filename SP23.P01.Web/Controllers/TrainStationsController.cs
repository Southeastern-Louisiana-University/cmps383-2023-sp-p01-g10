using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using SP23.P01.Web.Data;
using SP23.P01.Web.Entities;

namespace SP23.P01.Web.Controllers
{
    [ApiController]
    [Route("api/stations")]
    public class TrainStationsController : Controller
    {
        private readonly DataContext _dataContext;

        public TrainStationsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var response = new HttpResponseMessage();

            var stationsToReturn = _dataContext
                .TrainStation
                .Select(item => new TrainStationDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address
                })
                .ToList();
            
            if (stationsToReturn.Capacity == 0)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return BadRequest(response);
            }
            
            // response. = stationsToReturn;
            return Ok(response);
        }

        [HttpGet("{stationId:int}")]
        public ActionResult GetTrainStation([FromRoute] int stationId)
        {
            var response = new HttpResponseMessage();

            var stationFromDb  = _dataContext.TrainStation.FirstOrDefault(x => x.Id == stationId);

            if (stationFromDb == null) 
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return BadRequest(response);
            }

            var stationToReturn = new TrainStationDto
            {
                Id = stationFromDb.Id,
                Name = stationFromDb.Name,
                Address = stationFromDb.Address
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult CreateStation(TrainStationDto trainStationCreateDto)
        {
            var response = new HttpResponseMessage();

            if (trainStationCreateDto == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            if (string.IsNullOrEmpty(trainStationCreateDto.Name?.Trim()))
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            if (string.IsNullOrEmpty(trainStationCreateDto.Address?.Trim()))
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            if (trainStationCreateDto.Name != null && trainStationCreateDto.Name.Length > 120)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(response);
            }

            var stationToCreate = new TrainStation
            {
                Name = trainStationCreateDto.Name,
                Address = trainStationCreateDto.Address
            };

            _dataContext.TrainStation.Add(stationToCreate);
            _dataContext.SaveChanges();

            var stationToReturn = new TrainStationDto
            {
                Id = stationToCreate.Id,
                Name = trainStationCreateDto.Name,
                Address = trainStationCreateDto.Address
            };

            response.StatusCode = System.Net.HttpStatusCode.Created;
            return Ok(response.StatusCode);
        }

        [HttpPut("{stationId:int}")]
        public ActionResult EditStation([FromRoute] int stationId, [FromBody] TrainStationDto trainStationEditDto)
        {
            var response = new HttpResponseMessage();

            var stationToEdit = _dataContext.TrainStation.FirstOrDefault(x => x.Id == stationId);

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

            _dataContext.SaveChanges();

            var stationToReturn = new TrainStationDto
            {
                Id = stationToEdit.Id,
                Name = trainStationEditDto.Name,
                Address = trainStationEditDto.Address
            };

            return Ok(response);
        }

        [HttpDelete("{stationId:int}")]
        public ActionResult DeleteStation([FromRoute] int stationId)
        {
            var response = new HttpResponseMessage();

            var stationToDelete = _dataContext.TrainStation.FirstOrDefault(x => x.Id == stationId);

            if (stationToDelete == null)
            {
                // response
                return BadRequest(response);
            }

            _dataContext.TrainStation.Remove(stationToDelete);
            _dataContext.SaveChanges();

            return Ok(response);
        }
    }
}


