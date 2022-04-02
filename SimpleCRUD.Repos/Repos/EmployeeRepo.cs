using SimpleCRUD.DTO;
using SimpleCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SimpleCRUD.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private SimpleCrudDbContext _dbContext;
        public EmployeeRepo(SimpleCrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            var userData = await (from u in _dbContext.TblUsers
                                  join e in _dbContext.TblEmployees on u.UserId equals e.UserId
                                  join ad in _dbContext.TblAddresses on u.UserId equals ad.Userid
                                  where u.IsActive
                                  select new EmployeeDTO
                                  {
                                      UserId = u.UserId,
                                      Dob = u.Dob,
                                      Doj = e.Doj,
                                      EmployeeId = e.EmployeeId,
                                      FirstName = u.FirstName,
                                      IsActive = u.IsActive,
                                      LastName = u.LastName,
                                      PhoneNumber = u.PhoneNumber,
                                      AddressLine1 = ad.AddressLine1,
                                      AddressLine2 = ad.AddressLine2,
                                      District = ad.District,
                                      PinCode = ad.PinCode,
                                      State = ad.State
                                  }).ToListAsync();
            return userData;
        }
        public async Task<EmployeeDTO> GetEmployeeById(int userid)
        {
            var userData = await (from u in _dbContext.TblUsers
                                  join e in _dbContext.TblEmployees on u.UserId equals e.UserId
                                  join ad in _dbContext.TblAddresses on u.UserId equals ad.Userid
                                  where u.UserId == userid
                                  select new EmployeeDTO
                                  {
                                      UserId = u.UserId,
                                      Dob = u.Dob,
                                      Doj = e.Doj,
                                      EmployeeId = e.EmployeeId,
                                      FirstName = u.FirstName,
                                      IsActive = u.IsActive,
                                      LastName = u.LastName,
                                      PhoneNumber = u.PhoneNumber,
                                      AddressLine1 = ad.AddressLine1,
                                      AddressLine2 = ad.AddressLine2,
                                      District = ad.District,
                                      PinCode = ad.PinCode,
                                      State = ad.State
                                  }).FirstOrDefaultAsync();
            return userData;
        }
        public async Task<bool> AddEmployee(EmployeeDTO dto)
        {
            var res = false;
            var userData = await _dbContext.TblUsers.Where(x => x.UserId == dto.UserId).FirstOrDefaultAsync();
            if (userData != null)
            {
                userData.FirstName = dto.FirstName;
                userData.LastName = dto.LastName;
                userData.PhoneNumber = dto.PhoneNumber;
                userData.Dob = dto.Dob;
                userData.UpdatededDate = DateTime.Now;
                _dbContext.TblUsers.Update(userData);
                res = await _dbContext.SaveChangesAsync() > 0;
                if (res)
                {
                    var empData = await _dbContext.TblEmployees.Where(x => x.UserId == userData.UserId).FirstOrDefaultAsync();
                    if (empData != null)
                    {
                        empData.Doj = dto.Doj;
                        empData.UpdatededDate = DateTime.Now;
                        _dbContext.TblEmployees.Update(empData);
                        res = await _dbContext.SaveChangesAsync() > 0;
                    }
                }
                if (res)
                {
                    var addrData = await _dbContext.TblAddresses.Where(x=>x.Userid == userData.UserId).FirstOrDefaultAsync();
                    if (addrData!=null)
                    {
                        addrData.AddressLine1 = dto.AddressLine1;
                        addrData.AddressLine2 = dto.AddressLine2;
                        addrData.District = dto.District;
                        addrData.IsActive = true;
                        addrData.UpdatedDate = DateTime.Now;
                        addrData.PinCode = dto.PinCode;
                    }
                    _dbContext.TblAddresses.Update(addrData);
                    res = await _dbContext.SaveChangesAsync() > 0;
                }
            }
            else
            {
                var dbModel = new TblUsers
                {
                    CreatedDate = DateTime.Now,
                    Dob = dto.Dob,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    IsActive = true,
                    PhoneNumber = dto.PhoneNumber,
                    UpdatededDate = DateTime.Now
                };
                await _dbContext.TblUsers.AddAsync(dbModel);
                res = await _dbContext.SaveChangesAsync() > 0;
                if (res)
                {
                    var empModel = new TblEmployees
                    {
                        CreatedDate = DateTime.Now,
                        Doj = dto.Doj,
                        IsActive = true,
                        UpdatededDate = DateTime.Now,
                        UserId = dbModel.UserId,
                    };
                    await _dbContext.TblEmployees.AddAsync(empModel);
                    res = await _dbContext.SaveChangesAsync() > 0;
                }
                if (res)
                {
                    var addrModel = new TblAddress
                    {
                        AddressLine1 = dto.AddressLine1,
                        AddressLine2 = dto.AddressLine2,
                        CreatedDate = DateTime.Now,
                        District = dto.District,
                        IsActive = dto.IsActive,
                        PinCode = dto.PinCode,
                        State = dto.State,
                        UpdatedDate = DateTime.Now,
                        Userid = dbModel.UserId
                    };
                    await _dbContext.TblAddresses.AddAsync(addrModel);
                    res = await _dbContext.SaveChangesAsync() > 0;
                }
            }
            return res;
        }
        public async Task<bool> DeleteEmployee(int userid)
        {
            var res = false;
            var empdata = await _dbContext.TblEmployees.Where(x => x.UserId == userid).FirstOrDefaultAsync();
            if (empdata != null)
            {
                empdata.IsActive = false;
                empdata.UpdatededDate = DateTime.Now;
                _dbContext.TblEmployees.Update(empdata);
                res = await _dbContext.SaveChangesAsync() > 0;
                if (res)
                {
                    var userData = await _dbContext.TblUsers.Where(x => x.UserId == userid).FirstOrDefaultAsync();
                    if (userData != null)
                    {
                        userData.IsActive = false;
                        userData.UpdatededDate = DateTime.Now;
                        _dbContext.TblUsers.Update(userData);
                        res = await _dbContext.SaveChangesAsync() > 0;
                    }
                }
                if (res)
                {
                    var addrdata = await _dbContext.TblAddresses.Where(x => x.Userid == userid).FirstOrDefaultAsync();
                    if (addrdata!=null)
                    {
                        addrdata.IsActive = false;
                        addrdata.UpdatedDate = DateTime.Now;
                        _dbContext.TblAddresses.Update(addrdata);
                        res = await _dbContext.SaveChangesAsync() > 0;
                    }
                }
            }
            return res;
        }
    }
}
