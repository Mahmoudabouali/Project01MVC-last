﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _employeeRepository = new Lazy<IEmployeeRepository>(()=> new EmployeeRepository(dataContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dataContext));
            _dataContext = dataContext;
        }

        public IEmployeeRepository Employees => _employeeRepository.Value;
        public IDepartmentRepository Departments => _departmentRepository.Value;


        public async Task<int> SaveChangesAsync() => await _dataContext.SaveChangesAsync();
    }
}
