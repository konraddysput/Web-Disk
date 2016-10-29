﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.BusinessLogic.ViewModels;

namespace WebDisk.BusinessLogic.Interfaces
{
    public interface ISpaceService : IDisposable
    {
        IEnumerable<SpaceBusinessLogicViewModel> GetSpaces(Guid userId);
        void Create(Guid usedId);
        void ShareSpace(Guid spaceId, Guid userId);
        void Delete(Guid spaceId);
    }
}