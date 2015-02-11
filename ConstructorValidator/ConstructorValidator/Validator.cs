using System;
using System.Reflection;

namespace ConstructorValidator
{
    public class Validator
    {
        const BindingFlags BindFlags =
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

        public static void ValidateNotNullConstructorFields(object instance)
        {
            var type = instance.GetType();

            if (type.GetConstructors().Length > 1)
            {
                throw new InvalidOperationException("Methods with more than one constructor can not be validated.");
            }

            var ctor = type.GetConstructors()[0];
            var ctorParams = ctor.GetParameters();

            foreach (var ctorParam in ctorParams)
            {
                var field = type.GetField(ctorParam.Name, BindFlags);

                if (field != null)
                {
                    var val = field.GetValue(instance);
                    if (val == null)
                    {
                        throw new ArgumentNullException(ctorParam.Name);
                    }
                }
                else
                {
                    var errorMsg =
                        string.Format(
                            "Cannot validate constructor '{0}', because field corresponding to paramater '{1}' was not found.",
                            ctor.ReflectedType.FullName,
                            ctorParam.Name);
                    throw new ArgumentException(errorMsg);
                }
            }
        }

        public static void ValidateNotNullFields(object instance, params string[] args)
        {
            var type = instance.GetType();

            foreach (var arg in args)
            {
                var field = type.GetField(arg, BindFlags);
                if (field != null)
                {
                    var value = field.GetValue(instance);
                    if (value == null)
                    {
                        throw new ArgumentNullException(arg);
                    }
                }
                else
                {
                    var errorMsg = string.Format(
                        "Cannot find field '{0}', in '{1}'",
                        arg, type.FullName);
                    throw new ArgumentException(errorMsg);
                }
            }
        }
    }
}
