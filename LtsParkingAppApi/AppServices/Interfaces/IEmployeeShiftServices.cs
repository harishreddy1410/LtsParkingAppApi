//---------------------------------------------------------------------------------------
// Description: interface for the Employee shift service
//---------------------------------------------------------------------------------------
using AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IEmployeeShiftServices
    {
        Task<EmployeeShiftDtoOutput> Get(int id = 0);

        Task<bool> Create(EmployeeShiftDtoInput userProfileDtoInput);

        Task<bool> Update(EmployeeShiftDtoInput userProfileDtoInput);

        Task<bool> Delete(int id, int? DeletedBy);

        Task<List<EmployeeShiftDtoOutput>> GetAll(bool includeInactive);
    }
}
