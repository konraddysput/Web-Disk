using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class SpaceExtensions
    {
        public static Directory GetDefaultSpaceDirectory(this Repository<Space> source, Guid userId)
        {
            return source
                    .Get(n =>  n.SpaceId== userId)
                    .FirstOrDefault()
                    ?.Directory;
        }
    }
}
