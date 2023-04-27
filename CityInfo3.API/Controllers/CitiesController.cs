using CityInfo3.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo3.API.Controllers
{
   
        [ApiController]
        [Route("api/cities")]
        public class CitiesController : ControllerBase
        {
            [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
            {
            
            return Ok(CitiesDataStore.Current.cities);
                   
            }
           [HttpGet]
           public ActionResult<CityDto>  GetCity(int id)
           {
            //find city
            var CityToReturn= new JsonResult(CitiesDataStore.Current.cities
                .FirstOrDefault(c => c.Id == id));
            if (CityToReturn == null)
            {
                return NotFound();
            }
            return Ok(CityToReturn);

           }
    }
}
