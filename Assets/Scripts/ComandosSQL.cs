using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Data;
using System.IO;
using Mono.Data.Sqlite;

public class ComandosSQL : MonoBehaviour
{

    string rutaDB;
    string conexion;

    IDbConnection conexionDB;
    IDbCommand comandosDB;
    IDataReader leerDatos;
    string nombreDB = "chinook.db";

    // Use this for initialization
    void Start()
    {
        AbrirDB();
        //ComandoSelect("*","albums");
        //ComandoWHERE("*", "albums","AlbumId","=","33");
        //ComandoWHERE_AND("CustomerId", "FirstName", "LastName", "Country", "customers", "Country", "=" ,"Brazil" ,"CustomerId", ">", "10");
        //ComandoWHERE_AND_ORDER_BY("CustomerId", "FirstName", "LastName", "Country", "customers", "Country", "=", "Brazil", "CustomerId", ">", "10", "FirstName", "ASC");
        //ComandoIN("CustomerId", "FirstName", "LastName", "Country", "customers", "Country", "Brazil", "USA");
        //FuncCOUNT();
        //FuncAVG();
        //INSERT("Mariano");
        DELETE();
        //CerrarDB();
    }

    void AbrirDB()
    {
        rutaDB = Application.dataPath + "/StreamingAssets/" + nombreDB;
        conexion = "URI=file:" + rutaDB;
        conexionDB = new SqliteConnection(conexion);
        conexionDB.Open();
    }

    void CerrarDB()
    {
        leerDatos.Close();
        leerDatos = null;
        comandosDB.Dispose();
        comandosDB = null;
        conexionDB.Close();
        conexionDB = null;
    }

