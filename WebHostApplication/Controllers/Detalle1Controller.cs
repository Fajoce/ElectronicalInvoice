using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebHostApplication.Data;
using WebHostApplication.Models;
using WebHostApplication.ViewModels.Detalles;
using WebHostApplication.ViewModels.Utilidad;

namespace WebHostApplication.Controllers
{
    public class Detalle1Controller : Controller
    {
        private readonly WebHostDbcontext _context;

        public Detalle1Controller(WebHostDbcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sql = @"
                    SELECT 
            d2.numeroFactura, 
            d2.fecha, 
            CONCAT(c.nombre, ' ', c.apellido1) AS ClienteCompleto,
            SUM(IFNULL(d2.subtotal, 0)) AS TotalSubtotal,
            SUM(IFNULL(d1.costo, 0)) AS TotalCosto,
            SUM(IFNULL(d2.subtotal, 0) - IFNULL(d1.costo, 0)) AS TotalUtilidad
        FROM detalle2 d2
        INNER JOIN detalle1 d1 ON d2.numeroFactura = d1.numeroFactura
        INNER JOIN clientes c ON c.idcliente = d2.idcliente
        GROUP BY 
            d2.numeroFactura, 
            d2.fecha, 
            ClienteCompleto;
        ";

            var facturas = await _context.Set<UtilidadFacturasDTO>()
                                         .FromSqlRaw(sql)
                                         .ToListAsync();

            // Calcular totales
            var totalSubtotal = facturas.Sum(f => f.TotalSubtotal);
            var totalCosto = facturas.Sum(f => f.TotalCosto);
            var totalUtilidad = facturas.Sum(f => f.TotalUtilidad);

            ViewBag.TotalSubtotal = totalSubtotal;
            ViewBag.TotalCosto = totalCosto;
            ViewBag.TotalUtilidad = totalUtilidad;


            return View(facturas);
        }
    }   
}

