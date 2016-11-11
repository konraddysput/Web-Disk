using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.Extensions;
using WebDisk.BusinessLogic.Services;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Common
{
    public class AuthenticationManager
    {
        private Repository<ApplicationUser> _userRepository;
        private Repository<Field> _fieldRepository;
        public AuthenticationManager(WebDiskDbContext context)
        {
            _userRepository = new Repository<ApplicationUser>(context);
            _fieldRepository = new Repository<Field>(context);
        }
        public AuthenticationManager(Repository<ApplicationUser> userRepository, Repository<Field> fieldRepository)
        {
            _userRepository = userRepository;
            _fieldRepository = fieldRepository;
        }

        public bool IsUserHasRights(Guid userId, Guid fieldId)
        {
            var rootFieldId = _fieldRepository.GetFieldRoot(fieldId).FieldId;
            return _userRepository.IsSharedOrUserRootDirectory(userId, rootFieldId);
        }
    }
}
