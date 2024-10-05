using EmsServices.Model;
using Microsoft.EntityFrameworkCore;

namespace EmsServices.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly EmpDbContext _empDbContext;
        public EmployeeRepository(EmpDbContext dbContext)
        {
            _empDbContext = dbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {

            _empDbContext.Employees.Add(employee);
            await _empDbContext.SaveChangesAsync();
            return employee;
        }

        public bool DeleteEmployee(int ID)
        {
            bool result = false;
            var Employees = _empDbContext.Employees.Find(ID);
            if (Employees != null)
            {
                _empDbContext.Entry(Employees).State = EntityState.Deleted;
                _empDbContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _empDbContext.Employees?.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByID(int ID)
        {
            return await _empDbContext.Employees.FindAsync(ID);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _empDbContext.Entry(employee).State = EntityState.Modified;
            await _empDbContext.SaveChangesAsync();
            return employee;
        }
    }
}
