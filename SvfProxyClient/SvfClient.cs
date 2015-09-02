using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace com.icss.ge.Auction.Common.Svf
{
    public class SvfClient
    {
        //ログ
        private Logger log = Logger.GetLoggerInstance();

        //帳票印刷のコード配列
        private List<String[]> CallInfoList;

        //帳票印刷の代理インスタンス
        private SvfPrint svfprint = null;

        //帳票印刷の代理インスタンスを設定する。
        public void SetSvfPrintProxy(SvfPrint printProxy)
        {
            svfprint = printProxy;
        }

        public SvfClient()
        {
        }

        #region -- Methods of ISvf --
        public void Close()
        {
            string[] MethodInfo = new string[1];
            MethodInfo[0] = "Close";
            CallInfoList.Add(MethodInfo);
        }        

        public int VrAttribute(string fieldName, string data)
        {
            string[] MethodInfo = new string[3];
            MethodInfo[0] = "VrAttribute";
            MethodInfo[1] = fieldName;
            MethodInfo[2] = data;
            CallInfoList.Add(MethodInfo);
            return 0;
        }

        public int VrAttributen(string fieldName, int index, string data)
        {
            string[] MethodInfo = new string[4];
            MethodInfo[0] = "VrAttributen";
            MethodInfo[1] = fieldName;
            MethodInfo[2] = index.ToString();
            MethodInfo[3] = data;
            CallInfoList.Add(MethodInfo);
            return 0;
        }

        public int VrEndPage()
        {
            string[] MethodInfo = new string[1];
            MethodInfo[0] = "VrEndPage";
            CallInfoList.Add(MethodInfo);
            return 0;
        }

        public int VrEndRecord()
        {
            string[] MethodInfo = new string[1];
            MethodInfo[0] = "VrEndRecord";
            CallInfoList.Add(MethodInfo);
            return 0;
        }

        public int VrInit()
        {
            //印刷する帳票リストオード
            if (CallInfoList != null && CallInfoList.Count > 0)
            {
                CallInfoList.Clear ();
            }
            else
            {
                CallInfoList = new List<String[]>();
            }
            string[] MethodInfo = new string[1];
            MethodInfo[0] = "SvfClient";
            CallInfoList.Add(MethodInfo);

             MethodInfo = new string[1];
            MethodInfo[0] = "VrInit";
            CallInfoList.Add(MethodInfo);
            return 0;
        }

        public int VrPage(string flags)
        {
            string[] MethodInfo = new string[2];
            MethodInfo[0] = "VrPage";
            MethodInfo[1] = flags;
            CallInfoList.Add(MethodInfo);
            return 0;
        }       

        public int VrPrint()
        {
            string[] MethodInfo = new string[1];
            MethodInfo[0] = "VrPrint";
            CallInfoList.Add(MethodInfo);
            return 0;
        }

        public int VrQuit()
        {
            string[] MethodInfo = new string[1];
            MethodInfo[0] = "VrQuit";
            CallInfoList.Add(MethodInfo);

            //帳票出力を実行する。
            try
            {
                if (!svfprint.executePrint(CallInfoList))
                {
                    log.LogInfo("帳票印刷中に異常が発生した。");
                    //異常をスルーしました。
                    for (int i = 0; i < CallInfoList.Count; i++)
                    {
                        string[] tmpstrArr = null;
                        tmpstrArr = CallInfoList[i];
                        for (int j = 0; j < tmpstrArr.Length; j++)
                        {
                            log.LogInfo(tmpstrArr[j].ToString());
                        }
                    }
                    MessageBox.Show("帳票印刷中に異常が発生した。", "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }                
            }
            catch (Exception ex)
            {
                log.LogInfo("帳票印刷中に異常が発生した。\r\n{0}", ex);
                //異常をスルーしました。
                MessageBox.Show("帳票印刷中に異常が発生した。", "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return 0;
        }

        public int VrSetForm(string formName, int mode)
        {
            string[] MethodInfo = new string[3];
            MethodInfo[0] = "VrSetForm";
            MethodInfo[1] = formName;
            MethodInfo[2] = mode.ToString();
            CallInfoList.Add(MethodInfo);
            return 0;
        }

        public int VrSetPrinter(string printerName, string portName)
        {
            string[] MethodInfo = new string[3];
            MethodInfo[0] = "VrSetPrinter";
            MethodInfo[1] = printerName;
            MethodInfo[2] = portName;
            CallInfoList.Add(MethodInfo);
            return 0;
        }      

        public int VrSetSpoolFileName2(string fileName)
        {
            string[] MethodInfo = new string[2];
            MethodInfo[0] = "VrSetSpoolFileName2";
            MethodInfo[1] = fileName;
            CallInfoList.Add(MethodInfo);
            return 0;
        }        

        public int VrsOut(string fieldName, string data)
        {
            string[] MethodInfo = new string[3];
            MethodInfo[0] = "VrsOut";
            MethodInfo[1] = fieldName;
            MethodInfo[2] = data;
            CallInfoList.Add(MethodInfo);
            return 0;
        }

        public int VrsOutn(string fieldName, int index, string data)
        {
            string[] MethodInfo = new string[4];
            MethodInfo[0] = "VrsOutn";
            MethodInfo[1] = fieldName;
            MethodInfo[2] = index.ToString();
            MethodInfo[3] = data;
            CallInfoList.Add(MethodInfo);
            return 0;
        }
        #endregion
    }
}
