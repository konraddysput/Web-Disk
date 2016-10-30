using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.ViewModels;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Extensions
{
    public static class SpaceExtensions
    {
        public static IEnumerable<SpaceBusinessLogicViewModel> ConvertSpace(this IEnumerable<Space> source)
        {
            return source.Select(n => new SpaceBusinessLogicViewModel()
                        {
                            Id = n.SpaceId,
                            Name = n.Name,
                            Type = ViewModels.Types.SpaceType.UserSpace
                        });
        }

        public static IEnumerable<SpaceBusinessLogicViewModel> ConvertSpace(this IEnumerable<SpaceShare> source)
        {
            return source.Select(n => new SpaceBusinessLogicViewModel()
                        {
                            Id = n.ShareId,
                            Name = n.Space.Name,
                            Type = ViewModels.Types.SpaceType.SharedSpace
                        });
        }

        //public static IEnumerable<Field>
    }
}
