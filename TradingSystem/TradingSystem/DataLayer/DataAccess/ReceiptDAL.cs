using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class ReceiptDAL
    {
        private static List<ReceiptData> receipts = new List<ReceiptData>();

        public static ReceiptData getReceipt(int receiptID)
        {
            foreach (ReceiptData receiptData in receipts)
            {
                if (receiptData.receiptID == receiptID)
                    return receiptData;
            }
            return null;
        }


        public static bool isExist(int receiptID)
        {
            foreach (ReceiptData receiptData in receipts)
            {
                if (receiptData.receiptID == receiptID)
                    return true;
            }
            return false;

        }

        public static void addReceipt(ReceiptData receiptData)
        {
            receipts.Add(receiptData);
        }

        public static bool update(ReceiptData receiptData)
        {
            if (!receipts.Remove(receiptData))
                return false;
            receipts.Add(receiptData);
            return true;

        }

        public static bool remove(ReceiptData receiptData)
        {
            return receipts.Remove(receiptData);
        }
    }
}
