using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Cliente_WPF.Helpers
{
    public static class MetodosGlobales
    {
        /// <summary>
        /// Obtener la descripción de un enumerador
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue">Valor del enumerador</param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("El parámetro debe ser de tipo enumerador", "enumerationValue");
            }

            //Trata de encontrar un DescriptionAttribute para un nombre amigable
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Obtenemos el valor de la descripción
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //Si no tenemos una desscripción regresamos el ToString del enum
            return enumerationValue.ToString();

        }
    }
}
