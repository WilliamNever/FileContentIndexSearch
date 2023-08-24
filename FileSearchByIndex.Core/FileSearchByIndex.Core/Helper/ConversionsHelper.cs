using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileSearchByIndex.Core.Helper
{
    public static class ConversionsHelper
    {
        public static T? DeepCopy<T>(object obj) where T : class
        {
            return DeserializeJson<T>(SerializeToJson(obj), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public static string SerializeToCamelCaseJson(object obj)
        {
            return SerializeToJson(obj,
                new JsonSerializerOptions(JsonSerializerDefaults.General)
                { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                );
        }
        public static string SerializeToJson(object obj, JsonSerializerOptions? jsOption = null)
        {
            if (jsOption == null)
                return JsonSerializer.Serialize(obj);
            else
                return JsonSerializer.Serialize(obj, jsOption);
        }
        public static T? DeserializeJson<T>(string str, JsonSerializerOptions? jsOption = null)
        {
            T? result;
            try
            {
                if (jsOption == null)
                    result = JsonSerializer.Deserialize<T>(str);
                else
                    result = JsonSerializer.Deserialize<T>(str, jsOption);
            }
            catch (Exception)
            {
                result = default(T);
            }
            return result;
        }

        public static T? ConvertToEnum<T>(string str, bool ignoreCases = true) where T : struct
        {
            T? resl = null;
            if (Enum.TryParse(str, ignoreCases, out T re))
            {
                resl = re;
            }
            return resl;
        }

        public static Dictionary<TKey, TValue>? AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value) where TKey : notnull
        {
            if (dic != null)
                if (dic.ContainsKey(key))
                    dic[key] = value;
                else
                    dic.Add(key, value);
            return dic;
        }
        public static void ReflectCopy<T>(T objA, ref T objB)
        {
            if (objA == null || objB == null) return;

            var clType = typeof(T);
            var aPropties = objA.GetType().GetProperties();
            foreach (var prop in aPropties)
            {
                clType.GetProperty(prop.Name)?.SetValue(objB, prop.GetValue(objA));
            }
        }
        public static string? GetPropertyStringValue(string? json, string? propertyName)
        {
            if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(propertyName)) return null;

            var nJsn = Newtonsoft.Json.Linq.JToken.Parse(json);
            var retValue = nJsn.Value<string>(propertyName);

            return retValue;
        }
    }
}
