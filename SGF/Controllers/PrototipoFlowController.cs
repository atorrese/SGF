using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGF.Models;
using Bogus;

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
