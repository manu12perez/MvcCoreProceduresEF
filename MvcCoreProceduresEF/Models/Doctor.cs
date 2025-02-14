using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreProceduresEF.Models
{
    [Table("DOCTOR")]
    public class Doctor
    {
        [Key]
        [Column("hospital_cod")]
        public int IdHospital { get; set; }

        [Column("doctor_no")]
        public int DoctorNo { get; set; }

        [Column("apellido")]
        public string Apellido { get; set; }

        [Column("especialidad")]
        public string Especialidad { get; set; }

        [Column("salario")]
        public int Salario { get; set; }
    }
}
