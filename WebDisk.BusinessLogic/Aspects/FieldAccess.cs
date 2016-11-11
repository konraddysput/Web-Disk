using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Interfaces;

namespace WebDisk.BusinessLogic.Aspects
{
    [Serializable]
    public class FieldAccess : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            Guid userId = args.GetAttributeValue<Guid>("userId");
            Guid fieldId = args.GetAttributeValue<Guid>("fieldId");
            if (!((ServiceBase)args.Instance)._authManager.IsUserHasRights(userId,fieldId))
            {
                throw new UnauthorizedAccessException($"user with id {userId} does not have access to field");
            }
            base.OnInvoke(args);
        }

        
    }
}
