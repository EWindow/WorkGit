using System;
using System.ServiceProcess;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;
using System.Configuration;

namespace com.icss.ge.Auction.Common.Svf
{
    public partial class PrintProxyService : ServiceBase
    {
        //ログを出力するオブジェクトを作成する。
        private static Logger log = Logger.GetLoggerInstance();

        public PrintProxyService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string env_value = "";
            string env_key = "jp.co.fit.vfreport.home";
            env_value = System.Environment.GetEnvironmentVariable(env_key);
            if (env_value == null || env_value.Equals(""))
            {
                env_value = ConfigurationSettings.AppSettings[env_key];
                System.Environment.SetEnvironmentVariable(env_key, env_value);
            }
            log.LogInfo("オークション帳票印刷サービスが開始しました。");

            //配置ファイルを読み込む。。
            RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);
            //リモートオブジェクトを取得する。
            //Utility.DumpAllInfoAboutRegisteredRemotingTypes();
            DumpTypeEntries(RemotingConfiguration.GetRegisteredWellKnownServiceTypes());
        }

        protected override void OnStop()
        {
            //メモリを回収する。
            GC.Collect();
            GC.WaitForPendingFinalizers();
            log.LogInfo("オークション帳票印刷サービスが終了しました。");
        }

        private static void DumpTypeEntries(Array arr)
        {
            foreach (object obj in arr)
            {
                log.LogInfo(obj.GetType().Name + ":" + obj.ToString());
            }
        }
    }
}
