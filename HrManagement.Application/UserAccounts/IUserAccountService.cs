namespace HrManagement.Application.UserAccounts
{
    public interface IUserAccountService
    {
        Task<Guid> CreateAsync(CreateUserAccountRequest request);
        Task<List<UserAccountResponse>> GetAllAsync();
        Task<UserAccountResponse> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UpdateUserAccountRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
