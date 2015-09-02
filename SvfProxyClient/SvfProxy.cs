using System;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace com.icss.ge.Auction.Common.Svf
{
    public class SvfProxy
    {
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        //ログ出力処理
        private static Logger log = Logger.GetLoggerInstance();

        //帳票印刷の初期化処理、サーバと連絡する。
        static SvfProxy()
        {
            string configFile = "";
            configFile = getLogConfigFilePath();
            log.LogInfo("ConfigFileName:" + configFile);
            try
            {
                RemotingConfiguration.Configure(configFile, false);                               
                //ログ出力
                DumpTypeEntries(RemotingConfiguration.GetRegisteredWellKnownClientTypes());
            }
            catch (System.Security.SecurityException ex)
            {
                log.LogInfo("帳票印刷が異常終了しました。\r\n{0}", ex);
                throw ex;
            }
        }

        public static SvfClient CreateSvfClientInstance() 
        {
            SvfClient svfClient = null;
            try
            {
                SvfPrint svfprint = new SvfPrint();
                if (svfprint.IsActive())
                {
                    svfClient = new SvfClient();
                    svfClient.SetSvfPrintProxy(svfprint);
                }
                else
                {
                    log.LogInfo("帳票サーバと接続中に通信エラーが発生しました。");
                    MessageBox.Show("帳票サーバと接続中に通信エラーが発生しました。", "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //異常をスルーしました。
                    //throw new Exception("帳票サーバと接続中に通信エラーが発生しました。");
                }
            }
            catch (Exception ex)
            {
                log.LogInfo("帳票サーバと接続中に通信エラーが発生しました。\r\n{0}", ex);
                //異常をスルーしました。
                MessageBox.Show("帳票サーバと接続中に通信エラーが発生しました。", "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return svfClient;
        }

        private static void DumpTypeEntries(Array arr)
        {
            foreach (object obj in arr)
            {
                log.LogInfo(obj.GetType().Name + ":" + obj.ToString());
            }
        }

        //GeDirディレクトリを見つかる。
        private static string getLogConfigFilePath()
        {
            string gedir = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            if (gedir.EndsWith(@"\"))
            {
                gedir = gedir.Substring(0, gedir.Length - 1);
            }
            int index = 0;
            index = gedir.LastIndexOf(@"\");
            if (index > 0)
            {
                gedir = gedir.Substring(0, index + 1) + "Ini";
            }

            //APPサーバの配置ファイルを見つかる。
            Byte[] Buffer = new Byte[1024];
            string appSvrConfig = "";
            int bufLen = GetPrivateProfileString("PATH", "AppSvrConfig", "", Buffer, Buffer.GetUpperBound(0), gedir + @"\Gedir.ini");
            if (bufLen > 0)
            {
                appSvrConfig = System.Text.Encoding.GetEncoding(0).GetString(Buffer);
                appSvrConfig = appSvrConfig.Substring(0, bufLen);
            }

            return appSvrConfig;

        }

    }
}
