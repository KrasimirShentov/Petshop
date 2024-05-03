﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PetShop.Petshop.Models;
using PetShop.Petshop.Repositories.Interfaces;

namespace PetShop.Petshop.Repositories.Repositories
{
    public class EmployeeRepository : IEmployeeReposity
    {
        private readonly PetshopDB _dbContext;

        public EmployeeRepository(PetshopDB dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _dbContext.employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIDAsync(int employeeID)
        {
            return await _dbContext.employees.FindAsync(employeeID);
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _dbContext.employees.Add(employee);
            _dbContext.SaveChangesAsync();
            return employee;

        }

        public async Task DeleteEmployeeAsync(int employeeID)
        {
            var employeeToDelete = await GetEmployeeByIDAsync(employeeID);
            if (employeeToDelete != null)
            {
                _dbContext.employees.Remove(employeeToDelete);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException($"Employee with this ID: {employeeID} does not exist");
            }
        }


        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
