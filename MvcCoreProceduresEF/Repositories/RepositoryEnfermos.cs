using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;

#region Procedures

//create procedure SP_TODOS_ENFERMOS 
//as 
//	select * from ENFERMO
//go

//create procedure SP_FIND_ENFERMO (@inscripcion nvarchar(50))
//as
//	select * from ENFERMO where INSCRIPCION=@inscripcion
//go

//create procedure SP_DELETE_ENFERMO (@inscripcion nvarchar(50))
//as
//	delete from ENFERMO where INSCRIPCION=@inscripcion
//go

//create procedure SP_INSERT_ENFERMO
//(@apellido nvarchar(50), @direccion nvarchar(50), @fechanac datetime, @genero nvarchar(50))
//as
//	declare @id nvarchar(50)
//	set @id = (select MAX(inscripcion + 1) from ENFERMO)
//	insert into ENFERMO values (@id, @apellido, @direccion, @fechanac, @genero,'123456789')
//go
#endregion

namespace MvcCoreProceduresEF.Repositories
{
    public class RepositoryEnfermos
    {
        private EnfermosContext context;

        public RepositoryEnfermos (EnfermosContext context)
        {
            this.context = context;
        }

        public async Task<List<Enfermo>> GetEnfermosAsync()
        {
            using (DbCommand com = this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_TODOS_ENFERMOS";

                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;

                await com.Connection.OpenAsync();
                DbDataReader reader = await com.ExecuteReaderAsync();

                List<Enfermo> enfermos = new List<Enfermo>();

                while ( await reader.ReadAsync())
                {
                    Enfermo enfermo = new Enfermo
                    {
                        Inscripcion = reader["inscripcion"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        FechaNacimiento = DateTime.Parse(reader["fecha_nac"].ToString()),
                        Genero = reader["s"].ToString()
                    };
                    enfermos.Add(enfermo);
                }

                await reader.CloseAsync();
                await com.Connection.CloseAsync();
                return enfermos;
            }
        }

        public async Task<Enfermo> FindEnfermoByInscripcionAsync(string inscripcion)
        {
            string sql = "SP_FIND_ENFERMO @INSCRIPCION";
            SqlParameter paramInscripcion = new SqlParameter("@INSCRIPCION", inscripcion);

            var consulta = await this.context.Enfermos.FromSqlRaw(sql, paramInscripcion).ToListAsync();

            Enfermo enfermo = consulta.FirstOrDefault();

            return enfermo;
        }

        public async Task DeleteEnfermo(string inscripcion)
        {
            string sql = "SP_DELETE_ENFERMO";
            SqlParameter paramInscripcion = new SqlParameter("@INSCRIPCION", inscripcion);

            using (DbCommand com = this.context.Database.GetDbConnection().CreateCommand())
            {
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;

                com.Parameters.Add(paramInscripcion);

                await com.Connection.OpenAsync();

                com.ExecuteNonQuery();

                await com.Connection.CloseAsync();

                com.Parameters.Clear();
            }
        }

        public async Task DeleteEnfermoAsync(string inscripcion)
        {
            string sql = "SP_DELETE_ENFERMO @INSCRIPCION";
            SqlParameter paramInscripcion = new SqlParameter("@INSCRIPCION", inscripcion);

            await this.context.Database.ExecuteSqlRawAsync(sql, paramInscripcion);
        }

        public async Task InsertEnfermoAsync(string apellido, string direccion, DateTime fechaNac, string genero)
        {
            string sql = "SP_INSERT_ENFERMO @apellido, @direccion, @fechanac, @genero";
            SqlParameter paramApellido = new SqlParameter("@apellido", apellido);
            SqlParameter paramDireccion = new SqlParameter("@direccion", direccion);
            SqlParameter paramFechanac = new SqlParameter("@fechanac", fechaNac);
            SqlParameter paramGenero = new SqlParameter("@genero", genero);

            await this.context.Database.ExecuteSqlRawAsync(sql, paramApellido, paramDireccion, paramFechanac, paramGenero);
        }
    }
}
