using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class ShareInformationExtensions
    {
        public static void Delete(this Repository<FieldShareInformation> source,Guid fieldId)
        {
            var origin = source.Get(n=> n.FieldId == fieldId);
            if (origin == null|| origin.Count()==0)
            {
                throw new ArgumentException($"shared fields with fieldId={fieldId} does not exists");
            }

        }
        public static void Delete(Guid fieldId, Guid ownerId) {
            throw new NotImplementedException();
        }
    }
}
