using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public class MysqlAdmin : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

        OpenSql();
	} 
	
	// Update is called once per frame
	void Update () 
	{
		
	}
    /// <summary>
    /// 建立数据库
    /// </summary>
    /// <returns></returns>
    public MySqlConnection GetSqlConn()
    {
        //数据库
        MySqlConnection sqlConn;
        string connStr = "Database=unity;Data Source=127.0.0.1;User Id=root;Password=123456;port=3306";
        sqlConn = new MySqlConnection(connStr);
        return sqlConn;
    }
    public void OpenSql()
    {
        MySqlConnection sqlcon = GetSqlConn();
    
        try
        {
            sqlcon.Open();
            Debug.Log(sqlcon.State);
            MySqlScript msq = new MySqlScript("select * from unity;");

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return;
        }

    }
}
