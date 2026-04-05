using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyAccount.Models;

namespace DailyAccount.Contract
{
    public interface IAccountView
    {
        void UpdateDataView(List<AccountModel> lists);
    }
}
