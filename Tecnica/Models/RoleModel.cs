namespace Tecnica.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? PermisosId { get; set; }
        public string? Permisos { get; set; }
    }
}
