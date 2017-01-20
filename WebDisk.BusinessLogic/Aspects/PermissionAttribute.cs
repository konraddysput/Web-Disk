using PostSharp.Aspects;
using System;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Interfaces;

namespace WebDisk.BusinessLogic.Aspects
{
    [Serializable]
    public class Permission : MethodInterceptionAspect
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
