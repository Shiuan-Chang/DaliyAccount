using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyAccount.Models;

namespace DailyAccount.Utility.Strategy.Interface
{
    public interface IGroupingStrategy
    {
        List<AnalysisModel> GroupData(List<AnalysisRawDataDAO> rawData, List<string> conditionTypes, List<string> analyzeTypes);
    }
}
