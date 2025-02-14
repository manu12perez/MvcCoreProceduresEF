using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#region VISTA
/*

create view V_EMPLEADOS_DEPARTAMENTOS
as
	select CAST(
	ISNULL(ROW_NUMBER() over (order by APELLIDO), 0) as int) 
	as ID
	, EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO
	, DEPT.DNOMBRE as DEPARTAMENTO
	, DEPT.LOC as LOCALIDAD
	from EMP
	inner join DEPT
	on EMP.DEPT_NO = DEPT.DEPT_NO
go

*/
#endregion

namespace MvcCoreProceduresEF.Models
{
    [Table("V_EMPLEADOS_DEPARTAMENTOS")]
    public class VistaEmpleado
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("OFICIO")]
        public string Oficio { get; set; }

        [Column("SALARIO")]
        public int Salario { get; set; }

        [Column("DEPARTAMENTO")]
        public string Departamento { get; set; }

        [Column("LOCALIDAD")]
        public string Localidad { get; set; }
    }
}
