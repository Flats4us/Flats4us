using Flats4us.Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Services.Interfaces
{
    public interface IEquipmentService
    {
        Task<List<EquipmentDto>> GetAll(string? name = null);
    }
}
