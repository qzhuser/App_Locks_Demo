using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jiqima : MonoBehaviour
{
    private Text jiqima;
    private InputField user,password;
    private Button ok;
    private string zhucemaStr;
    SqlAccess sa;
    // Start is called before the first frame update
    void Start()
    {
        sa = new SqlAccess();

        jiqima = transform.Find("Panel/Text").GetComponent<Text>();
        user = transform.Find("Panel/user").GetComponent<InputField>();
        password = transform.Find("Panel/password").GetComponent<InputField>();
        ok = transform.Find("Panel/ok").GetComponent<Button>();


        ok.onClick.AddListener(delegate () { Login(); });
        //ok.onClick.AddListener(delegate() { BtnOk(); });
        //getjiqima();
        //print(DesTool.EncryptString(jiqima.text,"qzh12345"));
    }
    #region 生成机器码
    /// <summary>
    /// 生成机器码
    /// </summary>
    void getjiqima() {
        string shebeima = SystemInfo.deviceUniqueIdentifier + SystemInfo.graphicsDeviceID;
        //label2.Text = getCpu() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号  
        string[] strid = new string[24];//  
        for (int i = 0; i < 24; i++)//把字符赋给数组  
        {
            strid[i] = shebeima.Substring(i, 1);
        }
        jiqima.text = "";
        for (int i = 0; i < 24; i++)//从数组随机抽取24个字符组成新的字符生成机器三  
        {
            jiqima.text += strid[i];
        }
    }
    #endregion

    void Login() {
        string UserStr = user.text;
        string PwStr = password.text;

        DataSet ds = sa.SelectWhere("user",new string[] { "User","password"},new string[] { UserStr,PwStr});
        print(ds.Tables[0].Rows);
        if (ds.Tables[0].Rows.Count > 0) {
            DateTime timeStart = (DateTime)ds.Tables[0].Select()[0]["TimeStart"];
            int StartYear = int.Parse(timeStart.ToString("yyyy"));
            int StartMonth = int.Parse(timeStart.ToString("MM"));
            int StartDay = int.Parse(timeStart.ToString("dd"));


            DateTime timeEnd = (DateTime)ds.Tables[0].Select()[0]["TimeEnd"];
            int EndYear = int.Parse(timeEnd.ToString("yyyy"));
            int EndMonth = int.Parse(timeEnd.ToString("MM"));
            int EndDay = int.Parse(timeEnd.ToString("dd"));

            if (EndYear - StartYear > 0)
            {
                print("登陆成功，您的到期时间为:" + timeEnd);
            }
            else if (EndYear - StartYear == 0)
            {
                if (EndMonth - StartMonth > 0)
                {
                    jiqima.text = "登陆成功!";
                    print("登陆成功!");
                    LoadLevel();
                }
                else if (EndMonth - StartMonth == 0)
                {
                    if (EndDay - StartDay >= 0)
                    {
                        jiqima.text = "登陆成功!";
                        print("登陆成功!");
                        LoadLevel();
                    }
                    else
                    {
                        print("软件已到期，您的到期时间为:" + timeEnd);
                    }
                }
                else
                {
                    print("软件已到期，您的到期时间为:" + timeEnd);
                }
            }
            else {
                print("软件已到期，请联系管理员更改时间");
            }
        }
        else {
            print("用户名或密码错误");
        }
    }
    void LoadLevel() {
        SceneManager.LoadScene("SampleScene");
    }

    //void BtnOk() {
    //    if (shuruzhucema.text!= "")
    //    {
    //        if (zhucemaStr.TrimEnd().Equals(shuruzhucema.text.TrimEnd()))
    //        {

    //            Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.
    //            OpenSubKey("software", true).CreateSubKey("ZHY").CreateSubKey("ZHY.INI").
    //            CreateSubKey(zhucemaStr.TrimEnd());
    //            retkey.SetValue("UserName", "MySoft");
    //            print("注册成功");
    //        }
    //        else
    //        {
    //            print("注册码输入错误");

    //        }

    //    }
    //    else { print("请生成注册码"); }
    //}
    void Update()
    {
        
    }
}