    void ComandoSelect(string item, string tabla)
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "select " + item + " from " + tabla;
        comandosDB.CommandText = sqlQuery;
        leerDatos = comandosDB.ExecuteReader();
        while (leerDatos.Read())
        {
            try
            {
                Debug.Log(leerDatos.GetInt32(0) + " - " + leerDatos.GetString(1) + " - " + leerDatos.GetInt32(2));
            }
            catch (FormatException fe)
            {
                Debug.Log(fe.Message);
                continue;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                continue;
            }
        }
    }

    void ComandoWHERE(string item, string tabla, string campo, string comparador, string dato)
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "select " + item + " from " + tabla + " where " + campo + " " + comparador + " " + dato;
        comandosDB.CommandText = sqlQuery;
        leerDatos = comandosDB.ExecuteReader();
        while (leerDatos.Read())
        {
            try
            {
                Debug.Log(leerDatos.GetInt32(0) + " - " + leerDatos.GetString(1) + " - " + leerDatos.GetInt32(2));
            }
            catch (FormatException fe)
            {
                Debug.Log(fe.Message);
                continue;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                continue;
            }
        }
    }

    //select CustomerId, FirstName, LastName, Country 
    //from customers 
    //where Country = "Brazil" 
    //and CustomerId > 10 
    void ComandoWHERE_AND(string item1, string item2, string item3, string item4,
                            string tabla,
                            string campo1, string comparador1, string dato1,
                            string campo2, string comparador2, string dato2)
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "select " + item1 + "," + item2 + "," + item3 + "," + item4 +
                          " from " + tabla +
                          " where " + campo1 + " " + comparador1 + " " + "'" + dato1 + "'" +
                          " and " + campo2 + " " + comparador2 + dato2;
        comandosDB.CommandText = sqlQuery;
        leerDatos = comandosDB.ExecuteReader();
        while (leerDatos.Read())
        {
            try
            {
                Debug.Log(leerDatos.GetInt32(0) + " - " +
                            leerDatos.GetString(1) + " - " +
                            leerDatos.GetString(2) + " - " +
                            leerDatos.GetString(3));
            }
            catch (FormatException fe)
            {
                Debug.Log(fe.Message);
                continue;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                continue;
            }
        }
    }

    //select CustomerId, FirstName, LastName, Country 
    //from customers 
    //where Country = "Brazil" 
    //and CustomerId > 10 
    // order by FirstName ASC
    void ComandoWHERE_AND_ORDER_BY(string item1, string item2, string item3, string item4,
                            string tabla,
                            string campo1, string comparador1, string dato1,
                            string campo2, string comparador2, string dato2,
                            string campo3, string orden)
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "select " + item1 + "," + item2 + "," + item3 + "," + item4 +
                          " from " + tabla +
                          " where " + campo1 + " " + comparador1 + " " + "'" + dato1 + "'" +
                          " and " + campo2 + " " + comparador2 + dato2 +
                          " order by " + campo3 + " " + orden;
        comandosDB.CommandText = sqlQuery;
        leerDatos = comandosDB.ExecuteReader();
        while (leerDatos.Read())
        {
            try
            {
                Debug.Log(leerDatos.GetInt32(0) + " - " +
                            leerDatos.GetString(1) + " - " +
                            leerDatos.GetString(2) + " - " +
                            leerDatos.GetString(3));
            }
            catch (FormatException fe)
            {
                Debug.Log(fe.Message);
                continue;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                continue;
            }
        }
    }

    //select CustomerId, FirstName, LastName, Country 
    //from customers 
    //where Country IN ("Brazil","USA"); 
    // Idem para LIKE, BETWEEN, AS, LIMIT, LEFT JOIN, IS NULL
    void ComandoIN(string item1, string item2, string item3, string item4,
                            string tabla,
                            string campo1, string dato1, string dato2)
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "select " + item1 + "," + item2 + "," + item3 + "," + item4 +
                          " from " + tabla +
                          " where " + campo1 + " " + "IN" + " " + "('" + dato1 + "'" + ",'" + dato2 + "')";

        comandosDB.CommandText = sqlQuery;
        leerDatos = comandosDB.ExecuteReader();
        while (leerDatos.Read())
        {
            try
            {
                Debug.Log(leerDatos.GetInt32(0) + " - " +
                            leerDatos.GetString(1) + " - " +
                            leerDatos.GetString(2) + " - " +
                            leerDatos.GetString(3));
            }
            catch (FormatException fe)
            {
                Debug.Log(fe.Message);
                continue;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                continue;
            }
        }
    }

    //FUNCIONES; COUNT
    // SELECT count(*) FROM customers where Country = "Canada";
    void FuncCOUNT()
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "SELECT count(*) FROM customers where Country = \"Canada\"";
        comandosDB.CommandText = sqlQuery;
        int FilaCont = 0;
        FilaCont = Convert.ToInt32(comandosDB.ExecuteScalar());
        Debug.Log(FilaCont);
        comandosDB.Dispose();
        comandosDB = null;
        conexionDB.Close();
        conexionDB = null;
    }


    // select sum (total) from invoices; 
    // select avg (total) from invoices;
    // select min (total) from invoices;
    // select max (total) from invoices;
    void FuncAVG()
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "select avg (total) from invoices";
        comandosDB.CommandText = sqlQuery;
        Double FilaCont = 0;
        FilaCont = Convert.ToDouble(comandosDB.ExecuteScalar());
        Debug.Log(FilaCont);
        comandosDB.Dispose();
        comandosDB = null;
        conexionDB.Close();
        conexionDB = null;
    }

    //insert into media_types(Name) values("image")
    void INSERT(string dato)
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = String.Format("insert into media_types(Name) values(\"{0}\")",dato);
        comandosDB.CommandText = sqlQuery;   
        comandosDB.ExecuteScalar();

        comandosDB.Dispose();
        comandosDB = null;
        conexionDB.Close();
        conexionDB = null;
    }

    // update media_types SET Name = "Luis" where MediaTypeId = 11;

    void DELETE()
    {
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "delete from media_types where MediaTypeId = 11";
        comandosDB.CommandText = sqlQuery;
        comandosDB.ExecuteScalar();

        comandosDB.Dispose();
        comandosDB = null;
        conexionDB.Close();
        conexionDB = null;
    }
    // delete from media_types where MediaTypeId = 11;     

    
}
