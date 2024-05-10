using Aspose.Words;
using System;

using Microsoft.AspNetCore.Mvc;
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
        string documentoHtml = ConvertirXmlAHtml(documentos[0].DocXml);
        Console.WriteLine(documentoHtml);
        return View(documentos);
    }

    static string ConvertirXmlAHtml(string xmlString)
    {
        // Crear un documento de Words desde XML
        Document doc = new Document();
        DocumentBuilder builder = new DocumentBuilder(doc);
        builder.InsertHtml(xmlString);

        // Guardar el documento como HTML en un stream de memoria
        using (MemoryStream stream = new MemoryStream())
        {
            doc.Save(stream, SaveFormat.Html);
            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }
    }
    
}