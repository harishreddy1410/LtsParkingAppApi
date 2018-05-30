using AppDomain.Models;
using AppDomain.Models.Interfaces;
using AppServices.Dto;
using AppServices.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class EmployeeShiftServices:IEmployeeShiftServices
    {
        IRepository _repo;
        IMapper _mapper;

        public EmployeeShiftServices(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<bool> Create(EmployeeShiftDtoInput employeeShiftDtoInput)
        {
            try
            {
                _repo.Create<EmployeeShift>(_mapper.Map<EmployeeShift>(employeeShiftDtoInput), "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
                throw;
            }

        }

        public Task<bool> Delete(int id, int? DeletedBy)
        {
            var toBeDeleted = _repo.GetById<EmployeeShift>(id);
            try
            {
                toBeDeleted.IsDeleted = true;
                toBeDeleted.IsActive = false;
                toBeDeleted.ModifiedBy = DeletedBy;
                _repo.Update(toBeDeleted);
                return Task.FromResult(true);

            }
            catch (System.Exception)
            {
                return Task.FromResult(true);
                throw;
            }
        }

        public Task<List<EmployeeShiftDtoOutput>> GetAll(bool includeInactive)
        {
            return Task.FromResult(_mapper.Map<List<EmployeeShiftDtoOutput>>(_repo.GetQueryable<EmployeeShift>(x => x.IsActive == (includeInactive == false ? true : x.IsActive))));
        }

        public Task<EmployeeShiftDtoOutput> Get(int id = 0)
        {
            try
            {
                return Task.FromResult(_mapper.Map<EmployeeShiftDtoOutput>(_repo.Get<EmployeeShift>(x => x.Id == id)));
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Task<bool> Update(EmployeeShiftDtoInput employeeShiftDtoOutput)
        {
            var updated = _mapper.Map<EmployeeShift>(employeeShiftDtoOutput);
            try
            {
                _repo.Update(updated, "API");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(true);
                throw;
            }
        }
    }
}
