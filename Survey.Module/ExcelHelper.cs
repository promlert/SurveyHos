using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Module
{
    public class ExcelHelper
    {
        public static byte[] Export(DataTable dt, string sheetName)
        {
            DataView dv = dt.DefaultView;
            if (dv != null)
            {
                byte[] result = null;
                string Name = "";
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    
                using (ExcelPackage xls = new ExcelPackage())
                {
                    ExcelWorksheet ws = xls.Workbook.Worksheets.Add(sheetName);
                   // ws.Cells["A1"].LoadFromDataTable(dt, true);

                    Name = ws.Name;

                    if (dt.Rows.Count == 0)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                            ws.Cells[1, i + 1].Value = dt.Columns[i].ColumnName;
                    }
                    else
                    {

                        ws.Cells["A1"].LoadFromDataTable(dv.ToTable(), true);

                        int rowCount = dv.ToTable().Rows.Count;
                        var dateColumns = (from DataColumn d in dv.ToTable().Columns
                                           where d.DataType.Name == "DateTime"
                                           select d.Ordinal + 1);
                        foreach (int c in dateColumns)
                            ws.Cells[2, c, rowCount + 1, c].Style.Numberformat.Format = "dd/MM/yyyy";
                    }
                    result = xls.GetAsByteArray();

                }
                if (result != null)
                {
                    return result;

                }
                return null;
            }
            else
            {
                throw new DataException();
            }

        }
    }
}
