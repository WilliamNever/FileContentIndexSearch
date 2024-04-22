using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FileSearchByIndex.Core.Helper
{
    public static class ValidationHelper
    {
        public static List<ValidationResult> ReflectValidateProperties<T>(this T? inm) where T : class
        {
            List<ValidationResult> results = new List<ValidationResult>();
            var type = typeof(T);
            if (inm == null || type.Equals(typeof(string)) || type.IsValueType)
                return results;

            var Properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(x => x.PropertyType.IsClass && !typeof(string).Equals(x.PropertyType)).ToList()
                ;
            foreach (var prop in Properties)
            {
                var childObject = prop.GetValue(inm);
                /*
                 * if childObject is null, (childObject is IList childObjects) is false.
                 * do not need to make sure if the convert to objects is null.
                 * */
                if (childObject is IList childObjects)
                    foreach (var obj in childObjects)
                        results.AddRange(ReflectValidateProperties(obj));
                else if (childObject is IEnumerable IEChildren)
                {
                    /*
                     * for the IEnumerable<T>, it is not IList in some situation.
                     */
                    var ietor = IEChildren.GetEnumerator();
                    while (ietor.MoveNext())
                        results.AddRange(ReflectValidateProperties(ietor.Current));
                }
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
