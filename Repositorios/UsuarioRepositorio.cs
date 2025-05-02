using System.Runtime.InteropServices;
using back_teste.Data;
using back_teste.Model;
using back_teste.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace back_teste.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly LivrariaDBContext _dbContext;

        public UsuarioRepositorio(LivrariaDBContext dbContext)
        {
            _dbContext = dbContext;
        }
         async Task<List<UsuarioModel>> IUsuarioRepositorio.GetUsuarios()
        {
            return await _dbContext.users.ToListAsync();
        }

        async Task<List<UsuarioModel>> IUsuarioRepositorio.SetUsuarios(UsuarioModel usuario) 
        {
            _dbContext.users.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.users.ToListAsync(); 
            
        }

        async Task<UsuarioModel> IUsuarioRepositorio.UsuarioPorId(int id)
        {
            return await _dbContext.users.FirstOrDefaultAsync(x => x.id == id);
        }

        async Task<UsuarioModel> IUsuarioRepositorio.UpdateUsuarios(UsuarioModel newUsuario, int id)
        {

            UsuarioModel usuarioPorId = await _dbContext.users.FirstOrDefaultAsync(x => x.id == id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário por id: {id} não foi encontrado");
            }
            usuarioPorId.name = newUsuario.name;
            usuarioPorId.username = newUsuario.username;
            usuarioPorId.password = newUsuario.password;
            usuarioPorId.profile_picture = newUsuario.profile_picture;
            usuarioPorId.admin = newUsuario.admin;
            _dbContext.users.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();
            return usuarioPorId;
        }
    }
}
