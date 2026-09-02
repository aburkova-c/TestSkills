using apitest.DTO;
using apitest.DTO.BookStoreDTO;
using Refit;
using CreateUserResponseDTO = apitest.DTO.BookStoreDTO.CreateUserResponseDTO;

namespace apitest.Interfaces;

public interface IBookStore
{
    [Post("/Account/v1/User")]
    Task<CreateUserResponseDTO> CreateUserAsync([Body] UserDTO user);
    
    [Post("/Account/v1/GenerateToken")]
    Task<GetTokenDTO> GenerateTokenAsync([Body] LoginRequestDTO login);
}   
