using Flats4us.Entities;
using Flats4us.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        public readonly Flats4usContext _context;
        private readonly ILogger<OfferController> _logger;
        private readonly IOfferService _paginatorService;

        public OfferController(ILogger<OfferController> logger,
                                IOfferService paginatorStudentService)
        {
            _logger = logger;
            _paginatorService = paginatorStudentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetOffer(int skipCount, int maxResoultCount, string? wojewodzto, string? miejscowosc, int? odleglosc, int? cenaMin, int? cenaMax, string? dzielnica, int? powierzchniaOd, int? powierzchniaDo, int? LataBudowy)
        {
            if (_paginatorService == null) 
            {
                return NotFound(); 
            }

            var filtr = _context.Offers
                .Where(x => x.Property.Province == wojewodzto)
                .Where(x => x.Property.City==miejscowosc)
                //.Where(x => x.Property.==odleglosc)
                //.Where(x => x.Property.==cenaMin)
                //.Where(x => x.Property.City==cenaMax)
                .Where(x => x.Property.District==dzielnica)
                //.Where(x => x.Property.City==powierzchniaDo)
                //.Where(x => x.Property.City==powierzchniaOd)
                .Where(x => x.Property.ConstructionYear == LataBudowy);

            return Ok(filtr);
            
        }
        
    
    }
}
