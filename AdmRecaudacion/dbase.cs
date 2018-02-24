using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AdmRecaudacion
{
    public static class dbase
    {

        public static OleDbConnection Conectar()
        {
            var con = new OleDbConnectionStringBuilder();
            con.DataSource = "C:\\VisualEstudio\\AdmRecaudacion\\AdmRecaudacion\\doc\\";
            con.Provider = "Microsoft.Jet.OLEDB.4.0";
            //con.Provider = "VFPOLEDB";
            con["Extended Properties"] = "dBASE IV";
            return new OleDbConnection(con.ConnectionString);
        }

        public static OleDbConnection Conectar(string strDireccion)
        {
            var con = new OleDbConnectionStringBuilder();
            con.DataSource = strDireccion;
            con.Provider = "Microsoft.Jet.OLEDB.4.0";
            con["Extended Properties"] = "dBASE IV";
            return new OleDbConnection(con.ConnectionString);
        }

        public static OleDbDataAdapter adapter(OleDbConnection oleDbConnection, string sql)
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.CommandText = sql;
            oleDbCommand.Connection = oleDbConnection;
            return new OleDbDataAdapter(oleDbCommand);
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

            var con = Conectar(dirDB);

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = createSql;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            foreach (DataRow row in data.Rows)
            {
                string insertSql = "insert into " + nomBD + " values (";
                for (int i = 0; i < list.Count; i++)
                {
                    insertSql = insertSql + "'" + row[list[i].ToString()].ToString().Replace("'", "''") + "',";
                }
                insertSql = insertSql.Substring(0, insertSql.Length - 1) + ")";
                cmd.CommandText = insertSql;
                cmd.ExecuteNonQuery();
            }
            con.Close();

        }

        

    }
}
