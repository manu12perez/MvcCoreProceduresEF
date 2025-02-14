using Microsoft.AspNetCore.Mvc;
using MvcCoreProceduresEF.Models;
using MvcCoreProceduresEF.Repositories;

namespace MvcCoreProceduresEF.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryDoctores repo;

        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DoctoresEspecialidad()
        {
            ViewData["especialidades"] = await this.repo.GetEspecialidadesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoctoresEspecialidad(string especialidad, int aumento)
        {
            List<Doctor> doctores= await this.repo.AumentarSalarioAsync(especialidad, aumento);
            ViewData["especialidades"] = await this.repo.GetEspecialidadesAsync();
            return View(doctores);
        }
    }
}
