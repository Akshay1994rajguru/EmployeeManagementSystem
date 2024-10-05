using EmsServices.Model;

namespace EmsServices.Repository
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeByID(int ID);
        Task<Employee> AddEmployee(Employee objUsers);
        Task<Employee> UpdateEmployee(Employee objUsers);
        bool DeleteEmployee(int ID);
    }
}
