using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Text.RegularExpressions;

namespace com.icss.ge.Auction.Common.Svf
{
    class Logger
    {
        //ログインスタンスを作成する。
        private static Logger Log;

        //ログファイル名
        private string LogFileName;

        //SVFログファイル名
        private string LogSvfFileName;

        //ログファイル名
        private string LogFilePath;

        //ログ出力フラグ
        private string PrintLogFlg;

        //最大のSVFログファイルの番号
        private int MaxSvfFileKey;

        //**************************************************
        // 関数名   ：Logger
        // 機能説明 ：Private型の構造関数。
        // 引数     ：無し
        // 修正履歴 ：2010/04/13
        //**************************************************
        private Logger()
        {
        }

        //**************************************************
        // 関数名   ：LogInfo
        // 機能説明 ：ログ情報をログファイルに書き込む。
        // 引数     ：logInfo ログ情報
        // 引数     ：args    ログ詳細情報
        // 修正履歴 ：2010/04/13
        //**************************************************
        public void LogInfo(string logInfo, params object[] args)
        {
            if (PrintLogFlg == null || !PrintLogFlg.Equals("1"))
            {
                return;
            }
            string cHeader = "";
            string cLogTime = "";
            cLogTime = System.DateTime.Now.ToString();

            if (args != null && args.Length > 0)
            {
                logInfo = String.Format(logInfo, args);
            }

            StackTrace st = new StackTrace(true);
            StackFrame sf = st.GetFrame(1);
            //cHeader = sf.GetMethod
            cHeader = sf.GetMethod().ReflectedType.Assembly.FullName;
            cHeader = sf.GetMethod().ReflectedType.FullName + "." + sf.GetMethod().Name + "()";
            cHeader = cLogTime + " [" + cHeader + "] ";
            //ログ情報
            logInfo = cHeader + logInfo + Environment.NewLine;
            try
            {
                //'ログ情報はログファイルに書き込む。
                File.AppendAllText(LogFileName, logInfo);
            }
            catch (Exception ex)
            {
                logInfo = String.Format("異常が発生しました。{0}", ex);
                //'ログ情報はログファイルに書き込む。
                File.AppendAllText(LogFileName, logInfo);
            }
        }

        //**************************************************
        // 関数名   ：SetSvfFileKey
        // 機能説明 ：ログインスタンスを作成し、返す。
        // 引数     ：無し
        // 修正履歴 ：2010/04/13
        //**************************************************
        public void CreateNewSvfLogFileName()
        {
            MaxSvfFileKey = MaxSvfFileKey + 1;
            LogSvfFileName = LogFilePath + "\\SvfReport_" + MaxSvfFileKey.ToString().PadLeft(4, '0') + ".txt";
            if (File.Exists(LogSvfFileName))
            {
                File.Delete(LogSvfFileName);
            }
        }

        //**************************************************
        // 関数名   ：LogInfo
        // 機能説明 ：ログ情報をログファイルに書き込む。
        // 引数     ：logInfo ログ情報
        // 引数     ：args    ログ詳細情報
        // 修正履歴 ：2010/04/13
        //**************************************************
        public void LogSvfInfo(string logInfo, params object[] args)
        {
            if (PrintLogFlg == null || !PrintLogFlg.Equals("1"))
            {
                return;
            }
            string cHeader = "";
            string cLogTime = "";
            cLogTime = System.DateTime.Now.ToString();

            if (args != null && args.Length > 0)
            {
                logInfo = String.Format(logInfo, args);
            }

            StackTrace st = new StackTrace(true);
            StackFrame sf = st.GetFrame(1);
            //cHeader = sf.GetMethod
            cHeader = sf.GetMethod().ReflectedType.Assembly.FullName;
            cHeader = sf.GetMethod().ReflectedType.FullName + "." + sf.GetMethod().Name + "()";
            cHeader = cLogTime + " [" + cHeader + "] ";
            //ログ情報
            logInfo = cHeader + logInfo + Environment.NewLine;
            try
            {
                //'ログ情報はログファイルに書き込む。
                File.AppendAllText(LogSvfFileName, logInfo);
            }
            catch (Exception ex)
            {
                logInfo = String.Format("異常が発生しました。{0}", ex);
                //'ログ情報はログファイルに書き込む。
                File.AppendAllText(LogSvfFileName, logInfo);
            }
        }

        //**************************************************
        // 関数名   ：GetLoggerInstance
        // 機能説明 ：ログインスタンスを作成し、返す。
        // 引数     ：無し
        // 修正履歴 ：2010/04/13
        //**************************************************
        public static Logger GetLoggerInstance()
        {
            if (Log == null)
            {
                Log = new Logger();
            }
            Log.Init();
            return Log;
        }

        //**************************************************
        // 関数名   ：Init
        // 機能説明 ：ログインスタンスを初期化する。
        // 引数     ：無し
        // 修正履歴 ：2010/04/13
        //**************************************************
        private void Init()
        {
            //クラスのメンバー変数を初期化する。
            this.MaxSvfFileKey = 0;
            this.LogSvfFileName = "";

            //カレントディレクトリを取得する。
            string env_key = "LogFlg";
            this.PrintLogFlg = ConfigurationSettings.AppSettings[env_key];

            env_key = "LogDir";
            this.LogFilePath = ConfigurationSettings.AppSettings[env_key];
            this.LogFileName = LogFilePath + "\\PrintReportLog.txt";

            int tmpInt = 0;
            try
            {
                //今日以外で作成したログファイルを削除する。
                string[] strFiles = Directory.GetFiles(LogFilePath);
                foreach (string fileName in strFiles)
                {
                    //ログファイルの最後修正の日付を取得する。
                    string accessDate = File.GetLastAccessTime(fileName).Date.ToString();
                    string nowDate = DateTime.Now.Date.ToString();

                    //ログファイルは今日で作成するのかとチェックする。
                    if (!nowDate.Equals(accessDate))
                    {
                        //過去のログファイルを削除する。
                        File.Delete(LogFileName);
                    }
                    else
                    {
                        //svfログファイル番号を取得する。
                        int tmpIndex = fileName.IndexOf("SvfReport_");
                        if (tmpIndex > -1)
                        {
                            string tmpstr = fileName.Substring(tmpIndex + 10, 4);
                            //数字だろうかをチェックする。
                            if (Regex.IsMatch(tmpstr, @"^\d*$"))
                            {
                                tmpInt = int.Parse(tmpstr);
                            }
                            
                            if (tmpInt > MaxSvfFileKey)
                            {
                                MaxSvfFileKey = tmpInt;
                            }
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                //'ログ情報はログファイルに書き込む。
                String logStr = String.Format("ログ出力は異常が発生しまいました。", ex);
                File.AppendAllText(LogFileName, logStr);
            }
        }
    }

}
