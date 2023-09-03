using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SGF.Models;
using System.Diagnostics;

namespace SGF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult ShowData(/*[FromForm] IFormFile */ FileContacts FileExcel)
        {
            if (FileExcel.files.Length==0)
            {
                return BadRequest("No existe archivo");
            }
            var workbook = new XLWorkbook(FileExcel.files[0].OpenReadStream());
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
                                Nombre = row.Cell(1).ToString(),
                                Apellido = row.Cell(2).ToString(),
                                Telefono = row.Cell(3).ToString(),
                                Correo = row.Cell(4).ToString()
                            });
            }

            Console.WriteLine(FileExcel);
            return StatusCode(StatusCodes.Status200OK);
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