﻿//#define INCLUDE_WEB_FUNCTIONS

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ESsearchTest
{
    //
    //  November 2013
    //  http://www.mikesknowledgebase.com
    //
    //  Note: if you plan to use this in an ASP.Net application, remember to add a reference to "System.Web", and to uncomment
    //  the "INCLUDE_WEB_FUNCTIONS" definition at the top of this file.
    //
    //  Release history
    //   - Nov 2013: 
    //        Changed "CreateExcelDocument(DataTable dt, string xlsxFilePath)" to remove the DataTable from the DataSet after creating the Excel file.
    //        You can now create an Excel file via a Stream (making it more ASP.Net friendly)
    //   - Jan 2013: Fix: Couldn't open .xlsx files using OLEDB  (was missing "WorkbookStylesPart" part)
    //   - Nov 2012: 
    //        List<>s with Nullable columns weren't be handled properly.
    //        If a value in a numeric column doesn't have any data, don't write anything to the Excel file (previously, it'd write a '0')
    //   - Jul 2012: Fix: Some worksheets weren't exporting their numeric data properly, causing "Excel found unreadable content in '___.xslx'" errors.
    //   - Mar 2012: Fixed issue, where Microsoft.ACE.OLEDB.12.0 wasn't able to connect to the Excel files created using this class.
    //

    public static class CreateExcelFile
    {
        // ReSharper disable once UnusedMember.Global
        public static bool CreateExcelDocument<T>(IEnumerable<T> list, string xlsxFilePath)
        {
            var ds = new DataSet();
            ds.Tables.Add(ListToDataTable(list));

            return CreateExcelDocument(ds, xlsxFilePath);
        }

        #region HELPER_FUNCTIONS

        //  This function is adapated from: http://www.codeguru.com/forum/showthread.php?t=450171
        //  My thanks to Carl Quirion, for making it "nullable-friendly".
        // ReSharper disable once MemberCanBePrivate.Global
        public static DataTable ListToDataTable<T>(IEnumerable<T> list)
        {
            var dt = new DataTable();

            foreach (var info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (var t in list)
            {
                var row = dt.NewRow();
                foreach (var info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[info.Name] = info.GetValue(t, null);
                    else
                        row[info.Name] = info.GetValue(t, null) ?? DBNull.Value;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        private static Type GetNullableType(Type t)
        {
            var returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }

        private static bool IsNullableType(Type type)
        {
            return type == typeof(string) ||
                   type.IsArray ||
                   (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        // ReSharper disable once UnusedMember.Global
        public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath)
        {
            var ds = new DataSet();
            ds.Tables.Add(dt);
            var result = CreateExcelDocument(ds, xlsxFilePath);
            ds.Tables.Remove(dt);
            return result;
        }

        #endregion

#if INCLUDE_WEB_FUNCTIONS
    /// <summary>
    /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
    /// </summary>
    /// <param name="dt">DataTable containing the data to be written to the Excel.</param>
    /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
    /// <param name="Response">HttpResponse of the current page.</param>
    /// <returns>True if it was created succesfully, otherwise false.</returns>
        public static bool CreateExcelDocument(DataTable dt, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                CreateExcelDocumentAsStream(ds, filename, Response);
                ds.Tables.Remove(dt);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        public static bool CreateExcelDocument<T>(List<T> list, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list));
                CreateExcelDocumentAsStream(ds, filename, Response);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="Response">HttpResponse of the current page.</param>
        /// <returns>Either a MemoryStream, or NULL if something goes wrong.</returns>
        public static bool CreateExcelDocumentAsStream(DataSet ds, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {
                    WriteExcelFile(ds, document);
                }
                stream.Flush();
                stream.Position = 0;

                Response.ClearContent();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                //  NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                //  manually added System.Web to this project's References.

                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                byte[] data1 = new byte[stream.Length];
                stream.Read(data1, 0, data1.Length);
                stream.Close();
                Response.BinaryWrite(data1);
                Response.Flush();
                Response.End();

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }
#endif //  End of "INCLUDE_WEB_FUNCTIONS" section

        /// <summary>
        ///     Create an Excel file, and write it to a file.
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="excelFilename">Name of file to be written.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public static bool CreateExcelDocument(DataSet ds, string excelFilename)
        {
            try
            {
                using (var document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(ds, document);
                }
                Trace.WriteLine("Successfully created: " + excelFilename);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
        {
            //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
            //  to a file, or writing to a MemoryStream.
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new Workbook();

            //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            var workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            var stylesheet = new Stylesheet();
            workbookStylesPart.Stylesheet = stylesheet;

            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            foreach (DataTable dt in ds.Tables)
            {
                //  For each worksheet you want to create
                var newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new Worksheet();

                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new SheetData());

                // save worksheet
                WriteDataTableToExcelWorksheet(dt, newWorksheetPart);
                newWorksheetPart.Worksheet.Save();

                // create the worksheet to workbook relation
                if (worksheetNumber == 1)
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new Sheets());

                spreadsheet.WorkbookPart.Workbook.GetFirstChild<Sheets>().AppendChild(new Sheet
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = worksheetNumber,
                    Name = dt.TableName
                });

                worksheetNumber++;
            }

            spreadsheet.WorkbookPart.Workbook.Save();
        }

        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            //
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            var numberOfColumns = dt.Columns.Count;
            var isNumericColumn = new bool[numberOfColumns];

            var excelColumnNames = new string[numberOfColumns];
            for (var n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //
            //  Create the Header row in our Excel Worksheet
            //
            uint rowIndex = 1;

            var headerRow = new Row {RowIndex = rowIndex}; // add a row at the top of spreadsheet
            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            sheetData.Append(headerRow);

            for (var colInx = 0; colInx < numberOfColumns; colInx++)
            {
                var col = dt.Columns[colInx];
                AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow);
                isNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") ||
                                          (col.DataType.FullName == "System.Int32");
            }

            //
            //  Now, step through each row of data in our DataTable...
            //
            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                var newExcelRow = new Row {RowIndex = rowIndex}; // add a row at the top of spreadsheet
                // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                sheetData.Append(newExcelRow);

                for (var colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    var cellValue = dr.ItemArray[colInx].ToString();

                    // Create cell with data
                    if (isNumericColumn[colInx])
                    {
                        //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                        //  If this numeric value is NULL, then don't write anything to the Excel file.
                        double cellNumericValue;
                        if (!double.TryParse(cellValue, out cellNumericValue)) continue;
                        cellValue = cellNumericValue.ToString(CultureInfo.InvariantCulture);
                        AppendNumericCell(excelColumnNames[colInx] + rowIndex, cellValue, newExcelRow);
                    }
                    else
                    {
                        //  For text cells, just write the input data straight out to the Excel file.
                        AppendTextCell(excelColumnNames[colInx] + rowIndex, cellValue, newExcelRow);
                    }
                }
            }
        }

        private static void AppendTextCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            var cell = new Cell {CellReference = cellReference, DataType = CellValues.String};
            var cellValue = new CellValue {Text = cellStringValue};
            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            cell.Append(cellValue);
            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            excelRow.Append(cell);
        }

        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            var cell = new Cell {CellReference = cellReference};
            var cellValue = new CellValue {Text = cellStringValue};
            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            cell.Append(cellValue);
            // ReSharper disable once PossiblyMistakenUseOfParamsMethod
            excelRow.Append(cell);
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //
            //  eg  GetExcelColumnName(0) should return "A"
            //      GetExcelColumnName(1) should return "B"
            //      GetExcelColumnName(25) should return "Z"
            //      GetExcelColumnName(26) should return "AA"
            //      GetExcelColumnName(27) should return "AB"
            //      ..etc..
            //
            if (columnIndex < 26)
                return ((char) ('A' + columnIndex)).ToString();

            var firstChar = (char) ('A' + columnIndex/26 - 1);
            var secondChar = (char) ('A' + columnIndex%26);

            return $"{firstChar}{secondChar}";
        }
    }
}