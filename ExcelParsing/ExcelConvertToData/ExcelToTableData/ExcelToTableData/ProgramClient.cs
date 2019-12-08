using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExcelToTableData
{
    partial class Program
    {
        string ConvertFirstCharLower(string dst)
        {
            char[] schemeName = dst.ToCharArray();
            string temp = new string(schemeName);
            temp = temp.ToLower();
            schemeName[0] = temp.ToCharArray()[0];

            return new string(schemeName);
        }

        void CodeGeneratorForClient(string path, string fullName, string[,] values, int nMaxRow, int nMaxCol, string ver)
        {
            StreamWriter file = new StreamWriter(path + "\\TableData\\" + fullName + ".txt");

            List<string> noList = new List<string>();
            for (int y = _startDataRow; y < nMaxRow; ++y)
            {
                if (string.IsNullOrEmpty(values[y, 0]))
                    break;

                // Numbering - "No" 중복 체크.
                if (noList.Contains(values[y, 0]) && fullName != "RewardSet")
                {
                    Console.WriteLine("\t\t\t Duplicate [No]: " + values[y, 0] + "");
                    if (!DrawPressAnyKey())
                    {
                        file.Flush();
                        file.Dispose();
                        Environment.Exit(1);
                        return;
                    }
                }

                noList.Add(values[y, 0]);

                StringBuilder str = new StringBuilder();
                for (int x = 0; x < nMaxCol; ++x)
                {
                    if (values[0, x].Contains("-1"))
                        continue;

                    string schemeType = values[_schemeTypeRow, x];

                    if (string.IsNullOrEmpty(schemeType))
                        continue;

                    if (schemeType != "none")
                    {
                        // 검증 코드
                        string schemeName = values[_schemeNameRow, x];

                        // 추출 코드
                        if (y == _schemeNameRow)
                        {
                            str.Append(values[y, x]);
                        }
                        else
                            str.Append(values[y, x]);

                        str.Append("\t");
                    }
                }
                file.WriteLine(str.ToString());
            }

            file.Flush();
            file.Dispose();

            string name = fullName;
            string tableName = "Table" + name;
            string dataName = tableName + "Data";
            string fullPath = path + ("\\ClientTableCode\\" + tableName + ".cs");
            string idName = "";

            using (file = new StreamWriter(fullPath))
            {
                file.WriteLine("// Table scheme class: " + dataName);
                file.WriteLine("// Author: SoulSeek");
                file.WriteLine("");

                // write class name
                file.WriteLine("using UnityEngine;");
                file.WriteLine("using System;");
                file.WriteLine("using Tevol;");
                file.WriteLine("using System.Collections.Generic;");
                file.WriteLine("");
                file.WriteLine("namespace Tribio");
                file.WriteLine("{");

                // Make Data Class
                file.WriteLine("\t" + "public class " + dataName);
                file.WriteLine("\t" + "{");

                string schemeType;
                string schemeName;
                List<string> classList = new List<string>();

                for(int col = 0; col < nMaxCol; ++col)
                {
                    if (values[0, col].Contains("-1"))
                        continue;

                    schemeType = values[_schemeTypeRow, col];
                    schemeName = values[_schemeNameRow, col];

                    if (schemeType == null)
                        continue;

                    if (schemeType == "none")
                        continue;

                    string[] splitScheme = schemeName.Split('_');

                    if (schemeType == "char")
                        schemeType = "string";
                    else if (schemeType == "short")
                        schemeType = "int";

                    if(splitScheme.Length > 1)
                    {
                        if(false == classList.Contains(splitScheme[0]))
                        {
                            classList.Contains(splitScheme[0]);
                            splitScheme[0] = splitScheme[0] + "List";
                            splitScheme[0] = ConvertFirstCharLower(splitScheme[0]);

                            file.WriteLine("\t\t" + "protected " + "List<" + schemeType + "> _" + splitScheme[0] + " = new List<" + schemeType + ">();");
                            file.WriteLine("\t\t" + "public " + "List<" + schemeType + ">" + " " + splitScheme[0] + "{ get { return _" + splitScheme[0] + "; } }");
                            file.WriteLine("");
                        }
                    }
                    else
                    {
                        schemeName = ConvertFirstCharLower(schemeName);

                        if (col == 0)
                            idName = schemeName;

                        file.WriteLine("\t\t" + "protected " + schemeType + " _" + schemeName + ";");
                        file.WriteLine("\t\t" + "public " + schemeType + " " + schemeName + "{ get { return _" + schemeName + "; } }");
                        file.WriteLine("");
                    }
                }

                file.WriteLine("\t\t" + "public " + dataName + "( string[] splitData )");
                file.WriteLine("\t\t" + "{");

                int order = 0;

                for(int col = 0; col < nMaxCol; ++col)
                {
                    if (values[0, col].Contains("-1"))
                        continue;

                    schemeType = values[_schemeTypeRow, col];
                    schemeName = values[_schemeNameRow, col];

                    if (schemeType == "none")
                        continue;

                    if (true == string.IsNullOrEmpty(schemeName))
                        break;

                    string[] splitScheme = schemeName.Split('_');
                    bool isContain;

                    isContain = classList.Contains(splitScheme[0]);

                    splitScheme[0] = "_" + ConvertFirstCharLower(splitScheme[0]);

                    string frontString;
                    string endType;
                    string strOrder = "splitData[" + order.ToString() + "]";

                    if (isContain == true)
                    {
                        splitScheme[0] = splitScheme[0] + "List";
                        frontString = "if ( " + strOrder + " != \"-1\" )" + splitScheme[0] + ".Add( ";
                        endType = " );";
                    }
                    else
                    {
                        frontString = "" + splitScheme[0] + " = ";
                        endType = ";";
                    }

                    if ( schemeType == "int" || schemeType == "short")
                        file.WriteLine("\t\t" + "\t" + frontString + "Convert.ToInt32( " + strOrder + " )" + endType);
                    else if (schemeType == "long" )
                        file.WriteLine("\t\t" + "\t" + frontString + "Convert.ToInt64( " + strOrder + " )" + endType);
                    else if (schemeType == "string" || schemeType == "char")
                        file.WriteLine("\t\t" + "\t" + frontString + strOrder + endType);
                    else if (schemeType == "bool" )
                        file.WriteLine("\t\t" + "\t" + frontString + "Convert.ToBoolean( " + strOrder + " )" + endType);
                    else if (schemeType == "float" )
                        file.WriteLine("\t\t" + "\t" + frontString + "Convert.ToSingle( " + strOrder + " )" + endType);
                    else
                        file.WriteLine("\t\t" + "\t" + frontString + "(" + schemeType + ")" + "Convert.ToInt32( " + strOrder + " )" + endType);

                    ++order;
                }

                file.WriteLine("\t\t" + "}");

                file.WriteLine("\t" + "}");
                file.WriteLine("");
                file.WriteLine("\t" + "partial class " + tableName + " : GSingleton<" + tableName + ">");
                file.WriteLine("\t" + "{");

                file.WriteLine("");
                file.WriteLine("\t\t" + "private Dictionary<int, " + dataName + "> _sheet = new Dictionary<int, " + dataName + ">();");
                file.WriteLine("");

                // write the desc table list
                file.WriteLine("\t\t" + "// setup instance the record table");

                // write the default function
                file.WriteLine("\t\t" + "static public int GetTableCount()");
                file.WriteLine("\t\t" + "{");
                file.WriteLine("\t\t" + "\t" + "return instance._sheet.Count;");
                file.WriteLine("\t\t" + "}");
                file.WriteLine("");
                file.WriteLine("\t\t" + "static public " + dataName + " GetData( int tableNo )");
                file.WriteLine("\t\t" + "{");
                file.WriteLine("\t\t" + "\t" + "return instance._sheet[tableNo];");
                file.WriteLine("\t\t" + "}");
                file.WriteLine("");
                file.WriteLine("\t\t" + "static public Dictionary<int, " + dataName + ">" + " GetSheet()");
                file.WriteLine("\t\t" + "{");
                file.WriteLine("\t\t" + "\t" + "return instance._sheet;");
                file.WriteLine("\t\t" + "}");

                classList.Clear();

                bool isLocalizeMethod = false;

                for(int col = 0; col < nMaxCol; ++col)
                {
                    if (values[0, col].Contains("-1"))
                        continue;

                    schemeType = values[_schemeTypeRow, col];
                    schemeName = values[_schemeNameRow, col];

                    if (schemeType == "none")
                        continue;

                    if (true == string.IsNullOrEmpty(schemeName))
                        break;

                    string[] splitScheme = schemeName.Split('_');

                    bool isLocalize = false;

                    if (splitScheme.Length > 1)
                    {
                        if (false == classList.Contains(splitScheme[0]))
                        {
                            classList.Add(splitScheme[0]);

                            string lowerScheme = ConvertFirstCharLower(splitScheme[0]);

                            file.WriteLine("\t\t" + "static public List<" + schemeType + "> Get" + splitScheme[0] + "List( int tableNo )");
                            file.WriteLine("\t\t" + "{");
                            file.WriteLine("\t\t" + "\t" + "if ( false == instance._sheet.ContainsKey(tableNo) )");
                            file.WriteLine("\t\t" + "\t\t" + "Tevol.Debug.LogError( \"The key is not valid : \"" + " + tableNo);");
                            file.WriteLine("");

                            if (isLocalize)
                            {
                                file.WriteLine("\t\t" + "\t" + "if ( instance.localize != StringTableLoader.instance.localizing )");
                                file.WriteLine("\t\t" + "\t\t" + "instance.ReloadStringSheet( StringTableLoader.instance.localizing );");
                                file.WriteLine("");
                            }

                            file.WriteLine("\t\t" + "\t" + "return instance._sheet[tableNo]." + lowerScheme + "List;");
                            file.WriteLine("\t\t" + "}");
                            file.WriteLine("");
                            file.WriteLine("\t\t" + "static public " + schemeType + " Get" + splitScheme[0] + "ByOrder( int tableNo, int order )");
                            file.WriteLine("\t\t" + "{");
                            file.WriteLine("\t\t" + "\t" + "if ( false == instance._sheet.ContainsKey(tableNo) )");
                            file.WriteLine("\t\t" + "\t\t" + "Tevol.Debug.LogError( \"The key is not valid : \"" + " + tableNo);");
                            file.WriteLine("");

                            if (isLocalize)
                            {
                                file.WriteLine("\t\t" + "\t" + "if ( instance.localize != StringTableLoader.instance.localizing )");
                                file.WriteLine("\t\t" + "\t\t" + "instance.ReloadStringSheet( StringTableLoader.instance.localizing );");
                                file.WriteLine("");
                            }

                            file.WriteLine("\t\t" + "\t" + "return instance._sheet[tableNo]." + lowerScheme + "List[order];");
                            file.WriteLine("\t\t" + "}");
                            file.WriteLine("");
                        }
                    }
                    else
                    {
                        if (schemeType == "short")
                            schemeType = "int";
                        else if (schemeType == "char")
                            schemeType = "string";

                        file.WriteLine("\t\t" + "static public " + schemeType + " Get" + schemeName + "( int tableNo )");
                        file.WriteLine("\t\t" + "{");
                        file.WriteLine("\t\t" + "\t" + "if ( false == instance._sheet.ContainsKey(tableNo) )");
                        file.WriteLine("\t\t" + "\t\t" + "Tevol.Debug.LogError( \"" + "The key is not valid : \"" + " + tableNo);");
                        file.WriteLine("");

                        if (isLocalize)
                        {
                            file.WriteLine("\t\t" + "\t" + "if ( instance.localize != StringTableLoader.instance.localizing )");
                            file.WriteLine("\t\t" + "\t\t" + "instance.ReloadStringSheet( StringTableLoader.instance.localizing );");
                            file.WriteLine("");
                        }

                        schemeName = ConvertFirstCharLower(schemeName);

                        file.WriteLine("\t\t" + "\t" + "return instance._sheet[tableNo]." + schemeName + ";");
                        file.WriteLine("\t\t" + "}");
                        file.WriteLine("");
                    }
                }

                file.WriteLine("\t\t" + "public " + tableName + "()");
                file.WriteLine("\t\t" + "{");

                file.WriteLine("\t\t" + "\t" + "TextAsset txtFile = (TextAsset)ResourceLoader.LoadResource( \"" + "TableData/" + fullName + "\", typeof(TextAsset));");
                file.WriteLine("\t\t" + "\t" + "string[] inputData = txtFile.text.Split( '\\n' );");
                file.WriteLine("");
                file.WriteLine("\t\t" + "\t" + "for ( int lineCount = 1 ; lineCount < inputData.Length ; ++lineCount )");
                file.WriteLine("\t\t" + "\t" + "{");
                file.WriteLine("\t\t" + "\t\t" + "string[] parsedLines = inputData[lineCount].Split( '\\r' );");
                file.WriteLine("\t\t" + "\t\t" + "string[] parsedWords = parsedLines[0].Split( '\\t' );");
                file.WriteLine("");
                file.WriteLine("\t\t" + "\t\t" + "if( parsedWords.Length <= 1 )");
                file.WriteLine("\t\t" + "\t\t" + "\t" + "continue;");
                file.WriteLine("");
                file.WriteLine("\t\t" + "\t\t" + dataName + " data = new " + dataName + "( parsedWords );");
                file.WriteLine("\t\t" + "\t\t" + "_sheet.Add( data." + idName + ", data );");
                file.WriteLine("\t\t" + "\t" + "}");

                file.WriteLine("\t\t" + "}");
                file.WriteLine("\t" + "}");
                file.WriteLine("}");

                file.Flush();
                file.Dispose();
            }
        }
    }
}
