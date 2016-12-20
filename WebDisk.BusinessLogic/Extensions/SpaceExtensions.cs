using System;
using System.Linq;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class SpaceExtensions
    {
        public static Field GetDefaultSpaceDirectory(this Repository<Space> source, Guid userId)
        {
            return source
                    .Get(n =>  n.SpaceId== userId)
                    .FirstOrDefault()
                    ?.Directory;
        }       
    }
}
