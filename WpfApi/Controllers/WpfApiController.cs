using AutoMapper;
using DataLibrary.Domain;
using DataLibrary.Dtos;
using DataLibrary.PageList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceLibrary.MenmoServices;
using ServiceLibrary.ToDoServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WpfApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class WpfApiController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        private readonly IMenmoService _menmoService;
        private readonly IMapper _mapper;
        public WpfApiController(IToDoService toDoService, IMenmoService menmoService, IMapper mapper)
        {
            this._toDoService = toDoService;
            this._menmoService = menmoService;
            this._mapper = mapper;
        }

        #region 登录/注册

        [HttpPost]
        public int Login(UserDto userDto)
        {
            return 1;
        }

        [HttpPost]
        public int Register(UserDto userDto)
        {
            return 1;
        }
        #endregion

        #region 待办API
        // GET: api/<WpfApiController>
        [HttpPost]
        public async Task<IList<ToDoDto>> GetToDoListAsync(SearchDto searchDto)
        {
            var todo = await _toDoService.GetToDoListAsync();
            List<ToDo> dlist = null;
            if (todo != null && searchDto != null)
            {
                if (!string.IsNullOrEmpty(searchDto.Title) && searchDto.Status > 0)
                {
                    dlist = todo.Where(t => t.Title.Contains(searchDto.Title) && t.Status == searchDto.Status).ToList();
                }
                else if (!string.IsNullOrEmpty(searchDto.Title) && searchDto.Status == 0)
                {
                    dlist = todo.Where(t => t.Title.Contains(searchDto.Title)).ToList();
                }
                else if (string.IsNullOrEmpty(searchDto.Title) && searchDto.Status > 0)
                {
                    dlist = todo.Where(t => t.Status == searchDto.Status).ToList();
                }
                else
                {
                    dlist = todo.ToList();
                }
            }

            List<ToDoDto> list = _mapper.Map<List<ToDoDto>>(dlist);
            return list;
        }

        [HttpPost]
        public async Task<PagedList<ToDoDto>> GetToDoListPageAsync(int pageIndex, int pageSize, int indexFrom, SearchDto searchDto)
        {
            PagedList<ToDoDto> pagedList = null;
            try
            {
                var data = await _toDoService.GetToDoListAsync();
                List<ToDo> dlist = null;
                if (data != null && searchDto != null)
                    dlist = data.Where(t => string.IsNullOrEmpty(searchDto.Title) ? true : t.Title.Contains(searchDto.Title) && searchDto.Status == 0 ? true : t.Status == searchDto.Status).ToList();
                var list = _mapper.Map<List<ToDoDto>>(dlist);
                pagedList = new PagedList<ToDoDto>(list, pageIndex, pageSize, indexFrom);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pagedList;
        }

        // GET api/<WpfApiController>/5
        [HttpGet("{id}")]
        public async Task<ToDoDto> GetToDo(int id)
        {
            var toDo = await _toDoService.GetToDoByIDAsync(id);
            ToDoDto dto = _mapper.Map<ToDoDto>(toDo);
            return dto;
        }

        [HttpGet]
        public async Task<SummaryDto> GetSummary()
        {
            SummaryDto summary = new SummaryDto();
            var todoList = await _toDoService.GetToDoListAsync();
            var menmoList= await _menmoService.GetMenmoListAsync();
            summary.TodoList =_mapper.Map<List<ToDoDto>>(todoList.Where(t=>t.Status==1));
            summary.MemoList = _mapper.Map<List<MenmoDto>>(menmoList);
            summary.Sum = todoList.Count();
            summary.MemoCount = menmoList.Count();
            summary.CompltedCount = todoList.Where(t => t.Status == 2).Count();
            summary.CompltedRatio=(summary.CompltedCount/(double)summary.Sum).ToString("0%");
            return summary;
        }

        // POST api/<WpfApiController>
        [HttpPost]
        public async Task<ToDoDto> AddToDoAsync(ToDoDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException();
            ToDo todo = _mapper.Map<ToDo>(dto);
            int i = await _toDoService.AddToDoAsync(todo);
            dto = _mapper.Map<ToDoDto>(todo);
            return dto;
        }

        // PUT api/<WpfApiController>/5
        [HttpPost]
        public async Task<int> UpdateToDoAsync(ToDoDto dto)
        {
            if (dto == null) throw new ArgumentNullException();
            ToDo oldTodo = await _toDoService.GetToDoByIDAsync(dto.Id);
            oldTodo.Title = dto.Title;
            oldTodo.Status = dto.Status;
            oldTodo.Content = dto.Content;
            oldTodo.UpdateTime = dto.UpdateTime;
            int i = await _toDoService.UpdateToDoAsync(oldTodo);
            return i;
        }

        // DELETE api/<WpfApiController>/5
        [HttpDelete("{id}")]
        public async Task<int> DeleteToDoAsync(int id)
        {
            if (id <= 0) throw new ArgumentNullException();
            int i = await _toDoService.DeleteToDoAsync(id);
            return i;
        }

        #endregion

        #region 备忘录API

        // GET: api/<WpfApiController>
        [HttpPost]
        public async Task<IList<MenmoDto>> GetMenmoListAsync(SearchDto searchDto)
        {
            var data = await _menmoService.GetMenmoListAsync();
            List<Menmo> dlist = null;
            if (!string.IsNullOrEmpty(searchDto.Title))
            {
                dlist = data.Where(t => t.Title.Contains(searchDto.Title)).ToList();
            }
            else
            {
                dlist = data.ToList();
            }
            List<MenmoDto> list = _mapper.Map<List<MenmoDto>>(dlist);
            return list;
        }


        [HttpPost]
        public async Task<PagedList<MenmoDto>> GetMenmoDtoListPageAsync(int pageIndex, int pageSize, int indexFrom, SearchDto searchDto)
        {
            PagedList<MenmoDto> pagedList = null;
            try
            {
                var data = await _menmoService.GetMenmoListAsync();
                List<Menmo> dlist = null;
                if (data != null && searchDto != null)
                {
                    if (!string.IsNullOrEmpty(searchDto.Title))
                    {
                        dlist = data.Where(t => t.Title.Contains(searchDto.Title)).ToList();
                    }
                    else
                    {
                        dlist = data.ToList();
                    }
                }
                var list = _mapper.Map<List<MenmoDto>>(dlist);
                pagedList = new PagedList<MenmoDto>(list, pageIndex, pageSize, indexFrom);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pagedList;
        }


        // GET api/<WpfApiController>/5
        [HttpGet("{id}")]
        public async Task<MenmoDto> GetMenmoAsync(int id)
        {
            var toDo = await _menmoService.GetMenmoByIDAsync(id);
            MenmoDto dto = _mapper.Map<MenmoDto>(toDo);
            return dto;
        }

        // POST api/<WpfApiController>
        [HttpPost]
        public async Task<MenmoDto> AddMenmoAsync(MenmoDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException();
            Menmo todo = _mapper.Map<Menmo>(dto);
            int i = await _menmoService.AddMenmoAsync(todo);
            dto = _mapper.Map<MenmoDto>(todo);
            return dto;
        }

        // PUT api/<WpfApiController>/5
        [HttpPost]
        public async Task<int> UpdateMenmoAsync(MenmoDto dto)
        {
            if (dto == null) throw new ArgumentNullException();
            Menmo oldTodo = await _menmoService.GetMenmoByIDAsync(dto.Id);
            oldTodo.Title = dto.Title;
            oldTodo.Status = dto.Status;
            oldTodo.Content = dto.Content;
            oldTodo.UpdateTime = dto.UpdateTime;
            int i = await _menmoService.UpdateMenmoAsync(oldTodo);
            return i;
        }

        // DELETE api/<WpfApiController>/5
        [HttpDelete("{id}")]
        public async Task<int> DeleteMenmoAsync(int id)
        {
            if (id <= 0) throw new ArgumentNullException();
            int i = await _menmoService.DeleteMenmoAsync(id);
            return i;
        }

        #endregion
    }
}
