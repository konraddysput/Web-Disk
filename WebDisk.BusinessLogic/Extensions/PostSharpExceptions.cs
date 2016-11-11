using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class PostSharpExceptions
    {
        public static T GetAttributeValue<T>(this MethodInterceptionArgs arguments, string argumentName)
        {
            for (int x = 0; x < arguments.Arguments.Count; x++)
            {
                if (arguments.Method.GetParameters()[x].Name == argumentName)
                {
                    return (T)arguments.Arguments.GetArgument(x);
                }

            }
            throw new ArgumentNullException($"argument: {argumentName} does not exists in scope");
        }
    }
}
