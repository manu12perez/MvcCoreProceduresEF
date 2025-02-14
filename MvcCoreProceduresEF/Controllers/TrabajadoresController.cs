using Microsoft.AspNetCore.Mvc;
using MvcCoreProceduresEF.Models;
using MvcCoreProceduresEF.Repositories;

namespace MvcCoreProceduresEF.Controllers
{
    public class TrabajadoresController : Controller
    {
        private RepositoryTrabajadores repo;

        public TrabajadoresController(RepositoryTrabajadores repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            TrabajadoresModel model = await this.repo.GetTrabajadoresAsync();
            List<string> oficios = await this.repo.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string oficio)
        {
            TrabajadoresModel model = await this.repo.GetTrabajadoresModelOficioAsync(oficio);
            List<string> oficios = await this.repo.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;
            return View(model);
        } 
    }
}
