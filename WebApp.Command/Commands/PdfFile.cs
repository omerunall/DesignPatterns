﻿using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Command.Commands
{

    public class PdfFile<T>
    {
        private readonly List<T> _listPdf;
        public readonly HttpContext _httpContext;

        public PdfFile(List<T> listPdf, HttpContext httpContext)
        {
            _listPdf = listPdf;
            _httpContext = httpContext;
        }

        public string FileName => $"{typeof(T).Name}.pdf";

        public string FileType => "application/octet-stream";

        public MemoryStream Create()
        {
            var type = typeof(T);

            var sb = new StringBuilder();

            sb.Append($@"<html>
                         <head></head>
                         <body>
                         <div class='text-center'<h1>{type.Name} tablo</h1></div>
                         <table class='table table-striped align='center'>");

            sb.Append("<tr>");
            type.GetProperties().ToList().ForEach(x =>
            {
                sb.Append($"<th>{x.Name}</th>");
            });
            sb.Append("</tr");


            _listPdf.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToList();
                sb.Append("<tr>");
                values.ForEach(x =>
                {
                    sb.Append($"<td>{values}</td>");
                });
                sb.Append("</tr");
            });
            sb.Append("</table></body></table>");

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },

                Objects = {
                new ObjectSettings() {
                    PagesCount = true,
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8" ,UserStyleSheet=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/lib/bootstrap/dist/css/bootstrapçcss")},
                    HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                                  }
                }
            };

            var converter = _httpContext.RequestServices.GetRequiredService<IConverter>();

            return new(converter.Convert(doc));


        }


    }
}
