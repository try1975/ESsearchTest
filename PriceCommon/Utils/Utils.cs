﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using PriceCommon.Enums;

namespace PriceCommon.Utils
{
    public static class Utils
    {
        public static double GetPriceCalculation(List<double> prices, out string calculationText)
        {
            prices.Sort();
            var count = prices.Count;

            var sbText = new StringBuilder();
            sbText.Append(
                @"1.Выборка ценовых показателей упорядочивается по возрастанию. Каждому значению цены, начиная с минимального, присваивается порядковый номер:");
            var icount = 0;
            foreach (var price in prices)
            {
                icount++;
                sbText.Append($" {icount}) {price};");
            }
            sbText.Append(Environment.NewLine);
            sbText.AppendLine($"2. Количество ценовых показателей K= {prices.Count}");
            if (prices.Count < 10)
            {
                sbText.Append(@"3. В выборке должно быть не менее чем 10 цен");
                calculationText = sbText.ToString();
                return -2;
            }

            var min = count / 4;
            var max = count * 3 / 4;
            var del = true;
            if (count % 4 > 0)
            {
                min++;
                max++;
                del = false;
            }
            var curCount = 1;
            var minPrice = Convert.ToDouble(0);
            var maxPrice = Convert.ToDouble(0);
            var nextMinPrice = Convert.ToDouble(0);
            var nextMaxPrice = Convert.ToDouble(0);
            foreach (var price in prices)
            {
                if (curCount == min)
                {
                    minPrice = price;
                }
                if (curCount == min + 1)
                {
                    nextMinPrice = price;
                }
                if (curCount == max)
                {
                    maxPrice = price;
                }
                if (curCount == max + 1)
                {
                    nextMaxPrice = price;
                }
                curCount++;
            }
            var minInd = "Ц[" + min + "] ";
            var maxInd = "Ц[" + max + "] ";
            if (del)
            {
                minPrice = (minPrice + nextMinPrice) / 2;
                minInd = "(Ц[" + min + "]+Ц[" + (min + 1) + "])/2 ";
                maxPrice = (maxPrice + nextMaxPrice) / 2;
                maxInd = "(Ц[" + max + "]+Ц[" + (max + 1) + "])/2 ";
            }
            sbText.AppendLine($"3. Минимальное значение цены Цмин={minInd} ={minPrice}");
            sbText.AppendLine($"4. Максимальное значение цены Цмакс={maxInd} ={maxPrice}");
            if (maxPrice >= minPrice * 5 / 4)
            {
                maxPrice = minPrice * 5 / 4;
                sbText.AppendLine($"5. Верхняя граница диапазона Limmax={maxPrice}");
            }
            var sum = 0.0;
            var countPriceInCalc = 0;
            sbText.AppendLine($"6. Выборка цен, входящая в диапазон от  {minPrice} до  {maxPrice}:");
            foreach (var price in prices)
            {
                if (!(price >= minPrice) || !(price <= maxPrice)) continue;
                sbText.Append($" {price}; ");
                sum = sum + price;
                countPriceInCalc++;
            }
            sbText.Append(Environment.NewLine);

            double calculatedPrice;
            if (countPriceInCalc < 3)
            {
                calculatedPrice = -1;
                sbText.AppendLine(
                    @"7. В получившейся выборке цен недостаточно. Необходима экспертная оценка выборки, и определение параметров ценовых групп.");
            }
            else
            {
                calculatedPrice = Math.Round(sum / countPriceInCalc, 2, MidpointRounding.AwayFromZero);
            }
            sbText.Append($"7. Итого цена предмета государственного заказа: {calculatedPrice}");
            calculationText = sbText.ToString();
            return calculatedPrice;
        }

        public static IEnumerable<string> ReadLines(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static string GetDescription(Enum en)
        {
            if (en == null) return string.Empty;
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)

            {

                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumLocalizeAttribite),
                    false);

                if (attrs != null && attrs.Length > 0)

                    return ((EnumLocalizeAttribite)attrs[0]).Text;

            }

            return en.ToString();

        }

        public static DateTime LoopNextFrequencyDate(DateTime moment, DateTime date, Frequency frequency)
        {
            var nextDate = date;
            while (nextDate < moment)
            {
                nextDate = GetNextFrequencyDate(nextDate, frequency);
            }
            return nextDate;
        }


        public static DateTime GetNextFrequencyDate(DateTime date, Frequency frequency)
        {
            switch (frequency)
            {
                case Frequency.Daily: return date.AddDays(1);
                case Frequency.Weekly: return date.AddDays(7);
                case Frequency.Monthly: return date.AddMonths(1);
                case Frequency.Every2Month: return date.AddMonths(2);
                case Frequency.Quarterly: return date.AddMonths(3);
                case Frequency.HalfYearly: return date.AddMonths(6);
                case Frequency.Annually: return date.AddMonths(12);
                default: return date.AddDays(1);
            }
        }
    }
}