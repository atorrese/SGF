using System;
using System.Xml;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SGF.Context;
using SGF.Models;

namespace SGF.Controllers;

public class DocumentoController : Controller
{
    private readonly AppDbContext _context;
    
    public DocumentoController(AppDbContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        List<Documento> documentos = new List<Documento>();
        documentos = _context.Documento.ToList();
        Autorizacion documentoHtml = XmlToJson(documentos[0].DocXml);
        Console.WriteLine(documentoHtml);
        return View(documentos);
    }

    static Autorizacion XmlToJson(string xmlString)
    {
        // Cargar la cadena XML en un documento XML
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlString);

        Autorizacion autorizacion = new Autorizacion
        {
            estado = doc["autorizacion"]["estado"].InnerText,
            ambiente = doc["autorizacion"]["ambiente"].InnerText,
            comprobante = doc["autorizacion"]["comprobante"].InnerText,
            fechaAutorizacion = doc["autorizacion"]["fechaAutorizacion"].InnerText,
            numeroAutorizacion = doc["autorizacion"]["numeroAutorizacion"].InnerText,
            mensaje = doc["autorizacion"]["mensaje"]?.InnerText,
        };
        
        XmlDocument factura = new XmlDocument();
        factura.LoadXml(autorizacion.comprobante);
        
        /*string documentoJson = JsonConvert.SerializeXmlNode(doc["autorizacion"]);
        
        Dictionary<string, Autorizacion> documentoDictionary = JsonConvert.DeserializeObject<Dictionary<string, Autorizacion>>(documentoJson);

        string? xmlComprobante = (string)documentoDictionary["autorizacion"].comprobante["#cdata-section"];
        XmlDocument docComprobante = new XmlDocument();
        docComprobante.LoadXml(xmlComprobante);
        string comprobanteJson = JsonConvert.SerializeXmlNode(doc["autorizacion"]);*/
        //XmlDocument comprobante = new XmlDocument();
        //comprobante.LoadXml(xmlString);
        
        // Convertir el documento XML a una cadena JSON
        return autorizacion;
    }
    
}