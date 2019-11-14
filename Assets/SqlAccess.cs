using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;


public class SqlAccess
{
    //static void Main(string[] args) { }
    public static MySqlConnection dbConnection;
    //如果只是在本地的话，写localhost就可以。
    // static string host = "localhost";  
    //如果是局域网，那么写上本机的局域网IP
    static string host = "114.116.213.68";//ip地址，如果是本机则写localhost，如果不是本机，则写安装数据库电脑的ip
    static string id = "root";//数据库的用户名
    static string pwd = "yanghe@123";//数据库的密码
    static string database = "demo";//数据库的名称  这里我的数据库名称为：examsystem

    public SqlAccess()
    {
        OpenSql();
    }


    //}
    public static void OpenSql()
    {

        try
        {
            string connectionString = string.Format("Server = {0};port={4};Database = {1}; User ID = {2}; Password = {3};", host, database, id, pwd, "3306");
            dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();
        }
        catch (Exception e)
        {
            throw new Exception("服务器连接失败，请重新检查是否打开MySql服务。" + e.Message.ToString());

        }

    }
    //添加一个   表
    public DataSet CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {

            throw new Exception("columns.Length != colType.Length");

        }

        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];

        for (int i = 1; i < col.Length; ++i)
        {

            query += ", " + col[i] + " " + colType[i];

        }

        query += ")";

        return ExecuteQuery(query);
    }

    public DataSet CreateTableAutoID(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {

            throw new Exception("columns.Length != colType.Length");

        }

        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0] + " NOT NULL AUTO_INCREMENT";

        for (int i = 1; i < col.Length; ++i)
        {

            query += ", " + col[i] + " " + colType[i];

        }

        query += ", PRIMARY KEY (" + col[0] + ")" + ")";

        Debug.Log(query);

        return ExecuteQuery(query);
    }

    //插入一条数据，包括所有，不适用自动累加ID。
    public DataSet InsertInto(string tableName, string[] values)
    {

        string query = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";

        for (int i = 1; i < values.Length; ++i)
        {

            query += ", " + "'" + values[i] + "'";

        }

        query += ")";

        Debug.Log(query);
        return ExecuteQuery(query);

    }

    //插入部分ID
    public DataSet InsertInto(string tableName, string[] col, string[] values)
    {

        if (col.Length != values.Length)
        {

            throw new Exception("columns.Length != colType.Length");

        }

        string query = "INSERT INTO " + tableName + " (" + col[0];
        for (int i = 1; i < col.Length; ++i)
        {

            query += ", " + col[i];

        }

        query += ") VALUES (" + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {

            query += ", " + "'" + values[i] + "'";

        }

        query += ")";

        //        Debug.Log(query);
        return ExecuteQuery(query);

    }
    //选择数据库中的id
    public DataSet SelectWhere1(string tableName, string[] items)
    {
        string query = "SELECT " + items[0];

        for (int i = 1; i < items.Length; ++i)
        {

            query += ", " + items[i];

        }

        query += " FROM " + tableName;
        return ExecuteQuery(query);
    }
    /// <summary>
    ///  选出 items select items[] from tableName where  col operation(=) values 
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items"></param>
    /// <param name="col">列名</param>
    /// <param name="operation"></param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public DataSet SelectWhere(string tableName, string[] Column, string[] values, string[] items = null)
    {

        //if (col.Length != operation.Length || operation.Length != values.Length)
        //{

        //    throw new Exception("col.Length != operation.Length != values.Length");

        //}

        string query = "SELECT *"/* + items[0]*/;

        //for (int i = 1; i < items.Length; ++i)
        //{

        //    query += ", " + items[i];

        //}

        query += " FROM " + tableName + " WHERE " + Column[0]+ "= '" + values[0];

        for (int i = 1; i < Column.Length; ++i)
        {

            query += "' AND " + Column[i] + "= '" + values[i]+"'";

        }

        return ExecuteQuery(query);

    }
    //从数据库中获取所有的用户
    public DataSet GetAllUser(string tableName, string[] items)
    {
        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; ++i)
        {

            query += ", " + items[i];

        }
        query += " From " + tableName;

        return ExecuteQuery(query);
    }

    public DataSet UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {

        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += ", " + cols[i] + " =" + colsvalues[i];
        }

        query += " WHERE " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }
    //更新一条数据，例如  分数为90，现在想更新为99
    public DataSet updatewhere(string tableName, string score1, string userName)
    {
        string query = "update " + tableName + " set score = '" + score1 + "' where user" + " = '" + userName + "'";


        return ExecuteQuery(query);
    }

    //求数据库中有多少行数据
    public DataSet Count(string tableName)
    {
        string query = "select count(*) from " + tableName;

        return ExecuteQuery(query);
        // return query;
    }
    //检查密码是否正确
    public DataSet CheckPassword111(string tableName, string[] user)
    {
        string query = "select password from " + tableName + " where user = '" + user[0] + "'";

        return ExecuteQuery(query);
    }
    public DataSet Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        Debug.Log(query);
        return ExecuteQuery(query);
    }
    //删除用户
    public DataSet DeleteUser(string tableName, string col, string colvalue)
    {
        string query = "delete from " + tableName + " where " + col + " = '" + colvalue + "' ";

        return ExecuteQuery(query);
    }

    public void Close()
    {

        if (dbConnection != null)
        {
            dbConnection.Close();
            dbConnection.Dispose();
            dbConnection = null;
        }

    }

    public static DataSet ExecuteQuery(string sqlString)
    {
        if (dbConnection.State == ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);
                da.Fill(ds);
                Debug.Log(sqlString);
            }
            catch (Exception ee)
            {
                throw new Exception("SQL:" + sqlString + "/n" + ee.Message.ToString());

            }
            finally
            {
            }
            return ds;
        }
        return null;
    }

}