using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppWebERS.Models{
    public class ConectorBD{
        private static readonly Lazy<ConectorBD> instance = new Lazy<ConectorBD>(() => new ConectorBD());

        private static MySqlConnection Con;

        public ConectorBD()
        {
            string conS = "Server=localhost;Port=3306;Database=appers;Uid=conexion;password=1234;SslMode=None";
            Con = new MySqlConnection(conS);
        }

        public static ConectorBD Instance
        {
            get
            {
                return instance.Value;
            }
        }
        /**
         * <autor>Diego Iturriaga-Ariel Cornejo</autor>
         * <summary>
         * Metodo encargado de realizar las conusltas en la base de datos
         * </summary> 
         * <param name="consulta"> String con la consulta a realizae</param>
         * <returns> 
         * Retorna un objeto MySqlDataReader que contendra los datos necesarios de la tabla si es que la consulta fue exitosa y
         * en caso contrario retornara un objeto null.
         * </returns>
         */
        public MySqlDataReader RealizarConsulta(string consulta)
        {
            MySqlCommand command = Con.CreateCommand();
            command.CommandText = consulta;
            try
            {
                Con.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) return reader;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Con.Close();
            }
            return null;
        }

        public void RealizarConsultaNoQuery(string consulta)
        {
            MySqlCommand command = Con.CreateCommand();
            command.CommandText = consulta;
            try
            {
                Con.Open();
                command.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Con.Close();
            }
        }

        //Cierra la conexión con la base de datos.
        public void CerrarConexion()
        {
            Con.Close();
        }

        /**
         * <autor>Diego Iturriaga</autor>
         * <summary>
         * Transforma un objeto tipo MySqlDataReader en un objeto de tipo DataSet.
         * </summary> 
         * <param name="reader"> objeto MySqlDataReader a transformar</param>
         * <returns> 
         * Retorna un objeto DataSet que contiene una tabla proveniente del objeto MySqlDataReader.
         * </returns>
         */
        public DataSet GetDataSet(MySqlDataReader reader)
        {
            DataSet dataSet = new DataSet();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataSet.Tables.Add(dataTable);

            return dataSet;
        }
    }
}