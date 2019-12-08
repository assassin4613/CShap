using System;
using System.Collections.Generic;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelToTableData
{
    class ExcelManager
    {
        Excel.Application app_ = null;
        Excel.Workbook book_ = null;
        Excel.Worksheet sheet_ = null;

        public ExcelManager()
        {
            app_ = new Excel.Application();
            app_.Visible = false;
        }

        ~ExcelManager()
        {
            if (book_ != null)
                book_.Close();

            app_.Quit();
        }

        string TranslateColumnIndexToName(int index)
        {
            int quotient = (index) / 26;

            if (quotient > 0)
                return TranslateColumnIndexToName(quotient - 1) + (char)((index % 26) + 65);
            else
                return "" + (char)((index % 26) + 65);
        }

        public int Open(string file_name)
        {
            if(book_ != null)
            {
                book_.Close();
                book_ = null;
            }

            try
            {
                book_ = app_.Workbooks.Open(file_name);
            }
            catch(Exception ex)
            {
                Console.WriteLine("\t\t\t Excel file \"Open\" Error : " + file_name);
                Console.WriteLine(ex);
                return 0;
            }

            return book_.Sheets.Count;
        }

        public string GetSheetName(int sheetOrder)
        {
            return book_.Sheets[sheetOrder].Name;
        }

        public bool Get(int sheet_num, out string[,] values, ref int row, ref int col)
        {
            if (book_ == null || book_.Sheets.Count < sheet_num)
            {
                values = null;
                return false;
            }

            Array array_data;
            try
            {
                sheet_ = book_.Sheets[sheet_num];
                row = sheet_.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
                col = sheet_.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Column;
                array_data = (System.Array)sheet_.get_Range("A1", TranslateColumnIndexToName(col) + row).Cells.Value;

                values = new string[row, col];
                for (int y = 1; y < row + 1; ++y)
                {
                    for (int x = 1; x < col + 1; ++x)
                    {
                        if (array_data.GetValue(y, x) == null)
                            continue;

                        values[y - 1, x - 1] = array_data.GetValue(y, x).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                values = null;
                Console.WriteLine("\t\t\t Excel data \"Get\" Error : row - " + values.GetLength(0) + "\t col " + values.GetLength(1));
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

    }

    partial class Program
    {
        const int _usedorNotRow = 0;
        const int _schemeTypeRow = 1;
        const int _schemeNameRow = 2;
        const int _startDataRow = 2;

        class ArrayVariable
        {
            public string type;
            public string name;
            public int count;
        }

        Dictionary<int, List<string>> _checkedValue = new Dictionary<int, List<string>>();
        Dictionary<string, string[,]> _checkedValues = new Dictionary<string, string[,]>();
        Dictionary<string, Dictionary<string, int>> _enumTables = new Dictionary<string, Dictionary<string, int>>();

        string[] _args = null;

        static string GetConvertString(string type)
        {
            if (type == "int")
                return "ToInt(it);";
            else if (type == "short")
                return "ToInt(it);";
            else if (type == "float")
                return "ToFloat(it);";
            else if (type == "string")
                return "ToString(it);";
            else if (type == "char")
                return "ToString(it);";
            else
                return "static_cast<" + type + ">(ToInt(it));";
        }

        void Generate(string[] args)
        {
            _args = args;

            if(_args.Length == 0)
            {
                Console.WriteLine("version 정보를 입력해 주세요.");
                return;
            }

            //ver
            string version = _args[0];

            //out
            string out_path;
            string current_path = Directory.GetCurrentDirectory();

            if (_args.Length > 2 && _args[2] != ".")
                out_path = args[2];
            else
                out_path = current_path;

            //in
            string in_path;

            if (_args.Length > 1 && _args[1] != ".")
                in_path = _args[1];
            else
                in_path = current_path;

            //file list
            string[] excel_files;

            if(_args.Length > 3)
            {
                excel_files = _args[3].Split(',');
            }
            else
            {
                string[] files =
                {
                    "_Creature.xlsm",
                };
                excel_files = files;
            }

            Console.WriteLine("Output Path = " + out_path);
            ExcelManager mgr = new ExcelManager();

            int row = 0;
            int col = 0;

            //Generate Enum Code
            string sheet_name ="";

            Console.WriteLine("");
            Console.WriteLine("[Start allocation]");

            //Make valid container
            foreach(string file in excel_files)
            {
                Console.WriteLine("\t" + file);
                int sheet_count = mgr.Open(in_path + "\\" + file);

                for(int i = 1; i <= sheet_count; ++i)
                {
                    sheet_name = mgr.GetSheetName(i);

                    string[,] values;

                    mgr.Get(i, out values, ref row, ref col);
                    Console.WriteLine("\t\t" + sheet_name);

                    if (false == _checkedValues.ContainsKey(sheet_name))
                        _checkedValues.Add(sheet_name, values);
                }
            }

            Console.WriteLine("[Complete allocation]");
            Console.WriteLine("");
            Console.WriteLine("[Start Code Generate]");

            foreach (string file in excel_files)
            {
                Console.WriteLine("\t" + file);
                int sheet_count = mgr.Open(in_path + "\\" + file);

                for (int i = 1; i <= sheet_count; ++i)
                {
                    sheet_name = mgr.GetSheetName(i);

                    Console.WriteLine("\t\t" + sheet_name);

                    string[,] values = _checkedValues[sheet_name];
                    row = _checkedValues[sheet_name].GetLength(0);
                    col = _checkedValues[sheet_name].GetLength(1);

                    CodeGeneratorForClient(out_path, sheet_name, values, row, col, version);
                }
            }

            Console.WriteLine("[Export Complete]");
            Console.WriteLine("[Press any key to close window]");
            Console.WriteLine("");
            Console.Read();
        }

        bool DrawPressAnyKey()
        {
            Console.WriteLine("Press any key to continue... [exit : Esc]");
            ConsoleKeyInfo keyValue = Console.ReadKey();

            if (keyValue.Key == ConsoleKey.Escape)
                return false;

            return true;
        }

        static void Main(string[] args)
        {
            // args[0] : input path
            // args[1] : version
            // args[2] : output path
            // args[3] : file list

            //string[] args1 = { "1.0.0", "D:\\ExcelToTableData", "D:\\ExcelToTableData", "Tevol_TirBio_Static.xlsx" };

            Program program = new Program();

            program.Generate(args);
        }
    }
}
