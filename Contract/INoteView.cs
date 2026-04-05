using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DailyAccount.Forms;
using DailyAccount.Models;

namespace DailyAccount.Contract
{
    public interface INoteView
    {     
        void UpdateDataView(List<NoteModel> lists);
        void Reload();
    }
}
