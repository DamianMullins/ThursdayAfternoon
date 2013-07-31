using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Utilities.Core.Text;

namespace ThursdayAfternoon.Infrastructure.Data
{
    public static class DbContextExtensions
    {
        public static void AddConfigurations(this DbModelBuilder modelBuilder)
        {
            // Dynamically load all configurations
            IEnumerable<Type> typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                                        .Where(type => type.Namespace.IsNotEmpty() &&
                                                                       type.BaseType != null &&
                                                                       type.BaseType.IsGenericType &&
                                                                       type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var configurationInstance in typesToRegister.Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configurationInstance);
            }
        }
    }
}
