using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebDisk.Database.DatabaseModel.Identity
{
    public class AppClaimsPrincipal : ClaimsPrincipal
    {
        public AppClaimsPrincipal(ClaimsPrincipal principal) : base(principal)
        { }

        public int UserId
        {
            get { return int.Parse(this.FindFirst(ClaimTypes.Sid).Value); }
        }
    }
}
