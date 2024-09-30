
using System.Net;

namespace Demo.BusinessLogicLayer.Repository
{
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeeRepository
    {
        //private readonly DataContext _dataContext;
        ////ctor injection
        //public EmployeeRepository(DataContext dataContext)
        //{
        //    _dataContext = dataContext;
        //}
        //public int Create(Employee entity)
        //{
        //    _dataContext.Employees.Add(entity);
        //    return _dataContext.SaveChanges();
        //}

        //public int Delete(Employee entity)
        //{
        //    _dataContext.Employees.Remove(entity);
        //    return _dataContext.SaveChanges();
        //}

        //public Employee? Get(int id) => _dataContext.Employees.Find();
        //public IEnumerable<Employee> GetAll() => _dataContext.Employees.ToList();

        //public int Update(Employee entity)
        //{
        //    _dataContext.Employees.Update(entity);
        //    return _dataContext.SaveChanges();
        //}
        public EmployeeRepository(DataContext context):base(context)
        {
            
        }
        public async Task<IEnumerable<Employee>> GetAllAsync(string name)
        => await _entities.Where(e => e.Name.ToLower().Contains(name.ToLower())).Include(e => e.Department).ToListAsync();

        public async Task<IEnumerable<Employee>> GetAllWithDepartmentAsync()
            => await _entities.Include(e => e.Department).ToListAsync();
    }
}
