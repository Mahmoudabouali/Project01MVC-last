namespace Demo.BusinessLogicLayer.Intrerfaces
{
    public interface IEmployeeRepository : IGenaricRepository<Employee>
    {
        public Task<IEnumerable<Employee>> GetAllAsync(string name);
        public Task<IEnumerable<Employee>> GetAllWithDepartmentAsync();

    }
}
