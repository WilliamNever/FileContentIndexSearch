using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FileSearchByIndex.Core.Helper
{
    public static class ValidationHelper
    {
        public static List<ValidationResult> ReflectValidateProperties<T>(this T? inm) where T : class
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (inm == null || inm.GetType().Equals(typeof(string)) || inm.GetType().IsValueType)
                return results;

            var Properties = inm.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(x => x.PropertyType.IsClass && !typeof(string).Equals(x.PropertyType)).ToList()
                ;
            foreach (var prop in Properties)
            {
                var childObject = prop.GetValue(inm);
                if (childObject is IList childObjects)
                    foreach (var obj in childObjects)
                        results.AddRange(ReflectValidateProperties(obj));
                else
                    results.AddRange(ReflectValidateProperties(childObject));
            }
            ValidationContext context = new ValidationContext(inm);
            bool isValid = Validator.TryValidateObject(inm, context, results, true);
            return results;
        }
        public static List<ValidationResult> SimpleValidateProperties<T>(this T inm) where T : class
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(inm);
            bool isValid = Validator.TryValidateObject(inm, context, results, true);
            return results;
        }
    }
}
