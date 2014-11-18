using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Services
{
    public class HappyIndexCalculator
    {
        static public int Calculate(List<int> indicators)
        {
            if (indicators.Count == 0)
                return 0;

            decimal promoters = 0;
            decimal detractors = 0;
            var passives = 0;

            foreach (var indicator in indicators)
            {
                if (indicator > 0) 
                    promoters++;
                if (indicator < 0)
                    detractors++;
                if (indicator == 0)
                    passives++;
            }
            decimal percentagePromoters = promoters / indicators.Count * 100;
            decimal percentagDetractors = detractors / indicators.Count * 100;
            return Convert.ToInt32(Math.Round(percentagePromoters - percentagDetractors,MidpointRounding.AwayFromZero));
        }
    }
}