using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CustomControls
{
    public static class ExtendedMethods
    {
        public static object getValueProperty(this object obj, string nombrePropiedad)
        {
            if (obj != null)
            {
                return obj.GetType().GetProperty(nombrePropiedad).GetValue(obj, null);
            }
            else
            {
                return null;
            }
        }

        public static T getValueProperty<T>(this object obj, string nombrePropiedad)
        {
            if (obj != null)
            {
                return (T)obj.GetType().GetProperty(nombrePropiedad).GetValue(obj, null);
            }
            else
            {
                return default(T);
            }
        }

        public static void setValueProperty(this object obj, string nombreDePropiedad, object valor)
        {
            obj.GetType().GetProperty(nombreDePropiedad).SetValue(obj, valor, null);
        }

        public static void setValueProperty<T>(this object obj, string nombreDePropiedad, T valor)
        {
            obj.GetType().GetProperty(nombreDePropiedad).SetValue(obj, valor, null);
        }

        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }
    }
}
