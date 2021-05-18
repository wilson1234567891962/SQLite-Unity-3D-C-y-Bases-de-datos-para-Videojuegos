using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;


public class ConexDB : MonoBehaviour {

    string rutaDB;
    string strConexion;
    string DBFileName = "RopaMarianoDB.sqlite";
    public Text myText;

    IDbConnection dbConnection;
    IDbCommand dbCommand;
    IDataReader reader;
	
    // Use this for initialization
	void Start () {
        AbrirDB();
	}

    void AbrirDB()
    {
        // Crear y abrir la conexion
        //Compruebo que plataforma es
        // Si es PC mantengo la ruta
		if (Application.platform == RuntimePlatform.WindowsEditor ||Application.platform == RuntimePlatform.OSXEditor )
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        }
		else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			rutaDB = Application.dataPath + "/Raw/" + DBFileName;
		}
		// Si es android
        else if (Application.platform == RuntimePlatform.Android)
        {
            rutaDB = Application.persistentDataPath + "/" + DBFileName;
            // Comprobar si el archivo se encuantra almacenado en persistant data
            if (!File.Exists(rutaDB))
            {
                WWW loadDB = new WWW("jar:file://"+ Application.dataPath + "!/assets/" + DBFileName);
                while (!loadDB.isDone)
                { }
                File.WriteAllBytes(rutaDB, loadDB.bytes);
            }
        }
        // ALmaceno el archivo en load db 
        // copio el archivo a persistant data


        strConexion = "URI=file:" + rutaDB;
        dbConnection = new SqliteConnection(strConexion);
        dbConnection.Open();
        // Crear la consulta
        dbCommand = dbConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM Camisas";
        dbCommand.CommandText = sqlQuery;

        // Leer la base de datos
        reader = dbCommand.ExecuteReader();
        while (reader.Read())
        {
            //id
            int id = reader.GetInt32(0);
            //marca
            string marca = reader.GetString(1);
            //color
            string color = reader.GetString(2);
            //cantidades
            int cantidad = reader.GetInt32(3);
            Debug.Log(id + " - " + marca + " - " + color + " - " + cantidad);
            myText.text = id.ToString() + " - " + marca + " - " + color + " - " + cantidad.ToString();
        }

        // Cerrar las conexiones
        reader.Close();
        reader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}
