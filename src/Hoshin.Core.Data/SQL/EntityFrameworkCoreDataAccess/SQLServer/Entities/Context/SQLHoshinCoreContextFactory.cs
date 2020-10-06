using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context
{
    /// <summary>
    /// No se usa actualmente, pero probablemente sea necesaria cuando tengamos múltiples contextos
    /// </summary>
    public class SQLHoshinCoreContextFactory : IDesignTimeDbContextFactory<SQLHoshinCoreContext>
    {
        SQLHoshinCoreContext IDesignTimeDbContextFactory<SQLHoshinCoreContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<SQLHoshinCoreContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new SQLHoshinCoreContext(builder.Options);
        }
    }
}
