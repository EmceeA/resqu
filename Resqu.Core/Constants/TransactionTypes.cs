using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Constants
{
    public static class TransactionTypes
    {
        public static string INFLOW = "INFLOW";
        public static string OUTFLOW = "OUTFLOW";
        public static string DEBIT = "DEBIT";
        public static string CREDIT = "CREDIT";
    }

    public static class ResponseCodes
    {
        public static string Response00Code = "00";
        public static string Response00Message = "Successful";



        public static string Response33Code = "33";
        public static string Response33Message = $"The @object was not found";



        public static string Response88Code = "88";
        public static string Response88Message = $"Insufficient Fund";
    }
        
}
