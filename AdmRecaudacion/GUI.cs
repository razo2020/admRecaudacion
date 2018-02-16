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
        private string Banco, Url;

        public GUI()
        {
            InitializeComponent();
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

        private void btnOpen_Click(object sender, EventArgs e)
        {

            //opeFileDialogo.InitialDirectory = "C:\\VisualEstudio\\AdmRecaudacion\\AdmRecaudacion\\doc";
            opeFileDialogo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            opeFileDialogo.Filter = "Archivos de texto (*.txt)|*.txt|Archivo de datos (*.dbf)|*.dbf|Todo los archivos (*.*)|*.*";
            opeFileDialogo.Title = "Seleccionar un archivo de banco";

            if (opeFileDialogo.ShowDialog() == DialogResult.OK)
            {
                txtboxOpen.Text = opeFileDialogo.FileName;
                txtboxOpen.ForeColor = Color.Black;
            }
        }

        private void cbxListBancos_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxListBancos.SelectedItem.ToString())
            {
                case "BANCO DE CREDITO DEL PERU":
                    Banco = "BCP";
                    Url = "http://";
                    break;
                case "BANCO INTERBANK":
                    Banco = "INB";
                    Url = "http://";
                    break;
                case "BBVA BANCO CONTINENTAL":
                    Banco = "BBVA";
                    Url = "http://";
                    break;
                case "SCOTIABANK PERU SAA":
                    Banco = "SCB";
                    Url = "http://";
                    break;
            }
            cbxListBancos.ForeColor = Color.Black;
            btnAceptar.Select();
            btnAceptar.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cbxListBancos.SelectedItem == null)
            {
                MessageBox.Show("Seleccionar una Financiera.");
                cbxListBancos.ForeColor = Color.Red;
                return;
            }

            if (rBtn2.Checked)
            {
                string path = @txtboxOpen.Text;
                
                if(File.Exists(path))
                {
                    string ext = Path.GetExtension(path).ToLower();
                    if (ext.Equals(".dbf"))
                    {
                        importDBF(path);
                    }
                    else
                    {
                        switch (Banco)
                        {
                            case "BCP":
                                interpreteCREP_BCP(path);
                                break;
                            case "INB":
                                MessageBox.Show("No existe operacion INB.");
                                //interpreteCREP_BCP(path);
                                break;
                            case "BBVA":
                                MessageBox.Show("No existe operacion BBVA.");
                                //interpreteCREP_BCP(path);
                                break;
                            case "SCB":
                                MessageBox.Show("No existe operacion SCB.");
                                //interpreteCREP_BCP(path);
                                break;
                        }
                        
                    }
                    btnGuardar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("La dirección no es la correcta: " + path);
                    txtboxOpen.ForeColor = Color.Red;
                    return;
                }

            }
            else
            {
                if (rBtn1.Checked)
                {
                    MessageBox.Show("No esta habilitado esta opcion.");
                    //descargarArchivo(Url, Banco);
                    //interpreteCREP_BCP(@Banco + ".txt");
                    //btnGuardar.Enabled = true;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            saveFileDialogG.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialogG.Filter = "Archivo de datos (*.dbf)|*.dbf";
            saveFileDialogG.Title = "Guardar un la BD del Banco";

            if (saveFileDialogG.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialogG.FileName;
                DataTable tdb = dataGridView1.DataSource as DataTable;
                dbase.DataTableIntoDBF(path, tdb);
            }
            //string path = "C:\\VisualEstudio\\AdmRecaudacion\\AdmRecaudacion\\doc\\backup.dbf";
            //DataTable tdb = dataGridView1.DataSource as DataTable;
            //dbase.DataTableIntoDBF(path, tdb);//("backup.DBF",tdb);
        }

/***************************************************************************/

        private void descargarArchivo(string remoteUri, string txtBanco)
        {
            txtBanco = txtBanco + ".txt";
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(remoteUri, txtBanco);
        }

        private int interpreteCREP_BCP(string path)
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
                    if (line.Substring(0, 2).Equals("CC") && counter == 0)
                    {

                        try
                        {
                            numVal = Convert.ToInt32(line.Substring(22, 9));
                        }
                        catch (FormatException e)
                        {
                            e.ToString();
                        }
                        catch (OverflowException e)
                        {
                            e.ToString();
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
                    if (line.Substring(0, 2).Equals("DD") && data != null && numVal > 0)
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
                            row["importe"] = Convert.ToInt32(line.Substring(73, 15)) / 100;
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
                        }
                        catch (OverflowException e)
                        {
                            e.ToString();
                        }
                        catch (ArgumentNullException e)
                        {
                            e.ToString();
                        }
                        counter++;
                    }

                }
            }
            if (data != null)
                dataGridView1.DataSource = data;
            return counter;

        }
        
        private void importDBF(string path)
        {
            string nomBD = Path.GetFileName(path);
            string dirDB = Path.GetFullPath(path).Replace(nomBD, "");
            using (var oleDbConnection = dbase.Conectar(dirDB))
            {
                using (var da = dbase.adapter(oleDbConnection, "SELECT * FROM [" + nomBD + "]"))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
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
