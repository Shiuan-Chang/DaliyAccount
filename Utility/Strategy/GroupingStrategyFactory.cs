using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyAccount.Utility.Strategy.Interface;

namespace DailyAccount.Utility.Strategy
{
    public static class GroupingStrategyFactory
    {
        public static IGroupingStrategy GetGroupingStrategy(string chartType)
        {
            switch (chartType)
            {
                case "圓餅圖":
                    return new PieChartGroupingStrategy();
                case "堆疊圖":
                    return new StackedChartGroupingStrategy();
                //case "折線圖":
                //    return new LineChartGroupingStrategy();
                default:
                    throw new NotSupportedException($"不支持的圖表類型: {chartType}");
            }
        }
    }
}
