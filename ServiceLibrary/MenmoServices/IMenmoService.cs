using DataLibrary.Domain;
using DataLibrary.Dtos;
using DataLibrary.PageList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.MenmoServices
{
    public interface IMenmoService
    {
        public Task<Menmo> GetMenmoByIDAsync(int id);

        public Task<IList<Menmo>> GetMenmoListAsync();

        public Task<int> AddMenmoAsync(Menmo toDo);

        public Task<int> UpdateMenmoAsync(Menmo toDo);

        public Task<int> DeleteMenmoAsync(int id);
    }
}
