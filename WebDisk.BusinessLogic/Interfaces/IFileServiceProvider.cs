using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Interfaces
{
    public interface IFileServiceProvider
    {
        IEnumerable<File> GetUserDictionaryFiles(Guid dictionaryId);
    }
}
