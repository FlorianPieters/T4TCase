using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T4TCase.Data;
using T4TCase.Model;

namespace T4TCase.Controllers
{
    public class LogonController : Controller
    {
        private readonly DatabaseContext context;
        public LogonController(){}

        public LogonController(DatabaseContext _context)
        {
            context = _context;
        }

        public IActionResult Logon()
        {
            return View();
        }
    }
}