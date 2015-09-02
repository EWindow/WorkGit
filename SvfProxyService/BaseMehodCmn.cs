using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.icss.ge.Auction.Common.Svf
{
    class BaseMehodCmn
    {
        //メッソド名
        private string methodName;

        //パラメータ配列
        private List<String> paramArray;

        public BaseMehodCmn(string[] callStr)
        {
            if (callStr != null && callStr.Length > 0)
            {
                //メッソド名を取得する。
                methodName = callStr[0];

                //パラメータをつく関数の場合
                if (callStr.Length > 1)
                {
                    paramArray = new List<string>();
                    int i = 1;
                    while (i < callStr.Length)
                    {
                        paramArray.Add(callStr[i]);
                        i = i + 1;
                    }
                }
            }
        }

        public BaseMehodCmn(string callStr)
        {
            int startIndex = 0;
            int endIndex = 0;

            int kanmaIndex = 0;
            string tmpStr = "";
            string paramStr = "";
            startIndex = callStr.IndexOf("(");
            endIndex = callStr.IndexOf(")");

            methodName = callStr.Substring(0, startIndex);

            tmpStr = callStr.Substring(startIndex + 1, endIndex - startIndex);
            //kanmaIndex = tmpStr.IndexOf(",");
            if (!tmpStr.Trim().Equals(""))
            {
                paramArray = new List<String>();
                //tmpStr = callStr.Substring(startIndex+1);
                //paramArray.Add(tmpStr.Substring(0, kanmaIndex));
                //tmpStr = tmpStr.Substring(kanmaIndex + 1);
                while (true)
                {
                    kanmaIndex = tmpStr.IndexOf(",");
                    if (kanmaIndex == -1)
                    {
                        paramStr = tmpStr.Substring(0, tmpStr.Length - 1).Trim();
                        if (paramStr.StartsWith("\"") && paramStr.EndsWith("\""))
                        {
                            paramStr = paramStr.Substring(1, paramStr.Length - 2);
                        }
                        paramArray.Add(paramStr);
                        break;
                    }
                    else
                    {
                        paramStr = tmpStr.Substring(0, kanmaIndex).Trim();
                        if (paramStr.StartsWith("\"") && paramStr.EndsWith("\""))
                        {
                            paramStr = paramStr.Substring(1, paramStr.Length - 2);
                        }
                        paramArray.Add(paramStr);
                        tmpStr = tmpStr.Substring(kanmaIndex + 1);
                    }
                }
            }

        }

        public string getMethodName()
        {
            return methodName;
        }


        public List<String> getParamArray()
        {
            return paramArray;
        }

        //public void setMethodName(string argMethodName)
        //{
        //    methodName = argMethodName;
        //}


        //public void setParamArray(object[] argParamArray)
        //{
        //    if (argParamArray != null && argParamArray.Length != 0)
        //    {
        //        int paramLen = argParamArray.Length;
        //        paramArray = new List<String>();
        //        for (int i = 0; i < paramLen; i++)
        //        {
        //            paramArray[i] = argParamArray[i];
        //        }
        //    }
        //}



    }
}
