using System;

namespace DBConverting
{

    public class DBConvert
    {
        public static object ToDBObject(int? Value)
        {
            return Value == null ? DBNull.Value : (object)Value;
        }

        public static object ToDBObject(int Value)
        {
            return (object)Value;
        }

        public static object ToDBObject(bool? Value)
        {
            return Value == null ? DBNull.Value : (object)Value;
        }

        public static object ToDBObject(bool Value)
        {
            return (object)Value;
        }

        public static object ToDBObject(double? Value)
        {
            return Value == null ? DBNull.Value : (object)Value;
        }

        public static object ToDBObject(double Value)
        {
            return (object)Value;
        }

        public static object ToDBObject(DateTime? Value)
        {
            return Value == null ? DBNull.Value : (object)Value;
        }

        public static object ToDBObject(DateTime Value)
        {
            return (object)Value;
        }

        public static object ToDBObject(string Value)
        {
            return Value == null ? DBNull.Value : (object)Value;
        }

        public static int ToInt(object Value)
        {
            return Value == DBNull.Value ? 0 : (int)Value;
        }

        public static int? ToQInt(object Value)
        {
            return Value == DBNull.Value ? null : (int?)Value;
        }

        public static bool ToBool(object Value)
        {
            return Value == DBNull.Value ? false : (bool)Value;
        }

        public static bool? ToQBool(object Value)
        {
            return Value == DBNull.Value ? null : (bool?)Value;
        }

        public static double ToDouble(object Value)
        {
            return Value == DBNull.Value ? 0 : (double)Value;
        }

        public static double? ToQDouble(object Value)
        {
            return Value == DBNull.Value ? null : (double?)Value;
        }

        public static DateTime ToDateTime(object Value)
        {
            return (DateTime)Value;
        }

        public static DateTime? ToQDateTime(object Value)
        {
            return Value == DBNull.Value ? null : (DateTime?)Value;
        }

        public static string ToString(object Value)
        {
            return Value == DBNull.Value ? null : (string)Value;
        }

    }
}