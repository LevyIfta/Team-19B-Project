﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class ProductInfoDAL
    {
        private static List<ProductInfoData> ProductInfos;

        public static ProductInfoData getProductInfo(string productName, string manufacturer)
        {
            foreach (ProductInfoData productInfoData in ProductInfos)
            {
                if (productInfoData.productName == productName && productInfoData.manufacturer == manufacturer)
                    return productInfoData;
            }
            return null;
        }


        public static bool isExist(string productName, string manufacturer)
        {
            foreach (ProductInfoData productInfoData in ProductInfos)
            {
                if (productInfoData.productName == productName && productInfoData.manufacturer == manufacturer)
                    return true;
            }
            return false;

        }

        public static void addProductInfo(ProductInfoData productInfoData)
        {
            ProductInfos.Add(productInfoData);
        }

        public static bool update(ProductInfoData productInfoData)
        {
            if (!ProductInfos.Remove(productInfoData))
                return false;
            ProductInfos.Add(productInfoData);
            return true;

        }

        public static bool remove(ProductInfoData productInfoData)
        {
            return ProductInfos.Remove(productInfoData);
        }
    }
}