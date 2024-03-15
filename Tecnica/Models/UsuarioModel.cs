namespace Tecnica.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public string? Role { get; set; }
    }
}
