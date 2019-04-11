using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace WealthManager.Exporter
{
    public static class CExcel
    {
        public static void Export(string filename, string[,] data)
        {
            Application xlApp;
            Workbook xlWb;
            Worksheet xlWs;

            xlApp = new Application();
            xlWb = xlApp.Workbooks.Add(System.Reflection.Missing.Value);
            xlWs = (Worksheet)xlWb.Sheets["Sheet1"];

            for (int iRow = 0; iRow < data.GetLength(0); iRow++)
            {
                for (int iCol = 0; iCol < data.GetLength(1); iCol++)
                {
                    xlWs.Cells[iRow + 1, iCol + 1] = data[iRow, iCol];
                }
            }

            xlWs.Columns.AutoFit();
            xlWs.UsedRange.Borders.LineStyle = XlLineStyle.xlContinuous;
            xlApp.DisplayAlerts = false;


            //Save xlsx
            xlWs.SaveAs(filename);

            //Save as pdf
            xlWs.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF,
                                    filename.Substring(0, filename.Length - 5));

            xlWb.Close();
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWs);
            Marshal.ReleaseComObject(xlWb);
            Marshal.ReleaseComObject(xlApp);

        }
    }
}