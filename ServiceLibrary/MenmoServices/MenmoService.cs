using DataLibrary.Domain;
using DataLibrary.Dtos;
using DataLibrary.PageList;
using DataLibrary.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.MenmoServices
{
    public class MenmoService:IMenmoService
    {
        private readonly IRepository<Menmo> _repository;
        public MenmoService(IRepository<Menmo> repository)
        {
            _repository = repository;
        }
        public async Task<int> AddMenmoAsync(Menmo menmo)
        {
            if (menmo == null)
                throw new ArgumentNullException();
            return await _repository.InsertAsync(menmo);

        }

        public async Task<int> DeleteMenmoAsync(int id)
        {
            if (id <= 0)
                return 0;
            return await _repository.DeleteAsync(id);
        }

        public async Task<Menmo> GetMenmoByIDAsync(int id)
        {
            if(id <= 0) throw new ArgumentNullException();
            return await _repository.GetEntityByIdAsync(id);
        }

        public async Task<IList<Menmo>> GetMenmoListAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<int> UpdateMenmoAsync(Menmo menmo)
        {
            if(menmo == null) throw new ArgumentNullException();
           return await _repository.UpdateAsync(menmo);
        }
    }
}
