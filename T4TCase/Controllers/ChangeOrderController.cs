using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using T4TCase.Model;
using T4TCase.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T4TCase.Controllers
{
    [Authorize]
    public class ChangeOrderController : Controller
    {
        private readonly DatabaseContext _context;

        public ChangeOrderController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Change()
        {

            return View();
        }
    }
}
