using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using jp.co.fit.vfreport;

namespace com.icss.ge.Auction.Common.Svf
{
    public class SvfPrint : MarshalByRefObject
    {
        //リモートオブジェクトは有効するかとチェックする。
        public Boolean IsActive()
        {
            return true;
        }

        //**************************************************
        // 関数名   ：executePrint
        // 機能説明 ：帳票の出力を実行する。
        // 引数     ：List<string[]> 帳票出力する文
        // 修正履歴 ：2010/04/13
        //**************************************************
        public Boolean executePrint(List<string[]> arrayList)
        {
            SvfLogger log = new SvfLogger();
            log.LogSvfInfo("executePrint()  start");
            string env_key = "printer.server.host";
            string env_value = System.Configuration.ConfigurationSettings.AppSettings[env_key];

            Boolean rtnFlg = true;
            SvfrClient svfClient = null;
            try
            {
                //ArrayListの要素は0の場合。
                if (arrayList == null || arrayList.Count == 0)
                {
                    rtnFlg = false;
                }
                else
                {
                    int Ret = 0;
                    int count = arrayList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        BaseMehodCmn method = new BaseMehodCmn(arrayList[i]);
                        string methodName = method.getMethodName();
                        List<string> param = null;
                        param = method.getParamArray();
                        //どんなメッソドが呼び出せる。
                        if (methodName.Equals("SvfClient"))
                        {
                            svfClient = new SvfrClient(env_value);
                            if (svfClient == null)
                            {
                                rtnFlg = false;
                                log.LogSvfInfo("svfClient = new SvfrClient(\"{0}\") を呼び出すことが失敗しました。", env_value);
                                break;
                            }
                            log.LogSvfInfo("svfClient = new SvfrClient(\"{0}\")", env_value);
                        }

                        if (methodName.Equals("VrInit"))
                        {
                            Ret = svfClient.VrInit();
                            if (Ret != 0)
                            {
                                rtnFlg = false;
                                log.LogSvfInfo("svfClient.VrInit() を呼び出すことが失敗しました。");
                                break;
                            }
                            log.LogSvfInfo("svfClient.VrInit()");
                        }
                        //VrSetPrinter(string printerName, string portName)
                        if (methodName.Equals("VrSetPrinter"))
                        {
                            if (param != null)
                            {
                                string param1 = param[0].ToString();
                                string param2 = param[1].ToString();
                                Ret = svfClient.VrSetPrinter(param1, param2);
                                if (Ret != 0)
                                {
                                    rtnFlg = false;
                                    log.LogSvfInfo("svfClient.VrSetPrinter(\"{0}\", \"{1}\") を呼び出すことが失敗しました。", param1, param2);
                                    break;
                                }
                                log.LogSvfInfo("svfClient.VrSetPrinter(\"{0}\", \"{1}\")", param1, param2);
                            }
                        }
                        //VrSetForm(string formName, int mode)
                        if (methodName.Equals("VrSetForm"))
                        {
                            if (param != null)
                            {
                                string param1 = param[0].ToString();
                                int param2 = int.Parse(param[1].ToString());
                                Ret = svfClient.VrSetForm(param1, param2);
                                if (Ret != 0)
                                {
                                    rtnFlg = false;
                                    log.LogSvfInfo("svfClient.VrSetForm(\"{0}\", {1}) を呼び出すことが失敗しました。", param1, param2);
                                    break;
                                }
                                log.LogSvfInfo("svfClient.VrSetForm(\"{0}\", {1})", param1, param2);

                            }
                        }
                        //VrSetSpoolFileName2(string fileName)
                        if (methodName.Equals("VrSetSpoolFileName2"))
                        {
                            if (param != null)
                            {
                                string param1 = param[0].ToString();
                                Ret = svfClient.VrSetSpoolFileName2(param1);
                                if (Ret != 0)
                                {
                                    rtnFlg = false;
                                    log.LogSvfInfo("svfClient.VrSetSpoolFileName2(\"{0}\") を呼び出すことが失敗しました。", param1);
                                    break;
                                }
                                log.LogSvfInfo("svfClient.VrSetSpoolFileName2(\"{0}\")", param1);
                            }

                        }
                        //VrsOut(string fieldName, string data)
                        if (methodName.Equals("VrsOut"))
                        {
                            if (param != null)
                            {
                                string param1 = param[0].ToString();
                                string param2 = param[1].ToString();
                                Ret = svfClient.VrsOut(param1, param2);
                                if (Ret != 0)
                                {
                                    rtnFlg = false;
                                    log.LogSvfInfo("svfClient.VrsOut(\"{0}\", \"{1}\") を呼び出すことが失敗しました。", param1, param2);
                                    break;
                                }
                                log.LogSvfInfo("svfClient.VrsOut(\"{0}\", \"{1}\")", param1, param2);
                            }
                        }
                        // VrsOutn(string fieldName, int index, string data)
                        if (methodName.Equals("VrsOutn"))
                        {
                            if (param != null)
                            {
                                string param1 = param[0].ToString();
                                int param2 = int.Parse(param[1].ToString());
                                string param3 = param[2].ToString();
                                Ret = svfClient.VrsOutn(param1, param2, param3);
                                if (Ret != 0)
                                {
                                    rtnFlg = false;
                                    log.LogSvfInfo("svfClient.VrsOutn(\"{0}\", {1}, \"{2}\") を呼び出すことが失敗しました。", param1, param2, param3);
                                    break;
                                }
                                log.LogSvfInfo("svfClient.VrsOutn(\"{0}\", {1}, \"{2}\")", param1, param2, param3);

                            }
                        }
                        //VrPage(string flags)
                        if (methodName.Equals("VrPage"))
                        {
                            if (param != null)
                            {
                                string param1 = param[0].ToString();
                                Ret = svfClient.VrPage(param1);
                                if (Ret != 0)
                                {
                                    rtnFlg = false;
                                    log.LogSvfInfo("svfClient.VrPage(\"{0}\") を呼び出すことが失敗しました。", param1);
                                    break;
                                }
                                log.LogSvfInfo("svfClient.VrPage(\"{0}\")", param1);
                            }
                        }
                        //VrAttribute(string fieldName, string data)
                        if (methodName.Equals("VrAttribute"))
                        {
                            if (param != null)
                            {
                                string param1 = param[0].ToString();
                                string param2 = param[1].ToString();
                                Ret = svfClient.VrAttribute(param1, param2);
                                if (Ret != 0)
                                {
                                    rtnFlg = false;
                                    log.LogSvfInfo("svfClient.VrAttribute(\"{0}\", \"{1}\") を呼び出すことが失敗しました。", param1, param2);
                                    break;
                                }
                                log.LogSvfInfo("svfClient.VrAttribute(\"{0}\", \"{1}\")", param1, param2);
                            }
                        }
                        //VrAttribute(string fieldName, string data)
                        if (methodName.Equals("VrAttributen"))
                        {
                            if (param != null)
                            {
                                string param1 = param[0].ToString();
                                int param2 = int.Parse(param[1].ToString());
                                string param3 = param[2].ToString();
                                Ret = svfClient.VrAttributen(param1, param2, param3);
                                if (Ret != 0)
                                {
                                    rtnFlg = false;
                                    log.LogSvfInfo("svfClient.VrAttributen(\"{0}\", {1}, \"{2}\") を呼び出すことが失敗しました。", param1, param2, param3);
                                    break;
                                }
                                log.LogSvfInfo("svfClient.VrAttributen(\"{0}\",{1}, \"{2}\")", param1, param2, param3);
                            }
                        }
                        //VrEndPage
                        if (methodName.Equals("VrEndPage"))
                        {
                            Ret = svfClient.VrEndPage();
                            if (Ret != 0)
                            {
                                rtnFlg = false;
                                log.LogSvfInfo("svfClient.VrEndPage() を呼び出すことが失敗しました。");
                                break;
                            }
                            log.LogSvfInfo("svfClient.VrEndPage()");
                        }
                        if (methodName.Equals("VrEndRecord"))
                        {
                            Ret = svfClient.VrEndRecord();
                            if (Ret != 0)
                            {
                                rtnFlg = false;
                                log.LogSvfInfo("svfClient.VrEndRecord() を呼び出すことが失敗しました。");
                                break;
                            }
                            log.LogSvfInfo("svfClient.VrEndRecord()");
                        }
                        //VrPrint()
                        if (methodName.Equals("VrPrint"))
                        {
                            Ret = svfClient.VrPrint();
                            if (Ret != 0)
                            {
                                rtnFlg = false;
                                log.LogSvfInfo("svfClient.VrPrint() を呼び出すことが失敗しました。");
                                break;
                            }
                            log.LogSvfInfo("svfClient.VrPrint()");
                        }
                        //VrQuit()
                        if (methodName.Equals("VrQuit"))
                        {
                            Ret = svfClient.VrQuit();
                            if (Ret < 0)
                            {
                                rtnFlg = false;
                                log.LogSvfInfo("svfClient.VrQuit() を呼び出すことが失敗しました。");
                                break;
                            }
                            log.LogSvfInfo("svfClient.VrQuit()");
                        }
                        //close
                        if (methodName.Equals("Close"))
                        {
                            svfClient.Close();
                            log.LogSvfInfo("svfClient.Close()");
                        }
                    }
                }
                log.LogSvfInfo("executePrint()  end");
            }
            catch (Exception ex)
            {
                rtnFlg = false;
                if (svfClient != null)
                {
                    svfClient.VrQuit();
                    log.LogSvfInfo("帳票が異常終了しました。svfClient.VrQuit()");
                    svfClient.Close();
                }
                Console.WriteLine("帳票印刷が異常終了しました。{0}", ex);
            }
            return rtnFlg;
        }
    }
}
