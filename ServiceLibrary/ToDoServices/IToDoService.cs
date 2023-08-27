using DataLibrary.Domain;
using DataLibrary.PageList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.ToDoServices
{
    public interface IToDoService
    {

        public Task<ToDo> GetToDoByIDAsync(int id);

        public Task<IList<ToDo>> GetToDoListAsync();

        public Task<int> AddToDoAsync(ToDo toDo);

        public Task<int> UpdateToDoAsync(ToDo toDo);

        public Task<int> DeleteToDoAsync(int id);
    }
}
