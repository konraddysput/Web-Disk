using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Interfaces;

namespace WebDisk.BusinessLogic.Aspects
{
    [Serializable]
    public class AfterDataChange : OnMethodBoundaryAspect
    {
        public override void OnExit(MethodExecutionArgs args)
        {
            ((ServiceBase)args.Instance).Save();
            base.OnExit(args);
        }
    }
}
