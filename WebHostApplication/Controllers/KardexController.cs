using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebHostApplication.Data;

namespace WebHostApplication.Controllers
{
    public class KardexController : Controller
    {
        private readonly WebHostDbcontext _context;

        public KardexController(WebHostDbcontext context)
        {
            _context = context;
        }

        public IActionResult Index(string? productoId, int page = 1, int pageSize = 10)
        {
            // Base query
            var query = _context.Kardex.AsQueryable();

            // Filtro por producto si aplica
            if (!string.IsNullOrEmpty(productoId))
            {
                query = query.Where(k => k.ProductoId == productoId);
            }

            // Total de registros
            var totalRegistros = query.Count();

            // Paginación
            var lista = query
                .OrderByDescending(k => k.Fecha)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Productos para el combo
            var productos = _context.Productos
                .Select(p => new { p.Id, p.Descripcion })
                .OrderBy(p => p.Descripcion)
                .ToList();

            ViewBag.Productos = new SelectList(productos, "Id", "Descripcion", productoId);
            ViewBag.ProductoSeleccionado = productoId;

            // Datos de paginación
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRegistros / pageSize);

            return View(lista);
        }
        // GET: Kardex/Details/5
        public IActionResult Details(int id)
        {
            var item = _context.Kardex.FirstOrDefault(k => k.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}

