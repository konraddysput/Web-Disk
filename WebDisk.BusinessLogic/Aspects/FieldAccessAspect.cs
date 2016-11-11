using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDisk.BusinessLogic.Aspects
{
    [Serializable]
    public class FieldAccessAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            base.OnInvoke(args);
        }
    }
}
