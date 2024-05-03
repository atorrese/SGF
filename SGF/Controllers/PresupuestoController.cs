using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGF.Context;
using SGF.Models;

namespace SGF.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly AppDbContext _context;
        public PresupuestoController(AppDbContext context)
        {
            _context = context;
        }
        // GET: PresupuestoController1
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ShowData([FromForm] IFormFile FileExcel)
        {
            /*if (FileExcel.Length==0)
            {
                return BadRequest("No existe archivo");
            }*/
            var workbook = new XLWorkbook(FileExcel.OpenReadStream());
            var sheet = workbook.Worksheet(1);
            var sheetName = sheet.Name;

            var firstRowUsed = sheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
            var lastRowUsed = sheet.LastRowUsed().RangeAddress.FirstAddress.RowNumber;

            var contacts = new List<Presupuesto>();

            for (int i = firstRowUsed + 1; i <= lastRowUsed; i++)
            {
                var row = sheet.Row(i);
                contacts.Add(new Presupuesto
                {
                    Cliente = row.Cell(1).GetString(),
                    Anio = 2023, //row.Cell(2).GetString(),
                    Mes = 9, //row.Cell(3).GetString(),
                    Monto = row.Cell(3).GetDouble(), //row.Cell(4).GetString()
                });

            }
            Console.WriteLine(FileExcel);
            return StatusCode(StatusCodes.Status200OK, contacts);
        }

        [HttpPost]
        public IActionResult SaveData([FromForm] IFormFile FileExcel)
        {
            if (FileExcel == null)
            {
                return BadRequest("No existe archivo");
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var workbook = new XLWorkbook(FileExcel.OpenReadStream());
                    var sheet = workbook.Worksheet(1);

                    var firstRowUsed = sheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
                    var lastRowUsed = sheet.LastRowUsed().RangeAddress.FirstAddress.RowNumber;

                    var contacts = new List<Presupuesto>();

                    for (int i = firstRowUsed + 1; i <= lastRowUsed; i++)
                    {
                        var row = sheet.Row(i);
                        Presupuesto contact = new Presupuesto
                        {
                            Cliente = row.Cell(1).GetString(),
                            Anio = 2023,
                            Mes = 9,
                            Monto = row.Cell(3).GetDouble(),
                        };
                        contacts.Add(contact);
                    }
                    _context.AddRange(contacts);
                    _context.SaveChanges();
                    transaction.Commit();
                    Console.WriteLine(FileExcel);
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { mensaje = ex.Message });
                }
            }
        }

    }
}
