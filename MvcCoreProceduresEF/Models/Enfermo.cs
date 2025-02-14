using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreProceduresEF.Models
{
    [Table("ENFERMO")]
    public class Enfermo
    {
        [Key]
        [Column("INSCRIPCION")]
        public string Inscripcion { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("Direccion")]
        public string Direccion { get; set; }

        [Column("Fecha_nac")]
        public DateTime FechaNacimiento { get; set; }

        [Column("S")]
        public string Genero { get; set; }
    }
}
