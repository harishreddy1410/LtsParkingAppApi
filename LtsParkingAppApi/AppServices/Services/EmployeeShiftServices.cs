//---------------------------------------------------------------------------------------
// Description: crud oprations for the employee shifts
//---------------------------------------------------------------------------------------
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
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeShiftServices(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// create new emploee shift
        /// </summary>
        /// <param name="employeeShiftDtoInput"></param>
        /// <returns></returns>
        public Task<bool> Create(EmployeeShiftDtoInput employeeShiftDtoInput)
        {
            try
            {
                _repo.Create<EmployeeShift>(_mapper.Map<EmployeeShift>(employeeShiftDtoInput), "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Task<bool> Delete(int id, int? DeletedBy)
        {            
            try
            {
                var toBeDeleted = _repo.GetById<EmployeeShift>(id);
                toBeDeleted.IsDeleted = true;
                toBeDeleted.IsActive = false;
                toBeDeleted.ModifiedBy = DeletedBy;
                _repo.Update(toBeDeleted);
                return Task.FromResult(true);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return all the employee shifts
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public Task<List<EmployeeShiftDtoOutput>> GetAll(bool includeInactive)
        {
            try
            {
                return Task.FromResult(_mapper.Map<List<EmployeeShiftDtoOutput>>(_repo.GetQueryable<EmployeeShift>(x => x.IsActive == (includeInactive == false ? true : x.IsActive) && x.IsDeleted == false)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// return specific employee shift details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// update the employee shift details
        /// </summary>
        /// <param name="employeeShiftDtoOutput"></param>
        /// <returns></returns>
        public Task<bool> Update(EmployeeShiftDtoInput employeeShiftDtoOutput)
        {
            var updated = _mapper.Map<EmployeeShift>(employeeShiftDtoOutput);
            try
            {
                _repo.Update(updated, "API");
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
