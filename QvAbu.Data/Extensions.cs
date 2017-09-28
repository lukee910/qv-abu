using Microsoft.Extensions.Configuration;
using System;

namespace QvAbu.Data
{
    public static class Extensions
    {
        public static string CombineRevision(this Guid id, int revision)
        {
            return id.ToString() + revision;
        }

        public static string GetConnectionString(this IConfigurationRoot config)
        {
            return config["SQLAZURECONNSTR_defaultConnection"] // Try Azure Conn String
                ?? config["QvAbuConnection"]
                ?? "Data Source=.;Initial Catalog=QvAbu;Integrated Security=True;";
        }
    }
}
