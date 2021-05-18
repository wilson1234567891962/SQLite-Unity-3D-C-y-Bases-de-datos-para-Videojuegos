using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Data;
using System.IO;
using Mono.Data.Sqlite;

public class MarianoDB : MonoBehaviour {
    
    string rutaDB;
    string conexion;
    public Text miTexto;

    IDbConnection conexionDB;
    IDbCommand comandosDB;
    IDataReader leerDatos;
    string nombreDB = "RopaMarianoDB.sqlite";
    
	// Use this for initialization
	void Start () 
    {
        AbrirDB();
    }

    void AbrirDB()
    {
        rutaDB = Application.dataPath + "/StreamingAssets/" + nombreDB;
        conexion = "URI=file:" + rutaDB;

        conexionDB = new SqliteConnection(conexion);
        conexionDB.Open();

        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "select * from camisas";
        comandosDB.CommandText = sqlQuery;

        leerDatos = comandosDB.ExecuteReader();
        while (leerDatos.Read())
        {
            string testtoconsole = leerDatos.GetString(0);
            int testint = leerDatos.GetInt32(1);
            Debug.Log(testtoconsole + " - " + testint);
            miTexto.text = testtoconsole + " - " + testint.ToString();
        }

        leerDatos.Close();
        leerDatos = null;
        comandosDB.Dispose();
        comandosDB = null;
        conexionDB.Close();
        conexionDB = null;
    }
}
