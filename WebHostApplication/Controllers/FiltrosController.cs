using ClosedXML.Excel;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHostApplication.Data;
using WebHostApplication.Models;
using WebHostApplication.Models.Clientes;
using System.Globalization;


namespace WebHostApplication.Controllers
{
    public class FiltrosController : Controller
    {
        private readonly WebHostDbcontext _context;

        public FiltrosController(WebHostDbcontext context)
        {
            _context = context;
        }
        // GET: Facturas con filtros
        public async Task<IActionResult> Index(DateTime? fechaInicio, DateTime? fechaFin, int? clienteId, string formaPago)
        {
            var query = _context.Detalle2.OrderBy(d=>d.NumeroFactura)
                .Include(f => f.Cliente)
                .Include(f => f.Vendedor)
                .Include(f => f.Cajero)
                .AsQueryable();

            if (fechaInicio.HasValue)
                query = query.Where(f => f.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(f => f.Fecha <= fechaFin.Value);

            if (clienteId.HasValue)
                query = query.Where(f => f.IdCliente == clienteId.Value);

            if (!string.IsNullOrEmpty(formaPago))
            {
                switch (formaPago)
                {
                    case "Efectivo": query = query.Where(f => f.Efectivo > 0); break;
                    case "Tcredito": query = query.Where(f => f.Tcredito > 0); break;
                    case "Tdebito": query = query.Where(f => f.Tdebito > 0); break;
                    case "Transferencia": query = query.Where(f => f.Transferencia > 0); break;
                    case "Credito": query = query.Where(f => f.Credito > 0); break;
                }
            }

            // Guardar filtros en ViewBag
            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");
            ViewBag.ClienteId = clienteId;
            ViewBag.FormaPago = formaPago;

            // Combos
            ViewBag.Clientes = new SelectList(await _context.Cliente.ToListAsync(), "IdCliente", "Nombre", clienteId);
            ViewBag.FormasPago = new SelectList(new[] { "Efectivo", "Tcredito", "Tdebito", "Transferencia", "Credito" }, formaPago);

            // Proyectar a objetos donde los string nulos se reemplazan por string.Empty
            var facturas = await query
                .Select(f => new Detalle2
                {
                    NumeroFactura = f.NumeroFactura,
                    Fecha = f.Fecha,
                    Cliente = f.Cliente,
                    Efectivo = f.Efectivo ?? 0,
                    Tcredito = f.Tcredito ?? 0,
                    Tdebito = f.Tdebito ?? 0,
                    Transferencia = f.Transferencia ?? 0,
                    Credito = f.Credito ?? 0,
                    Subtotal = f.Subtotal ?? 0,
                    Mesa = f.Mesa ?? "",
                    Pedido = f.Pedido ?? "",
                    Devol = f.Devol ?? "",
                    TT = f.TT ?? "",
                    FechaHora = f.FechaHora ?? "",
                    Observacion = f.Observacion ?? ""
                })
                .ToListAsync();

            return View(facturas);
        }

        // ✅ Exportar a Excel
        public async Task<IActionResult> ExportarExcel(DateTime? fechaInicio, DateTime? fechaFin, int? clienteId, string formaPago)
        {
            var facturas = await FiltrarFacturas(fechaInicio, fechaFin, clienteId, formaPago);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Facturas");

            // ✅ Encabezados
            worksheet.Cell(1, 1).Value = "Número";
            worksheet.Cell(1, 2).Value = "Fecha";
            worksheet.Cell(1, 3).Value = "Cliente";
            worksheet.Cell(1, 4).Value = "Forma de Pago"; // nueva
            worksheet.Cell(1, 5).Value = "Monto";        // nueva
            worksheet.Cell(1, 6).Value = "Total";

            int row = 2;
            foreach (var f in facturas)
            {
                string forma = "";
                decimal monto = 0;

                if (f.Efectivo > 0) { forma = "Efectivo"; monto = f.Efectivo ?? 0; }
                else if (f.Tcredito > 0) { forma = "Tarjeta Crédito"; monto = f.Tcredito ?? 0; }
                else if (f.Tdebito > 0) { forma = "Tarjeta Débito"; monto = f.Tdebito ?? 0; }
                else if (f.Transferencia > 0) { forma = "Transferencia"; monto = f.Transferencia ?? 0; }
                else if (f.Credito > 0) { forma = "Crédito"; monto = f.Credito ?? 0; }

                worksheet.Cell(row, 1).Value = f.NumeroFactura.ToString() ?? "";
                worksheet.Cell(row, 2).Value = f.Fecha?.ToString("yyyy-MM-dd") ?? "";
                worksheet.Cell(row, 3).Value = f.Cliente?.Nombre ?? "";
                worksheet.Cell(row, 4).Value = forma ?? "";
                worksheet.Cell(row, 5).Value = monto;
                worksheet.Cell(row, 6).Value = f.Subtotal ?? 0;

                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Facturas.xlsx");
        }

        // ✅ Exportar a PDF con encabezado

      public async Task<IActionResult> ExportarPdf(DateTime? fechaInicio, DateTime? fechaFin, int? clienteId, string formaPago)
      {
            var culture = new CultureInfo("es-CO");
            var facturas = await FiltrarFacturas(fechaInicio, fechaFin, clienteId, formaPago);

            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont regular = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            // ✅ Título
            document.Add(new Paragraph("Listado de Facturas")
                .SetFont(bold)
                .SetFontSize(14)
                .SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph("\n"));

            // ✅ Tabla con columnas extras (6 columnas)
            var table = new Table(new float[] { 2, 3, 5, 3, 3, 3 }).UseAllAvailableWidth();
            table.AddHeaderCell(new Cell().Add(new Paragraph("Número").SetFont(bold)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Fecha").SetFont(bold)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Cliente").SetFont(bold)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Forma de Pago").SetFont(bold)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Monto").SetFont(bold)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Total").SetFont(bold)));

            decimal totalGeneral = 0;

            foreach (var f in facturas)
            {
                string forma = "";
                decimal monto = 0;

                if (f.Efectivo > 0) { forma = "Efectivo"; monto = f.Efectivo ?? 0; }
                else if (f.Tcredito > 0) { forma = "Tarjeta Crédito"; monto = f.Tcredito ?? 0; }
                else if (f.Tdebito > 0) { forma = "Tarjeta Débito"; monto = f.Tdebito ?? 0; }
                else if (f.Transferencia > 0) { forma = "Transferencia"; monto = f.Transferencia ?? 0; }
                else if (f.Credito > 0) { forma = "Crédito"; monto = f.Credito ?? 0; }

                // ✅ Agregar fila
                table.AddCell(new Cell().Add(new Paragraph(f.NumeroFactura.ToString()).SetFont(regular)));
                table.AddCell(new Cell().Add(new Paragraph(f.Fecha?.ToString("dd/MM/yyyy") ?? "").SetFont(regular)));
                table.AddCell(new Cell().Add(new Paragraph(f.Cliente?.Nombre ?? "").SetFont(regular)));
                table.AddCell(new Cell().Add(new Paragraph(forma).SetFont(regular)));
                table.AddCell(new Cell().Add(new Paragraph(monto.ToString("C", culture)).SetFont(regular)));
                table.AddCell(new Cell().Add(new Paragraph((f.Subtotal ?? 0).ToString("C",culture)).SetFont(regular)));

                totalGeneral += f.Subtotal ?? 0;
            }

            // ✅ Fila de total general
            var totalCell = new Cell(1, 5)
                .Add(new Paragraph("TOTAL GENERAL").SetFont(bold).SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell(totalCell);

            var totalValueCell = new Cell()
                .Add(new Paragraph(totalGeneral.ToString("C", culture)).SetFont(bold));
            table.AddCell(totalValueCell);

            document.Add(table);
            document.Close();

            return File(stream.ToArray(), "application/pdf", "Facturas.pdf");
        }

        // 🔧 Método privado para reutilizar el mismo filtro en Index/Exportar
        private async Task<List<Detalle2>> FiltrarFacturas(DateTime? fechaInicio, DateTime? fechaFin, int? clienteId, string formaPago)
        {
            var query = _context.Detalle2
                .Include(f => f.Cliente)
                .AsQueryable();

            if (fechaInicio.HasValue)
                query = query.Where(f => f.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(f => f.Fecha <= fechaFin.Value);

            if (clienteId.HasValue)
                query = query.Where(f => f.IdCliente == clienteId.Value);

            if (!string.IsNullOrEmpty(formaPago))
            {
                switch (formaPago)
                {
                    case "Efectivo": query = query.Where(f => f.Efectivo > 0); break;
                    case "Tcredito": query = query.Where(f => f.Tcredito > 0); break;
                    case "Tdebito": query = query.Where(f => f.Tdebito > 0); break;
                    case "Transferencia": query = query.Where(f => f.Transferencia > 0); break;
                    case "Credito": query = query.Where(f => f.Credito > 0); break;
                }
            }

            // ✅ Nueva proyección: reemplaza DBNull por valores seguros ANTES de materializar los datos
            return await query.Select(f => new Detalle2
            {
                NumeroFactura = f.NumeroFactura,
                Fecha = f.Fecha,
                IdCliente = f.IdCliente,
                Cliente = new Cliente
                {
                    IdCliente = f.Cliente.IdCliente,
                    Nombre = f.Cliente.Nombre ?? ""
                },
                Vendedor = f.Vendedor,
                Cajero = f.Cajero,
                Observacion = f.Observacion ?? "",
                Mesa = f.Mesa ?? "",
                Pedido = f.Pedido ?? "",
                Devol = f.Devol ?? "",
                FechaHora = f.FechaHora ?? "",
                TT = f.TT ?? "",
                Electronica = f.Electronica,
                Efectivo = f.Efectivo ?? 0,
                Tcredito = f.Tcredito ?? 0,
                Tdebito = f.Tdebito ?? 0,
                Transferencia = f.Transferencia ?? 0,
                Credito = f.Credito ?? 0,
                Subtotal = f.Subtotal ?? 0
            }).ToListAsync();
        }
    }
}
