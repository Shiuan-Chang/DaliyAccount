using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using DailyAccount.Models;

namespace DailyAccount.Utility.Strategy.Interface
{
    public interface ICreateChartStrategy
    {
        void SetChartArea(Chart chart);
    }
}
