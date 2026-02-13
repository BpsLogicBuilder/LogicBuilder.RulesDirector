using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace LogicBuilder.RulesDirector
{
    public static class TypeHelpers
    {
        internal static bool TryParse(this string toParse, Type type, out object result)
        {
            if (type == null)
                throw new ArgumentException("Argument cannot be null.", nameof(type));

            if (!IsLiteralType(type))
                throw new ArgumentException("Not a valid literal type.", nameof(type));

            if (type == typeof(string))
            {
                result = toParse;
                return true;
            }

            if (typeof(Enum).IsAssignableFrom(type))
            {
                if (!int.TryParse(toParse, out int _) && !Enum.IsDefined(type, toParse))
                {
                    result = null;
                    return false;
                }

                result = Enum.Parse(type, toParse);
                return true;
            }

            if (type.IsNullable())
                type = Nullable.GetUnderlyingType(type);

            MethodInfo method = type.GetMethods().SingleOrDefault(IsTryParseMethod);

            if (method == null)
            {
                result = null;
                return false;
            }

            object[] args = new object[] { toParse, null };
            bool success = (bool)method.Invoke(null, args);
            result = success ? args[1] : null;

            return success;

            bool IsTryParseMethod(MethodInfo method)
            {
                if (method.Name != "TryParse") return false;
                ParameterInfo[] parameters = method.GetParameters();
                return parameters.Length == 2
                    && parameters[0].ParameterType == typeof(string)
                    && parameters[1].IsOut
                    && parameters[1].ParameterType.GetElementType() == type;
            }
        }

        [ExcludeFromCodeCoverage]
        [Obsolete("This method is obsolete. It was used to support the standard forms feature which has been removed from LogicBuilder")]
        internal static bool CanBeAssignedNull(this Type type) 
            => !type.IsValueType || type.IsNullable();

        [Obsolete("This method is obsolete. It was used to support the standard forms feature which has been removed from LogicBuilder")]
        private static Dictionary<Type, HashSet<Type>> NumbersDictionary = new Dictionary<Type, HashSet<Type>>()
        {
            { typeof(decimal), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong) } },
            { typeof(double), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float) } },
            { typeof(float), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong) } },
            { typeof(ulong), new HashSet<Type> { typeof(byte), typeof(char), typeof(ushort), typeof(uint) } },
            { typeof(long), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort), typeof(int), typeof(uint) } },
            { typeof(uint), new HashSet<Type> { typeof(byte), typeof(char), typeof(ushort) } },
            { typeof(int), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort) } },
            { typeof(ushort), new HashSet<Type> { typeof(byte), typeof(char) } },
            { typeof(short), new HashSet<Type> { typeof(byte), typeof(sbyte) } }
        };

        internal static bool IsNullable(this Type type) 
            => type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));

        [ExcludeFromCodeCoverage]
        [Obsolete("This method is obsolete. It was used to support the standard forms feature which has been removed from LogicBuilder")]
        public static bool AssignableFrom(this Type to, Type from)
        {
            if (to.IsAssignableFrom(from))
                return true;

            if (!(!to.IsNullable() && from.IsNullable()))
            {//Anything but To is NOT nullable and From IS nullable
                to = to.IsNullable() ? Nullable.GetUnderlyingType(to) : to;
                from = from.IsNullable() ? Nullable.GetUnderlyingType(from) : from;

                if (NumbersDictionary.ContainsKey(to) && NumbersDictionary[to].Contains(from))
                    return true;
            }

            bool ReturnTypeValid(Type returnType) => returnType == to || (NumbersDictionary.ContainsKey(to) && NumbersDictionary[to].Contains(returnType));
            bool ParameterValid(Type parameterType) => (parameterType == from) || (NumbersDictionary.ContainsKey(parameterType) && NumbersDictionary[parameterType].Contains(from));
            bool MatchImplicitOperator(MethodInfo m) => m.Name == "op_Implicit"
                                                        && ReturnTypeValid(m.ReturnType)
                                                        && ParameterValid(m.GetParameters().Single().ParameterType);

            return from.GetMethods(BindingFlags.Public | BindingFlags.Static).Any(MatchImplicitOperator)
                    || to.GetMethods(BindingFlags.Public | BindingFlags.Static).Any(MatchImplicitOperator);
        }

        private static bool IsLiteralType(Type type)
        {
            if (type.IsNullable())
                type = Nullable.GetUnderlyingType(type);

            return LiteralTypes.Contains(type)
                || UneferencedLiteralTypes.Contains(type.FullName)
                || typeof(Enum).IsAssignableFrom(type);
        }

        private static HashSet<Type> LiteralTypes => [.. _literalTypes];

        private static readonly HashSet<string> UneferencedLiteralTypes =
        [
            UnreferencedLiteralTypeNames.DATEONLY,
            UnreferencedLiteralTypeNames.TIMEONLY,
            UnreferencedLiteralTypeNames.DATE,
            UnreferencedLiteralTypeNames.TIMEOFDAY
        ];

        private struct UnreferencedLiteralTypeNames
        {
            public const string DATEONLY = "System.DateOnly";
            public const string TIMEONLY = "System.TimeOnly";
            public const string DATE = "Microsoft.OData.Edm.Date";
            public const string TIMEOFDAY = "Microsoft.OData.Edm.TimeOfDay";
        }

        private static Type[] _literalTypes => [
                typeof(bool),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid),
                typeof(decimal),
                typeof(byte),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(float),
                typeof(double),
                typeof(char),
                typeof(sbyte),
                typeof(ushort),
                typeof(uint),
                typeof(ulong),
                typeof(string)
            ];
    }
}
