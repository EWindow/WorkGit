using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace com.icss.ge.Auction.Common.Svf
{
    class Logger
    {
        //ログインスタンスを作成する。
        private static Logger Log;
        //ログファイル名
        private string LogFileName;
        //ログ出力フラグ
        //private string OutPutFlg;

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
            //カレントディレクトリを取得する。
            string CurrentPath = System.Environment.CurrentDirectory.ToString();
            LogFileName = CurrentPath + "\\PrintReportLog.txt";
            if (File.Exists(LogFileName))
            {
                //ログファイルの最後修正の日付を取得する。
                string accessDate = File.GetLastAccessTime(LogFileName).Date.ToString();
                string nowDate = DateTime.Now.Date.ToString();

                //ログファイルは今日で作成するのかとチェックする。
                if (!nowDate.Equals(accessDate))
                {
                    //過去のログファイルを削除する。
                    File.Delete(LogFileName);
                }

            }
        }
    }

}
