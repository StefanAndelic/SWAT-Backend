using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWAT_Project_API.Data;
using SWAT_Project_API.Models;

namespace SWAT_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        SWATDbContext _dbContext;

        public BookingController(SWATDbContext dbContext)
        {
            _dbContext = dbContext;
        }


       
        [HttpPost]
        public IActionResult Post([FromForm] Booking bookingObj)
        {
            _dbContext.Bookings.Add(bookingObj);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }



        
    }
}