
#region procedures
//create procedure SP_GET_ESPECIALIDADES
//as
//	select distinct(especialidad) from DOCTOR
//go

//create procedure SP_INCREMENTO_SALARIAL_ESPECIALIDAD
//(@especialidad nvarchar(50), @incremento int)
//as
//	update doctor set SALARIO=SALARIO + @incremento where ESPECIALIDAD=@especialidad

//	select * from DOCTOR where ESPECIALIDAD=@especialidad
//go
#endregion

using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;
using Microsoft.Data.SqlClient;

namespace MvcCoreProceduresEF.Repositories
{
    public class RepositoryDoctores
    {
        private EnfermosContext context;

        public RepositoryDoctores(EnfermosContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> GetEspecialidadesAsync()
        {
            using (DbCommand com = this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_GET_ESPECIALIDADES";

                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;

                await com.Connection.OpenAsync();
                DbDataReader reader = await com.ExecuteReaderAsync();

                List<string> especialidades = new List<string>();

                while (await reader.ReadAsync())
                {
                    especialidades.Add(reader["especialidad"].ToString());
                }

                await reader.CloseAsync();
                await com.Connection.CloseAsync();
                return especialidades;
            }
        }

        public async Task<List<Doctor>> AumentarSalarioAsync(string especialidad, int incremento)
        {
            string sql = "SP_INCREMENTO_SALARIAL_ESPECIALIDAD @especialidad, @incremento";
            SqlParameter paramEspecialidad = new SqlParameter("@especialidad", especialidad);
            SqlParameter paramIncremento = new SqlParameter("@incremento", incremento);

            var consulta = await this.context.Doctores.FromSqlRaw(sql, paramEspecialidad, paramIncremento).ToListAsync();

            return consulta;
        }
    }
}
