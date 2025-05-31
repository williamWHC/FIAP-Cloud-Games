using Application.DTOs;
using AutoMapper;
using Domain.Entity;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class PessoaService
    {
        IPessoaRepository _pessoaRepository;
        IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PessoaService(IPessoaRepository pessoaRepository, IMapper mapper, IConfiguration configuration)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task AddPessoa(PessoaDTO pessoaDTO)
        {
            if (!IsEmailValid(pessoaDTO.Email))
                throw new Exception("sadas");

            if (!IsSenhaValid(pessoaDTO.Senha)) 
                throw new Exception("asd");

            pessoaDTO.Senha = GenerateGuidFromSenha(pessoaDTO.Senha);
            var pessoaToAdd = _mapper.Map<Pessoa>(pessoaDTO);
            await _pessoaRepository.Add(pessoaToAdd);
        }

        public async Task ReactivatePessoaById(int id)
        {
            Pessoa pessoa = await _pessoaRepository.GetById(id);

            if (pessoa == null)
                throw new Exception("a");

            pessoa.IsActive = true;
            await _pessoaRepository.Update(pessoa);
        }

        public async Task<LoggedDTO> Login(LoginDTO loginDTO)
        {
            loginDTO.Senha = GenerateGuidFromSenha(loginDTO.Senha);
            Pessoa pessoa = await _pessoaRepository.GetPessoaByEmailESenha(loginDTO.Email, loginDTO.Senha);
            if (pessoa == null || !pessoa.IsActive)
                throw new Exception("asdas");

            LoggedDTO loggedDTO = new LoggedDTO() { Email = loginDTO.Email, Token = GenerateJwtToken(pessoa)};
            return loggedDTO; 
        }

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private bool IsSenhaValid(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                return false;

            // Pelo menos 8 caracteres, 1 letra, 1 número e 1 caractere especial
            var pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&^+=_\-])[A-Za-z\d@$!%*#?&^+=_\-]{8,}$";
            return Regex.IsMatch(senha, pattern);
        }

        private string GenerateGuidFromSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

            var guidBytes = new byte[16];
            Array.Copy(hash, guidBytes, 16);

            var guid = new Guid(guidBytes);
            return guid.ToString();
        }

        private string GenerateJwtToken(Pessoa pessoa)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, pessoa.Email),
                new Claim(ClaimTypes.Role, pessoa.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiresInMinutes"])),
                issuer: _configuration["Jwt:Issuer"],
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
