using CleanArchitecture.Application.Models.Identity;

namespace CleanArchitecture.Application.Contracts.Identity
{
    public interface IUserServices
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string userId);
        public string UserId { get;}
    }
}
