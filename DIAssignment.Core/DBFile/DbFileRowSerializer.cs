using System;
using System.Linq;
using System.Reflection;

namespace DIAssignment.Core.DBFile
{
    /// <summary>
    /// Serializes a DbFileRow into an entity
    /// </summary>
    public static class DbFileRowSerializer
    {
        /// <summary>
        /// Serialize row into type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T Serialize<T>(DbFileRow row) where T : new()
        {
            var value = new T();

            foreach (var p in typeof(T).GetProperties())
                if (GetAttribute(p) is DbFileColumnAttribute att)
                    p.SetValue(value, GetValue(row.GetValue(att.Name), p));

            return value;
        }

        /// <summary>
        /// Find <see cref="DbFileColumnAttribute"/>
        /// </summary>
        private static DbFileColumnAttribute? GetAttribute(PropertyInfo prop)
            => prop.GetCustomAttributes(typeof(DbFileColumnAttribute))
                .FirstOrDefault() as DbFileColumnAttribute;

        /// <summary>
        /// Convert value to Property type
        /// </summary>
        private static object? GetValue(string value, PropertyInfo prop)
        {
            try
            {
                if (prop.PropertyType == typeof(bool))
                    return Convert.ChangeType(Convert.ToInt32(value), prop.PropertyType);

                return Convert.ChangeType(value, prop.PropertyType);
            } 
            catch
            {
                return prop.PropertyType.IsValueType ?
                    Activator.CreateInstance(prop.PropertyType)
                    : null;
            }
        }
    }
}
