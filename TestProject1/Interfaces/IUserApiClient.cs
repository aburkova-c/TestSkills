using apitest.DTO;
using Refit;

namespace apitest.Interfaces;

[Headers("x-api-key: free_user_3HrWsxg43Ph37efyTBEFS2tnWPu")]
public interface IUserApiClient
{
    [Get("/users/{id}")]
    Task<UserResponseDTO> GetUserAsync(int id);
    
    [Post("/users")]
    Task<CreateUserResponseDTO> PostUserAsync([Body] CreateUserRequestDTO user);
    
    [Put("/users/{id}")]
    Task<CreateUserRequestDTO> PutUserAsync(int id, [Body] CreateUserRequestDTO user);
    
    [Delete("/users/{id}")]
    Task<ApiResponse<string>> DeleteUserAsync(int id);
}