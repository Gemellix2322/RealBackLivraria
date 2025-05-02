using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_teste.Model
{
    [Table("users")]
    public class UsuarioModel
    {
        public int id { get; set; }

        public string? name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string? profile_picture { get; set; }
        public int admin { get; set; }
        public UsuarioModel() { }

    }
}
