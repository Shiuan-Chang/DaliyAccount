using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using DailyAccount.Models;
using DailyAccount.Utility.Strategy.Interface;

namespace DailyAccount.Utility.Strategy.ChartStrategy
{
    public class PieChartStrategy : ICreateChartStrategy
    {
        public void SetChartArea(Chart chart)
        {
            // 設定圓餅圖的圖表區域屬性
            if (chart.ChartAreas.Count == 0)
            {
                var chartArea = new ChartArea("MainArea")
                {
                    BackColor = Color.Transparent
                };
                chart.ChartAreas.Add(chartArea);
            }
        }
    }
}
