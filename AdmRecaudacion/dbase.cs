using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmRecaudacion
{
    public static class dbase
    {

        public static OleDbConnection ConectarT()
        {
            var con = new OleDbConnectionStringBuilder();
            con.DataSource = "C:\\VisualEstudio\\AdmRecaudacion\\AdmRecaudacion\\doc\\";
            con.Provider = "Microsoft.Jet.OLEDB.4.0";
            //con.Provider = "VFPOLEDB";
            con["Extended Properties"] = "dBASE IV";
            return new OleDbConnection(con.ConnectionString);
        }

        public static OleDbConnection Conectar()
        {
            var con = new OleDbConnectionStringBuilder();
            //con["User ID"] = "";
            //con["DSN"] = "";
            //con["Cache Authentication"] = false;
            //con.DataSource = "C:\\VisualEstudio\\AdmRecaudacion\\AdmRecaudacion\\doc\\";
            //con.Provider = "vfpoledb";
            //con["Collating Sequence"] = "general";
            //con["Mask Password"] = false;
            //con["persist security info"] = false;
            //con["Mode"] = "Share Deny None";
            //con["Extended Properties"] = "";
            //con["Encrypt Password"] = false;
            return new OleDbConnection(con.ConnectionString);
        }

        public static OleDbConnection Conectar(string strDireccion)
        {
            var con = new OleDbConnectionStringBuilder();

            //con["User ID"] = "";
            //con["DSN"] = "";
            //con["Cache Authentication"] = false;
            //con.DataSource = strDireccion;
            //con.Provider = "vfpoledb";
            //con["Collating Sequence"] = "MACHINE";
            //con["Mask Password"] = false;
            //con["persist security info"] = false;
            //con["Mode"] = "Share Deny None";
            //con["Extended Properties"] = "";
            //con["Encrypt Password"] = false;

            con.DataSource = strDireccion;
            //con.Provider = "vfpoledb";
            con.Provider = "Microsoft.Jet.OLEDB.4.0";
            //con["Collating Sequence"] = "general";
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

    }
}
