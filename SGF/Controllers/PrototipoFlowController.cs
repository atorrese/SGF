using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGF.Models;
using Bogus;
using ClosedXML.Excel;

namespace SGF.Controllers
{
    public class PrototipoFlowController : Controller
    {
        // GET: PrototipoFlowController
        public ActionResult Index()
        {
            var faker = new Faker();
            List<FlowPedido> pedidos = new List<FlowPedido>();
            string[] aprobadores = { "A.Logistica", "A.Gte Linea", "A.Gte Unidad", "A.Gte Gral", "A.Directorio" };
            for (int i = 0; i < 10; i++)
            {
                int contNumero = i.ToString().Length;
                string secuencia = new string('0',7-contNumero);
                int secuencial = i + 1;
                int indiceAprobador = faker.Random.Int(0, 4);
                FlowPedido pedido = new FlowPedido
                {
                    Id = i,
                    NumeroPedido = $"PE{secuencia}{secuencial}",
                    Titulo = faker.Lorem.Slug(),
                    Proveedor = faker.Company.CompanyName(),
                    FechaAlmacen = faker.Date.Future(),
                    Fecha = faker.Date.Future(),
                    FechaDisponible = faker.Date.Future(),
                    Urgente = faker.Random.Bool(),
                    Total = (double)faker.Finance.Amount(), 
                    CantidadAprobaciones = indiceAprobador + 1,
                    TextoCantidadAprobaciones = aprobadores[indiceAprobador],
                    Estado = faker.Random.Int(1,5)
                };
                pedido.CantidadAprobaciones = pedido.Estado == 4 ? 5 : pedido.Estado; 
                pedido.Estado = pedido.CantidadAprobaciones==5?4:pedido.Estado;
                
                pedidos.Add(pedido);
            }
            return View(pedidos);
        }

        public IActionResult LoadData([FromForm] IFormFile fileExcel, string country, string sql_data)
        {
            try
            {
                List<string> errors = new List<string>();
                string json = (sql_data != null) ? sql_data : "";
                if (sql_data == null)
                {
                    var workbook = new XLWorkbook(fileExcel.OpenReadStream());
                    var sheet = workbook.Worksheet(1);
                    var firstRowUsed = sheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
                    var lastRowUsed = sheet.LastRowUsed().RangeAddress.FirstAddress.RowNumber;
                    for (int i = firstRowUsed + 1; i <= lastRowUsed; i++)
                    {
                        string? message = null;
                        var row = sheet.Row(i);
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new
                {
                    errors,
                    // controles = controlesCalidad,
                    // correct_records = controlesCalidadCorrectas,
                    // incorrect_records = controlesCalidadIncorrectas,
                    // number_of_correct_records = controlesCalidadCorrectas.Count(),
                    // number_of_incorrect_records = controlesCalidadIncorrectas.Count(),
                    country,
                    sql_data= json
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: PrototipoFlowController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrototipoFlowController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrototipoFlowController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrototipoFlowController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrototipoFlowController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrototipoFlowController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrototipoFlowController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
