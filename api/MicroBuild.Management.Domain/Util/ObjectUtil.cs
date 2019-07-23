using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Util
{
    public class ObjectUtil
    {
        public static object GetValue(object objectWithField, string field)
        {
            string[] propertyComponents = field.Split('.');
            object value = objectWithField;

            foreach (var propertyComponent in propertyComponents)
            {
                if (value == null)
                    return null;

                value = value.GetType().GetProperty(propertyComponent)?.GetValue(value);
            }

            return value;
        }

        public static void SetValue(object target, string propertyName, object value)
        {
            string[] propertyNameParts = propertyName.Split('.');
            int length = propertyNameParts.Length;

            if (length > 1)
            {
                for (int i = 0; i < length - 1; i++)
                {
                    target = target.GetType().GetProperty(propertyNameParts[i]).GetValue(target);
                }
            }

            target.GetType().GetProperty(propertyNameParts[length - 1]).SetValue(target, value);
        }
    }
}
