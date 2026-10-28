namespace HrManagement.Application.Positions
{
    public interface IPositionService
    {
        Task<Guid> CreateAsync(CreatePositionRequest request);

        Task<List<PositionResponse>> GetAllAsync();

        Task<PositionResponse> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(Guid id, UpdatePositionRequest request);

        Task<bool> DeleteAsync(Guid id);
    }
}
