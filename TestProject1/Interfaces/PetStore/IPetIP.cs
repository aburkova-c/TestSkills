using apitest.DTO.PetModelsDTO;
using Refit;

namespace apitest.Interfaces.PetStore;

public interface IPetIP
{
    [Get("/pets")]
    Task<AllPetsResponseDTO> GetAllPetsAsync();
    
    // получение по ID
    [Get("/pets/{id}")]
    Task<PetDTO> GetPetByIdAsync(string id);
    
    [Get("/pets")]
    Task<AllPetsResponseDTO> GetAllPetsByStatusAndLimitAsync([Query] string status, [Query] int limit);
    
}