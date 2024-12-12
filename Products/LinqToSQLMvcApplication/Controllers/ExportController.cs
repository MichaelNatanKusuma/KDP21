using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using System.IO;

namespace LinqToSQLMvcApplication.Controllers
{
    public class ExportController : Controller
    {
        // GET: Export
        public ActionResult ExportToExcel()
        {
            // Create a new workbook
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];

            // Add some data to the worksheet
            sheet.Cells["A1"].PutValue("ID");
            sheet.Cells["B1"].PutValue("Name");
            sheet.Cells["C1"].PutValue("Price");

            sheet.Cells["A2"].PutValue(1);
            sheet.Cells["B2"].PutValue("Product A");
            sheet.Cells["C2"].PutValue(10.0);

            sheet.Cells["A3"].PutValue(2);
            sheet.Cells["B3"].PutValue("Product B");
            sheet.Cells["C3"].PutValue(20.0);

            // Save the workbook to a memory stream
            MemoryStream stream = new MemoryStream();
            workbook.Save(stream, SaveFormat.Xlsx);

            // Return the file as a download
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
        }
    }
}