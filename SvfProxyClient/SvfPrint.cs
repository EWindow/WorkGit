using System;
using System.Collections.Generic;
using System.Collections;

namespace com.icss.ge.Auction.Common.Svf
{
    //代理クラス
    public class SvfPrint : MarshalByRefObject
    {
        //リモートメソッド
        public Boolean IsActive()
        {
            return false;
        }

        //リモートメソッド
        public Boolean executePrint(List<string[]> arrayList)
        {
            return true;
        }
    }
}
