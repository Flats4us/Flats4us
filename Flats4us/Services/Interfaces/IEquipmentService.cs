using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IEquipmentService
    {
        Task<List<EquipmentDto>> GetAll();
    }
}
