using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.DomainDto;

namespace WpfDemo.Service.MenmoService
{
    public interface IMenmoService
    {
        public Task<ApiResponse<List<MemoDto>>> GetMemoDtosAsync(SearchDto searchDto);
        public Task<ApiResponse<List<MemoDto>>> GetMemoDtosPageAsync(int pageIndex, int pageSize, int indexFrom, SearchDto searchDto);
        public Task<MemoDto> GetMemoDtoByIdAsync(int id);
        public Task<ApiResponse> AddMemoDtoAsync(MemoDto toDoDto);
        public Task<ApiResponse> UpdateMemoDtoAsync(MemoDto toDoDto);
        public Task<ApiResponse> DeleteMemoDtoAsync(int id);
    }
}
