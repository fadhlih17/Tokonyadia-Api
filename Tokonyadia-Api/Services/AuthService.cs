using Microsoft.Win32.SafeHandles;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Exceptions;
using Tokonyadia_Api.Middlewares;
using Tokonyadia_Api.Repositories;
using Tokonyadia_Api.Security;

namespace Tokonyadia_Api.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<UserCredential> _repository;
    private readonly Ipersistance _persistance;

    private readonly IRoleService _roleService;
    private readonly ICustomerService _customerService;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IRepository<UserCredential> repository, Ipersistance persistance, IRoleService roleService,
        ICustomerService customerService, IJwtUtils jwtUtils)
    {
        _repository = repository;
        _persistance = persistance;
        _roleService = roleService;
        _customerService = customerService;
        _jwtUtils = jwtUtils;
    }
    
    public async Task<UserCredential> LoadByEmail(string email)
    {
        var user = await _repository.Find(credential => credential.Equals(email));
        if (user is null) throw new NotFoundException("User not found");
        return user;
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        var registerResponse = await _persistance.ExecuteTransactionAsync(async () =>
        {
            // Menyimpan role
            var role = await _roleService.GetOrSave(ERole.Customer);

            // Menyimpan usercredential
            var userCredential = new UserCredential
            {
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = role
            };
            var saveUser = await _repository.Save(userCredential);

            // menyimpan customer yang berrelasi dengan usercredential
            await _customerService.Create(new Customer { PhoneNumber = request.PhoneNumber, UserCredential = saveUser });

            return new RegisterResponse
            {
                Email = saveUser.Email,
                Role = saveUser.Role.ERole.ToString()
            };
        });

        return registerResponse;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _repository.Find(credential => credential.Email.Equals(request.Email), new []{"Role"});
        if (user is null) throw new UnauthorizedException("Unauthorized");

        var verify = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!verify) throw new UnauthorizedException("Unauthorized");

        // Generate Token
        var token = _jwtUtils.GenerateToken(user);
        
        return new LoginResponse
        {
            Email = user.Email,
            Role = user.Role.ERole.ToString(),
            Token = token
        };
    }
    
    public async Task<RegisterResponse> RegisterAdmin(RegisterRequest request)
    {
        var registerResponse = await _persistance.ExecuteTransactionAsync(async () =>
        {
            // Menyimpan role
            var role = await _roleService.GetOrSave(ERole.Admin);

            // Menyimpan usercredential
            var userCredential = new UserCredential
            {
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = role
            };
            var saveUser = await _repository.Save(userCredential);
            await _persistance.SaveChangesAsync();
            
            // menyimpan customer yang berrelasi dengan usercredential
            // await _customerService.Create(new Customer { PhoneNumber = request.PhoneNumber, UserCredential = saveUser });

            return new RegisterResponse
            {
                Email = saveUser.Email,
                Role = saveUser.Role.ERole.ToString()
            };
        });

        return registerResponse;
    }
}