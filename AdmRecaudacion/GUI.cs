using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdmRecaudacion
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private void cbxListBancos_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAceptar.Select();
            btnAceptar.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (rBtn2.Checked)
            {
                string path = @txtboxOpen.Text;
                string ext = Path.GetExtension(path).ToLower();
                if (ext.Equals(".dbf"))
                {
                    importDBF(path);
                }
                else
                {
                    interpreteCREP(path);
                }
                
            }
            else
            {
                if (rBtn1.Checked)
                {
                    descargarArchivo();
                    interpreteCREP(@"CREP1109 27.10.txt");
                }
            }
        }

        private void descargarArchivo()
        {
            string remoteUri = "http://www.sbs.gob.pe/app/xmltipocambio/";
            string fileName = "TC_TI_Portal_xml.xml", myStringWebResource = null;
            WebClient myWebClient = new WebClient();
            myStringWebResource = remoteUri + fileName;
            fileName = "TC.xml";
            myWebClient.DownloadFile(myStringWebResource, fileName);
        }

        //public static void Main()
        //{
        //    string path = @"c:\temp\MyTest.txt";
        //    if (!File.Exists(path))
        //    {
        //        // Create a file to write to.
        //        using (StreamWriter sw = File.CreateText(path))
        //        {
        //            sw.WriteLine("Hello");
        //            sw.WriteLine("And");
        //            sw.WriteLine("Welcome");
        //        }
        //    }
        //}

        private int interpreteCREP(string path)
        {

            //string path = @"CREP1109 27.10.txt";
            int counter = 0, numVal = -1;
            string line, fecha, key, suc, ope;          
           
            if (!File.Exists(path))
            {
                return counter;
            }

            DataTable data = null;
            DataColumn column;
            DataRow row;

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Substring(0, 2).Equals("CC") && counter==0)
                    {
                        
                        try
                        {
                            numVal = Convert.ToInt32(line.Substring(22,9));
                        }
                        catch (FormatException e)
                        {
                            e.ToString();
                            //Console.WriteLine("Input string is not a sequence of digits.");
                        }
                        catch (OverflowException e)
                        {
                            e.ToString();
                            //Console.WriteLine("The number cannot fit in an Int32.");
                        }
                        finally
                        {
                            //if (numVal < Int32.MaxValue)
                            //{
                            //    Console.WriteLine("The new value is {0}", numVal + 1);
                            //}
                            //else
                            //{
                            //    Console.WriteLine("numVal cannot be incremented beyond its current value");
                            //}
                        }

                        if (numVal > 0)
                        {
                            #region Cabecera Tabla BCP
                            
                            
                            data = new DataTable("Recaudacion");

                            column = new DataColumn();
                            column.DataType = Type.GetType("System.String");
                            column.ColumnName = "ruc";
                            data.Columns.Add(column);

                            column = new DataColumn();
                            column.DataType = System.Type.GetType("System.DateTime");
                            column.ColumnName = "fecha";
                            data.Columns.Add(column);

                            column = new DataColumn();
                            column.DataType = Type.GetType("System.Char");
                            column.ColumnName = "indicador";
                            data.Columns.Add(column);

                            column = new DataColumn();
                            column.DataType = Type.GetType("System.String");
                            column.ColumnName = "moneda";
                            data.Columns.Add(column);

                            column = new DataColumn();
                            column.DataType = Type.GetType("System.Double");
                            column.ColumnName = "importe";
                            data.Columns.Add(column);

                            column = new DataColumn();
                            column.DataType = Type.GetType("System.String");
                            column.ColumnName = "sucursal";
                            data.Columns.Add(column);

                            column = new DataColumn();
                            column.DataType = Type.GetType("System.String");
                            column.ColumnName = "operacion";
                            data.Columns.Add(column);

                            column = new DataColumn();
                            column.DataType = Type.GetType("System.String");
                            column.ColumnName = "key-ope";
                            data.Columns.Add(column);

                            #endregion
                        }

                    }
                    if (line.Substring(0,2).Equals("DD") && data!=null && numVal > 0) 
                    {
                        try
                        {
                            row = data.NewRow();
                            row["ruc"] = line.Substring(16, 11);
                            fecha = line.Substring(57, 8);
                            fecha = fecha.Substring(6, 2) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(0, 4);
                            row["fecha"] = Convert.ToDateTime(fecha);
                            row["indicador"] = Convert.ToChar(line.Substring(186, 1));
                            row["moneda"] = line.Substring(5, 1);
                            row["importe"] = Convert.ToInt32(line.Substring(73, 15))/100;
                            suc = line.Substring(118, 6);
                            //numero de operacion
                            ope = line.Substring(124, 6);
                            //key
                            key = suc + ope;
                            row["sucursal"] = suc;
                            row["operacion"] = ope;
                            row["key-ope"] = key;
                            data.Rows.Add(row);
                        }
                        catch (FormatException e)
                        {
                            e.ToString();
                            //Console.WriteLine("Input string is not a sequence of digits.");
                        }
                        catch (OverflowException e)
                        {
                            e.ToString();
                            //Console.WriteLine("The number cannot fit in an Int32.");
                        }
                        catch (ArgumentNullException e)
                        {
                            e.ToString();
                        }
                        counter++;
                    }
                    
                }
            }
            if (data != null )
                dataGridView1.DataSource = data;
            return counter;

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            opeFileDialogo.InitialDirectory = "C:\\VisualEstudio\\AdmRecaudacion\\AdmRecaudacion\\doc";
            opeFileDialogo.Filter = "Archivos de texto (*.txt)|*.txt|Todo los archivos (*.*)|*.*";
            opeFileDialogo.Title = "Seleccionar un archivo de banco";
            //opeFileDialogo.FilterIndex = 2;
            //opeFileDialogo.RestoreDirectory = true;

            if (opeFileDialogo.ShowDialog() == DialogResult.OK)
            {
                //StreamReader sr = new StreamReader(opeFileDialogo.FileName);
                //MessageBox.Show(sr.ReadToEnd());
                //sr.Close();
                txtboxOpen.Text = opeFileDialogo.FileName;
                //MessageBox.Show(opeFileDialogo.FileName);
            }
        }

        private void rBtn1_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtn1.Checked)
            {
                txtboxOpen.Enabled = false;
                btnOpen.Enabled = false;
            }
        }

        private void rBtn2_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtn2.Checked)
            {
                txtboxOpen.Enabled = true;
                btnOpen.Enabled = true;
            }
        }

        private void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }


        private void importDBF(string path)
        {
            string nomBD = Path.GetFileName(path);
            string dirDB = Path.GetFullPath(path).Replace(nomBD, "");
            using (var oleDbConnection = dbase.Conectar(dirDB))
            {
                using (var da = dbase.adapter(oleDbConnection,"SELECT * FROM [" + nomBD + "]"))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            //using (OleDbConnection oleDbConnection = new OleDbConnection())
            //{
            //    oleDbConnection.ConnectionString = GetConnection(dirDB);
            //    OleDbCommand oleDbCommand = new OleDbCommand();
            //    oleDbCommand.CommandText = "SELECT * FROM [" + nomBD + "]";
            //    oleDbCommand.Connection = oleDbConnection;
            //    using (OleDbDataAdapter da = new OleDbDataAdapter(oleDbCommand))
            //    {
            //        DataTable dt = new DataTable();
            //        da.Fill(dt);
            //        dataGridView1.DataSource = dt;
            //    }
            //}
        }

        public static void DataTableIntoDBF(string path, DataTable data)
        {
            # region mydata
            string nomBD = Path.GetFileName(path);
            string dirDB = Path.GetFullPath(path).Replace(nomBD, "");
            nomBD = nomBD.Replace(".dbf", "");
            ArrayList list = new ArrayList();

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string createSql = "create table " + nomBD + " (";

            foreach (DataColumn dc in data.Columns)
            {
                string fieldName = dc.ColumnName;
                string type = dc.DataType.ToString();

                switch (type)
                {
                    case "System.Char":
                        type = "varchar(1)";
                        break;

                    case "System.String":
                        type = "varchar(100)";
                        break;

                    case "System.Boolean":
                        type = "varchar(10)";
                        break;

                    case "System.Int32":
                        type = "int";
                        break;

                    case "System.Double":
                        type = "Double";
                        break;

                    case "System.DateTime":
                        type = "TimeStamp";
                        break;
                }
                createSql = createSql + "[" + fieldName + "]" + " " + type + ",";
                list.Add(fieldName);
            }

            createSql = createSql.Substring(0, createSql.Length - 1) + ")";
            #endregion

            var con = dbase.ConectarT();

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = createSql;
            cmd.Connection = con;
            con.Open();
            //cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            //con.Close();

            //con = dbase.Conectar();

            //string insertSql = "insert into " + nomBD + " values ";
            foreach (DataRow row in data.Rows)
            {
                string insertSql = "insert into " + nomBD + " values (";
                for (int i = 0; i < list.Count; i++)
                {
                    insertSql = insertSql + "'" + row[list[i].ToString()].ToString().Replace("'", "''") + "',";
                }
                insertSql = insertSql.Substring(0, insertSql.Length - 1) + ")";
                cmd.CommandText = insertSql;
                //cmd.Connection = con;
                //con.Open();
                cmd.ExecuteNonQuery();
            }
            //insertSql = insertSql.Substring(0, insertSql.Length - 1)+";";
            //cmd.CommandText = insertSql;
            //cmd.Connection = con;
            //con.Open();
            //cmd.ExecuteNonQuery();
            con.Close();
            
        }

        //private static string GetConnection(string strDireccion)
        //{
        //    return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDireccion + ";Extended Properties=dBASE IV;";
        //}

        //public static string ReplaceEscape(string str)
        //{
        //    str = str.Replace("'", "''");
        //    return str;
        //}

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string path = "C:\\VisualEstudio\\AdmRecaudacion\\AdmRecaudacion\\doc\\backup.dbf";
            DataTable tdb = dataGridView1.DataSource as DataTable;
            DataTableIntoDBF(path,tdb);//("backup.DBF",tdb);
        }

        private string prepareToCompareString(string s)
        {
            s.ToLower();
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            s = replace_a_Accents.Replace(s, "a");
            s = replace_e_Accents.Replace(s, "e");
            s = replace_i_Accents.Replace(s, "i");
            s = replace_o_Accents.Replace(s, "o");
            s = replace_u_Accents.Replace(s, "u");
            s = s.ToUpper().Replace(" ", "");
            return s;
        }
    }
}
