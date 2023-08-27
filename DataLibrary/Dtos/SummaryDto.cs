using DataLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Dtos
{
    public class SummaryDto
    {
        public int Sum { get; set; }

        public int CompltedCount { get; set; }

        public int MemoCount { get; set; }

        public string CompltedRatio { get; set; }

        public List<ToDoDto> TodoList { get;set; }

        public List<MenmoDto> MemoList { get; set; }
    }
}
