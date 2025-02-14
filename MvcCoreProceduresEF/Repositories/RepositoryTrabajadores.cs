﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;

namespace MvcCoreProceduresEF.Repositories
{
    public class RepositoryTrabajadores
    {
        private HospitalContext context;

        public RepositoryTrabajadores(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<TrabajadoresModel> GetTrabajadoresAsync()
        {
            var consulta = from datos in this.context.Trabajadores
                           select datos;

            TrabajadoresModel model = new TrabajadoresModel();
            model.Trabajadores = await consulta.ToListAsync();
            model.Personas = await consulta.CountAsync();
            model.SumaSalarial = await consulta.SumAsync(z => z.Salario);
            model.MediaSalarial = (int) await consulta.AverageAsync(x => x.Salario);

            return model;
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            var consulta = (from datos in this.context.Trabajadores
                            select datos.Oficio).Distinct();

            return await consulta.ToListAsync();
        }

        public async Task<TrabajadoresModel> GetTrabajadoresModelOficioAsync(string oficio)
        {
            //VAMOS A LLAMAR AL PROCEDIMIENTO CON EF
            //LA UNICA DIFERENCIA RADICA EN QUE TENGO QUE PONER LA PALABRA out EN CADA PARAMETRO DE SALIDA EN LA CONSULTA SQL
            string sql = "SP_WORKERS_OFICIO @oficio, @personas OUT, @media OUT, @suma OUT";

            SqlParameter pamOficio = new SqlParameter("@oficio", oficio);
            SqlParameter pamPersonas = new SqlParameter("@personas", -1);
            pamPersonas.Direction = System.Data.ParameterDirection.Output;
            SqlParameter pamMedia = new SqlParameter("@media", -1);
            pamMedia.Direction = System.Data.ParameterDirection.Output;
            SqlParameter pamSuma = new SqlParameter("@suma", -1);
            pamSuma.Direction = System.Data.ParameterDirection.Output;

            //EJECUTAMOS LA CONSULTA DE SELECCION
            var consulta = this.context.Trabajadores.FromSqlRaw(sql, pamOficio, pamPersonas, pamMedia, pamSuma);

            TrabajadoresModel model = new TrabajadoresModel();

            //HASTA QUE NO EXTRAEMOS LOS DATOS DEL SELCT NO TENEMOS LOS PARAMETROS DE SALIDA (reader.Close())
            model.Trabajadores = await consulta.ToListAsync();
            model.Personas = int.Parse(pamPersonas.Value.ToString());
            model.MediaSalarial = int.Parse(pamMedia.Value.ToString());
            model.SumaSalarial = int.Parse(pamSuma.Value.ToString());

            return model;
        }
    }
}
