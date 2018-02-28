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
using System.Xml;

namespace AdmRecaudacion
{
    public partial class GUI : Form
    {
        private Config.Element banco, confi;

        public GUI()
        {
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            Config.Element[] items = Config.leerBancosXML();
            cbxListBancos.Items.AddRange(items);
        }

        // evento cuando se cambia de seleccion (online) 
        private void rBtn1_CheckedChanged(object sender, EventArgs e)
        {
            //se confirma si esta habilitado la opcion online
            if (rBtn1.Checked)
            {
                //se deshabilita la caja de texto donde se direcciona el archivo a abrir y el boton de busqueda.
                txtboxOpen.Enabled = false;
                btnOpen.Enabled = false;
            }
        }

        // evento cuando se cambia de seleccion (local) 
        private void rBtn2_CheckedChanged(object sender, EventArgs e)
        {
            //se confirma si esta habilitado la opcion local
            if (rBtn2.Checked)
            {
                //se habilita la caja de texto donde se direcciona el archivo a abrir y el boton de busqueda.
                txtboxOpen.Enabled = true;
                btnOpen.Enabled = true;
            }
        }

        //Evento que se activa cuando damos clic en buscar.
        private void btnOpen_Click(object sender, EventArgs e)
        {

            //se direcciona el inicio del directorio, en este caso USUARIO/DOCUMENTOS/
            //Se configura los archivos a filtrar
            //Se coloca un titulo a nuestra ventana de dialogo.
            opeFileDialogo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            opeFileDialogo.Filter = "Archivos de texto (*.txt)|*.txt|Archivo de datos (*.dbf)|*.dbf|Todo los archivos (*.*)|*.*";
            opeFileDialogo.Title = "Seleccionar un archivo de banco";

            //Se confirma de nuestra ventana de dialogo si se obtenemos un OK
            if (opeFileDialogo.ShowDialog() == DialogResult.OK)
            {
                //Agregamos en nuestra caja de texto de abrir archivo la direccion obtenida.
                //Cambiamos de color a negro las letras de nuestra caja de texto de abrir archivo. 
                txtboxOpen.Text = opeFileDialogo.FileName;
                txtboxOpen.ForeColor = Color.Black;
            }
        }

        //evento al seleccionar un banco.
        private void cbxListBancos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se comprueba la seleccion para cada caso.
            cbxListBancos.ForeColor = Color.Black;
            var items = (Config.Element)cbxListBancos.SelectedItem;
            cbxListConfig.Items.Clear();
            cbxListConfig.Items.AddRange(items.url);
            cbxListConfig.Select();
            cbxListConfig.Focus();
        }

        //evento seleccionar configuracion de archivo
        private void cbxListConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            banco = (Config.Element)cbxListBancos.SelectedItem;
            confi = (Config.Element)cbxListConfig.SelectedItem;
            btnAceptar.Select();
            btnAceptar.Focus();
        }

        //evento al dar clic en abrir el archivo sea online o local.
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
                        switch (banco.key)
                        {
                            case "BCP":
                                //dataGridView1.Rows.Clear();
                                interpreteTXT_Banco(path);
                                break;
                            case "INB":
                                MessageBox.Show("No existe operacion." + banco.titulo);
                                //interpreteCREP_BCP(path);
                                break;
                            case "BBVA":
                                MessageBox.Show("No existe operacion." + banco.titulo);
                                //interpreteCREP_BCP(path);
                                break;
                            case "SCB":
                                MessageBox.Show("No existe operacion." + banco.titulo);
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
        }

/***************************************************************************/

        private void descargarArchivo(string remoteUri, string txtBanco)
        {
            txtBanco = txtBanco + ".txt";
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(remoteUri, txtBanco);
        }

        private int interpreteTXT_Banco(string path)
        {
            //string path = @"CREP1109 27.10.txt";
            int counter = 0, numVal = -1;
            string line, fecha;

            if (!File.Exists(path))
            {
                return counter;
            }

            DataTable data = null;
            DataColumn column;
            DataRow row = null;
            List<Config.Element> elementos = null;

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (counter == 0)
                    {
                        #region Cabecera Tabla BCP

                        data = new DataTable("Recaudacion");

                        elementos = Config.leerColumnasXML(banco.key, confi.key);

                        foreach (Config.Element elemento in elementos)
                        {
                            column = new DataColumn();
                            switch (elemento.type)
                            {
                                case "string":
                                    column.DataType = Type.GetType("System.String");
                                    break;
                                case "int":
                                    column.DataType = typeof(int);
                                    break;
                                case "short":
                                    column.DataType = typeof(short);
                                    break;
                                case "char":
                                    column.DataType = typeof(char);
                                    break;
                                case "date":
                                    column.DataType = typeof(DateTime);
                                    break;
                                case "byte":
                                    column.DataType = typeof(Byte);
                                    break;
                                case "double":
                                    column.DataType = typeof(double);
                                    break;
                            }
                            column.ColumnName = elemento.key;
                            column.Caption = elemento.titulo;
                            data.Columns.Add(column);
                        }

                        #endregion
                    }

                    if (line.Substring(0, 2).Equals("DD") && data != null && elementos != null)
                    {
                        try
                        {
                            row = data.NewRow();

                            foreach (Config.Element elemento in elementos)
                            {
                                var str = line.Substring(elemento.valueX, elemento.valueY);
                                var tipo = elemento.type;
                                if (str.Trim().Equals("") && tipo!="string")
                                    str = "0";

                                switch (tipo)
                                {
                                    case "int":
                                        row[elemento.key] = Convert.ToInt32(str);
                                        break;
                                    case "short":
                                        row[elemento.key] = Convert.ToInt16(str);
                                        break;
                                    case "char":
                                        row[elemento.key] = Convert.ToChar(str);
                                        break;
                                    case "date":
                                        fecha = str.Substring(6, 2) + "/" + str.Substring(4, 2) + "/" + str.Substring(0, 4);
                                        row[elemento.key] = Convert.ToDateTime(fecha);
                                        break;
                                    case "byte":
                                        row[elemento.key] = Convert.ToByte(str);
                                        break;
                                    case "double":
                                        row[elemento.key] = Convert.ToDouble(str);
                                        break;
                                    default :
                                        row[elemento.key] = str;
                                        break;
                                }
                            }
                            data.Rows.Add(row);
                        }
                        catch (FormatException e)
                        {
                            MessageBox.Show(e.ToString());
                        }
                        catch (OverflowException e)
                        {
                            MessageBox.Show(e.ToString());
                        }
                        catch (ArgumentNullException e)
                        {
                            MessageBox.Show(e.ToString());
                        }
                        
                    }
                    counter++;
                }
            }

            if (data != null)
            {
                dataGridView1.DataSource = data;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    var nom = data.Columns[col.DataPropertyName].Caption;
                    if (!nom.ToString().Trim().Equals(""))
                        col.HeaderText = nom;
                }
            }

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
