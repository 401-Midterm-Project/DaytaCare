using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaytaCare.Models;
using DaytaCare.Models.DTO;
using DaytaCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DaytaCare.Controllers
{ 
    [Authorize(Roles = "Administrator, Parent")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly IParentRepository daycares;

        public ParentsController(IParentRepository daycares)
        {
            this.daycares = daycares;
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<List<DaycareDTO>>> Search([FromQuery]ParentSearchDto filter)
        {
            return await daycares.Search(filter);
        }

        [Route("daycare/{id}")]
        [HttpGet]
        public async Task<ActionResult<DaycareDTO>> GetDaycare(int id)
        {
            var daycare = await daycares.GetById(id);

            if (daycare == null)
            {
                return NotFound();
            }

            return daycare;
        }

    }
}
