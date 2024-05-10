using System;
using System.IO;
using System;
using System.Xml;
using System.Xml.Xsl;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
        // Crear un transformador XSLT
        XslCompiledTransform transformador = new XslCompiledTransform();

        // Cargar la plantilla XSLT (puedes proporcionar una ruta de archivo o cargarla desde una cadena)
        string xsltString = "<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'><xsl:template match='/documento'><html><head><title>Documento</title></head><body><h1><xsl:value-of select='titulo'/></h1><xsl:apply-templates/></body></html></xsl:template><xsl:template match='parrafo'><p><xsl:value-of select='.'/></p></xsl:template></xsl:stylesheet>";
        using (XmlReader reader = XmlReader.Create(new System.IO.StringReader(xsltString)))
        {
            transformador.Load(reader);
        }

        // Crear un lector XML para el XML de entrada
        using (XmlReader xmlReader = XmlReader.Create(new System.IO.StringReader(xmlString)))
        {
            // Crear un escritor de texto para el resultado HTML
            using (StringWriter sw = new StringWriter())
            {
                // Aplicar la transformación
                transformador.Transform(xmlReader, null, sw);

                // Devolver el HTML resultante como una cadena
                return sw.ToString();
            }
        }
    }
    static void ConvertirXmlAPdf(string xmlString, string pdfFilePath)
    {
        // Crear un documento PDF
        Document document = new Document();
        try
        {
            // Crear un escritor PDF
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));

            // Abrir el documento
            document.Open();

            // Crear un lector XML
            XmlReader xmlReader = XmlReader.Create(new StringReader(xmlString));

            // Leer el XML y agregar contenido al PDF
            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement())
                {
                    switch (xmlReader.Name)
                    {
                        case "titulo":
                            // Agregar título al PDF
                            Paragraph titulo = new Paragraph(xmlReader.ReadString());
                            titulo.Alignment = Element.ALIGN_CENTER;
                            document.Add(titulo);
                            break;
                        case "parrafo":
                            // Agregar párrafo al PDF
                            Paragraph parrafo = new Paragraph(xmlReader.ReadString());
                            document.Add(parrafo);
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al convertir XML a PDF: " + ex.Message);
        }
        finally
        {
            // Cerrar el documento
            document.Close();
        }

        Console.WriteLine("Archivo PDF generado exitosamente: " + pdfFilePath);
    }
    
}