using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SGF.Context;
using SGF.Models;
using System.Diagnostics;

namespace SGF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ShowData()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ShowData([FromForm] IFormFile  FileExcel)
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

            var contacts = new List<Contacto>();

            for (int i = firstRowUsed + 1 ; i <= lastRowUsed; i++)
            {
                var row = sheet.Row(i);
                contacts.Add(new Contacto
                {
                    Nombre = row.Cell(1).GetString(),
                    Apellido = row.Cell(2).GetString(),
                    Telefono = row.Cell(3).GetString(),
                    Correo = row.Cell(4).GetString()
                });

            }
            Console.WriteLine(FileExcel);
            return StatusCode(StatusCodes.Status200OK, contacts);
        }

        [HttpPost]
        public IActionResult SaveData([FromForm] IFormFile FileExcel)
        {
            try
            {
                if (FileExcel == null)
                {
                    return BadRequest("No existe archivo");
                }
                var workbook = new XLWorkbook(FileExcel.OpenReadStream());
                var sheet = workbook.Worksheet(1);

                var firstRowUsed = sheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
                var lastRowUsed = sheet.LastRowUsed().RangeAddress.FirstAddress.RowNumber;

                var contacts = new List<Contacto>();

                for (int i = firstRowUsed + 1; i <= lastRowUsed; i++)
                {
                    var row = sheet.Row(i);
                    Contacto contact = new Contacto
                    {
                        Nombre = row.Cell(1).GetString(),
                        Apellido = row.Cell(2).GetString(),
                        Telefono = row.Cell(3).GetString(),
                        Correo = row.Cell(4).GetString()
                    };
                    _context.Add(contact);
                    _context.SaveChanges();
                    contacts.Add(contact);
                }

                Console.WriteLine(FileExcel);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
            }
            catch (Exception ex)
            {
                return Json( new { mensaje = ex.Message } );
            }


        }

        public IActionResult Privacy()
        {
            /*Stream stream = FileExcel.OpenReadStream();
            IWorkbook MyExcel = null;

            if (Path.GetExtension(FileExcel.FileName) == ".xlsx")
            {
                MyExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MyExcel= new HSSFWorkbook(stream);
            }
            ISheet SheetExcel = MyExcel.GetSheetAt(0);
            int countRows = SheetExcel.LastRowNum;
            List<Contacto> Contacts = new List<Contacto>();

            for (int i = 1; i <= countRows; i++)
            {
                IRow row = SheetExcel.GetRow(i);
                Contacts.Add(new Contacto
                {
                    Nombre = row.GetCell(0).ToString(),
                    Apellido = row.GetCell(1).ToString(),
                    Telefono = row.GetCell(2).ToString(),
                    Correo = row.GetCell(3).ToString()
                });
            }*/
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}