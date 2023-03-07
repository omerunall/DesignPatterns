using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Command.Commands;
using WebApp.Command.Models;

namespace WebApp.Command.Controllers
{

    public class ProductsController : Controller
    {
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public ProductsController(AppIdentityDbContext appIdentityDbContext)
        {
            _appIdentityDbContext = appIdentityDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _appIdentityDbContext.Products.ToListAsync());
        }
        public async Task<IActionResult> CreateFile(int type)
        {
            var products = await _appIdentityDbContext.Products.ToListAsync();

            FileCreateInvoker fileCreateInvoker = new();


            EFileType eFileType = (EFileType)type;

            switch (eFileType)
            {
                case EFileType.Excel:
                    ExcelFile<Product> excel = new(products);

                    fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excel));

                    break;
                case EFileType.Pdf:
                    PdfFile<Product> pdfFile = new(products, HttpContext);

                    fileCreateInvoker.SetCommand(new CreatePdfTableActionCommand<Product>(pdfFile));
                    break;
                default:
                    break;
            }
            return fileCreateInvoker.CreateFile();

        }

        public async Task<IActionResult> CreateFiles()
        {
            var products = await _appIdentityDbContext.Products.ToListAsync();

            ExcelFile<Product> excel = new(products);

            PdfFile<Product> pdfFile = new(products, HttpContext);

            FileCreateInvoker fileCreateInvoker = new();

            fileCreateInvoker.AddCommand(new CreateExcelTableActionCommand<Product>(excel));
            fileCreateInvoker.AddCommand(new CreatePdfTableActionCommand<Product>(pdfFile));

            var filesResult = fileCreateInvoker.CreateFiles();

            using (var zipMemoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create))
                {
                    foreach (var item in filesResult)
                    {
                        var fileContent = item as FileContentResult;

                        var zipFİle = archive.CreateEntry(fileContent.FileDownloadName);

                        using (var zipEntiStream = zipFİle.Open())
                        {
                            await new MemoryStream(fileContent.FileContents).CopyToAsync(zipEntiStream);
                        }
                    }
                }

                return File(zipMemoryStream.ToArray(),"application/zip","all.zip");
            }


        }
    }
}
