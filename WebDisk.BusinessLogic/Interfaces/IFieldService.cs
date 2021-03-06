﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.ViewModels;

namespace WebDisk.BusinessLogic.Interfaces
{
    public interface IFieldService
    {
        void CreateField(Guid userId, Guid fieldId, IEnumerable<FileViewModel> fileViewModels);
        void CreateField(Guid userId, Guid fieldId, FileViewModel fileViewModel);
        void Delete(Guid userId, Guid fieldId);

        void Copy(Guid userId, Guid destinationId, Guid fieldId);
        void Cut(Guid userId, Guid destinationId, Guid fieldId);

        void Update(Guid userId, Guid fieldId, string newFieldName);
    }
}
