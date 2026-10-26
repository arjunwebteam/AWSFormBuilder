using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
    public class CurrencyCodes
    {
        private Int64 _CurrencyCodeId;

        public Int64 CurrencyCodeId
        {
            get { return _CurrencyCodeId; }
            set { _CurrencyCodeId = value; }
        }
        private string _CurrencyCode;

        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
    }
}
