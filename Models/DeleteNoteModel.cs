using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyAccount.Models
{
    public class DeleteNoteModel
    {
        public string DeleteDataDate;

        public DeleteNoteModel(string DeleteDataDate)
        {
            this.DeleteDataDate = DeleteDataDate;
        }
    }
}
