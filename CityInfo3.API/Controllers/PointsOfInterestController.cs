using CityInfo3.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo3.API.Controllers
{
    [Route(("api/cities/{cityId}/pointsofinterest"))]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        public object PointOfInterest { get; private set; }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointsOfInterest);
        }
        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(int cityId, int PointOfInterestId)
        {
            var city = CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            //find point of interest
            var pointsOfInterest = city.PointsOfInterest.FirstOrDefault(c => c.Id == cityId);

            if (pointsOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointsOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(
           int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            var city = CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var MaxPointOfInterestId = CitiesDataStore.Current.cities.SelectMany
                (c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterestDto = new PointOfInterestDto()
            {
                Id = ++MaxPointOfInterestId,
                Name = PointOfInterest.Name,
                Description = PointOfInterest.Description,

            };
            city.PointsOfInterest.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
            new
            {
                cityId = cityId,
                pointOfInterestId = finalPointOfInterest.Id
            },
            finalPointOfInterest);

        }

        [HttpPut("{pointofinterestid}")]
        public  ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            var city =  CitiesDataStore.Current.cities.FirstOrDefault(c => c.Id == cityId);
            if (city==null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore =  city.PointsOfInterest
               .FirstOrDefault(c=>c.Id== pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
    }
}
}
