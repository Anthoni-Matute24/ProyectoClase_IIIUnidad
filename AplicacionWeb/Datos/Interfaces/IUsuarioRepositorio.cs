﻿using Modelos;

namespace Datos.Interfaces
{
    // Contiene métodos para guardar, consultar, actualizar y eliminar usuarios
    public interface IUsuarioRepositorio
    {
        // Métodos
        Task<Usuario> GetPorCodigo(string codigo); // Devuelve un usuario

        Task<bool> NuevoUsuario(Usuario usuario); // Crear nuevo usuario.

        Task<bool> ActualizarUsuario(Usuario usuario); // Actualizar un usuario.

        Task<bool> EliminarUsuario(string codigo); // Eliminar un usuario.

        Task<IEnumerable<Usuario>> GetLista(); // Devuelve la lista de usuarios.
    }
}
