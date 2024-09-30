namespace Demo.BusinessLogicLayer.Repository
{
    public class DepartmentRepository :GenaricRepository<Department> , IDepartmentRepository
    {
        /*
        //dependency innjection
        //method innjection => method([FromServices]DataContext dataContext)

        //proprity injection => [FromServices]
        //public DataContext DataContext {get; set;}


        
         * Get , get all , create , update , delete
         

        //private DataContext dataContext = new DataContext(); //hard codded dependency
*/
        //private readonly DataContext _dataContext;
        ////ctor injection
        //public DepartmentRepository(DataContext dataContext)
        //{
        //    _dataContext = dataContext;
        //}

        //public Department? Get(int id) => _dataContext.Departments.Find(id);

        //public IEnumerable<Department> GetAll() => _dataContext.Departments.ToList();

        //public int Create(Department entity)
        //{
        //    _dataContext.Departments.Add(entity);
        //    return _dataContext.SaveChanges();
        //}
        //public int Update(Department entity)
        //{
        //    _dataContext.Departments.Update(entity);
        //    return _dataContext.SaveChanges();
        //}
        //public int Delete(Department entity)
        //{
        //    _dataContext.Departments.Remove(entity);
        //    return _dataContext.SaveChanges();
        //}
        public DepartmentRepository(DataContext context) : base(context)
        {

        }
    }
}
