using DataLibrary.Domain;
using DataLibrary.Repositorys;

namespace ServiceLibrary.ToDoServices
{
    public class ToDoService : IToDoService
    {
        private readonly IRepository<ToDo> _repository;
        public ToDoService(IRepository<ToDo> repository)
        {
            _repository = repository;
        }
        public async Task<int> AddToDoAsync(ToDo toDo)
        {
            if (toDo == null)
                throw new ArgumentNullException();
            return await _repository.InsertAsync(toDo);

        }

        public async Task<int> DeleteToDoAsync(int id)
        {
            if (id <= 0)
                return 0;
            return await _repository.DeleteAsync(id);
        }

        public async Task<ToDo> GetToDoByIDAsync(int id)
        {
            if(id <= 0) throw new ArgumentNullException();
            return await _repository.GetEntityByIdAsync(id);
        }

        public async Task<IList<ToDo>> GetToDoListAsync()
        {
           return await _repository.GetAllAsync();
        }

        public async Task<int> UpdateToDoAsync(ToDo toDo)
        {
            if (toDo == null) throw new ArgumentNullException();
            return await _repository.UpdateAsync(toDo);
        }
    }
}
