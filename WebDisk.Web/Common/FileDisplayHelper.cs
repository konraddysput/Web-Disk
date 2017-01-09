using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebDisk.BusinessLogic.Common;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Web.Models.Home;

namespace WebDisk.Web.Common
{
    public static class FileDisplayHelper
    {
        public static FileResultViewModel ConvertFile(FileViewModel source)
        {
            string mimeType = MimeMapping.GetMimeMapping(source.FileName);
            byte[] content = ByteHelper.ReadToEnd(source.InputStream);
            var extension = Path.GetExtension(source.FileName);

            switch (extension)
            {
                case ".docx":
                case ".doc":
                    mimeType = "application/pdf";
                    content = ExportDocxToPdf(content, Convert.ToInt32(source.ContentLength));
                    break;

                case ".xlsx":
                case ".xls":
                    mimeType = "application/pdf";
                    content = ExportXlsToPdf(content, Convert.ToInt32(source.ContentLength));
                    break;
                default:
                    break;
            }


            return new FileResultViewModel()
            {
                ContentType = mimeType,
                Content = content
            };
        }

        private static byte[] ExportXlsToPdf(byte[] content, int contentLength)
        {
            // Create COM Objects
            Microsoft.Office.Interop.Excel.Application excelApplication;
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook;

            // Create new instance of Excel
            excelApplication = new Microsoft.Office.Interop.Excel.Application();

            excelApplication.ScreenUpdating = false;
            excelApplication.DisplayAlerts = false;

            var temporaryFilePath = CreateTemporaryField(content, contentLength);

            // Open the workbook that you wish to export to PDF
            excelWorkbook = excelApplication.Workbooks.Open(temporaryFilePath);

            // If the workbook failed to open, stop, clean up, and bail out
            if (excelWorkbook == null)
            {
                excelApplication.Quit();

                return new byte[0];
            }

            string path = $@"{HttpContext.Current.Server.MapPath("/App_Data/")}{Guid.NewGuid()}.pdf";
            excelWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, path);
            return ReturnFile(path);
        }

        private static string CreateTemporaryField(byte[] content, int lenght)
        {

            var tmpFile = Path.GetTempFileName();
            var tmpFileStream = File.OpenWrite(tmpFile);
            tmpFileStream.Write(content, 0, lenght);
            tmpFileStream.Close();
            return tmpFile;
        }

        private static byte[] ReturnFile(string path)
        {
            var result = File.ReadAllBytes(path);
            File.Delete(path);
            return result;
        }
        private static byte[] ExportDocxToPdf(byte[] content, int lenght)
        {
            Application appWord = new Application();

            var temporaryFilePath = CreateTemporaryField(content, lenght);
            var wordDocument = appWord.Documents.Open(temporaryFilePath);

            string path = $@"{HttpContext.Current.Server.MapPath("/App_Data/")}{Guid.NewGuid()}.pdf";
            wordDocument.ExportAsFixedFormat(path, WdExportFormat.wdExportFormatPDF);

            return ReturnFile(path);

        }
    }
}