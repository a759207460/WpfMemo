using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.DomainDto;

namespace WpfDemo.Service.ToDoService
{
    public interface IToDoService
    {
        public Task<ApiResponse<List<ToDoDto>>> GetToDoDtosAsync(SearchDto searchDto);
        public Task<ApiResponse<List<ToDoDto>>> GetToDoDtosPageAsync(int pageIndex, int pageSize, int indexFrom, SearchDto searchDto);
        public Task<ApiResponse<SummaryDto>> GetSummary();
        public Task<ToDoDto> GetToDoByIdAsync(int id);
        public Task<ApiResponse> AddToDoAsync(ToDoDto toDoDto);
        public Task<ApiResponse> UpdateToDoAsync(ToDoDto toDoDto);
        public Task<ApiResponse> DeleteToDoAsync(int id);


    }
}
