using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;

#region VISTAS Y PROCEDIMIENTOS ALMACENADOS
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

create view V_WORKERS
as
	select EMP_NO as IDWORKER
	, APELLIDO, OFICIO, SALARIO from EMP
	union 
	select DOCTOR_NO, APELLIDO, ESPECIALIDAD, SALARIO
	from DOCTOR
	union
	select EMPLEADO_NO, APELLIDO, FUNCION, SALARIO
	from PLANTILLA
go

create procedure SP_WORKERS_OFICIO
(@oficio nvarchar(50)
, @personas int out
, @media int out
, @suma int out)
as
	select * from V_WORKERS
	where OFICIO = @oficio
	select @personas = COUNT(IDWORKER)
	, @media = AVG(SALARIO), @suma = SUM(SALARIO)
	from V_WORKERS where OFICIO = @oficio
go

*/
#endregion

namespace MvcCoreProceduresEF.Repositories
{
    public class RepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<VistaEmpleado>> GetVistaEmpleadosAsync()
        {
            var consulta = from datos in this.context.VistasEmpleados
                           select datos;

            return await consulta.ToListAsync();
        }
    }
}
