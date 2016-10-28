using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Services
{
    public class DirectoryService : IDisposable
    {
        private WebDiskDbContext _context = new WebDiskDbContext();
        private Repository<Directory> _directoryRepository;
        private Repository<File> _fileRepository;

        public DirectoryService()
        {
            _directoryRepository = new Repository<Directory>(_context);
            _fileRepository = new Repository<File>(_context);
        }


        //public IEnumerable<SpaceDataBase> GetFields(Guid? directoryId)
        //{
            
        //}

        //public Repository<Directory> DirectoryRepository
        //{
        //    get
        //    {

        //        if (_directoryRepository == null)
        //        {
        //            _directoryRepository = new Repository<Directory>(_context);
        //        }
        //        return _directoryRepository;
        //    }
        //}

        //public Repository<File> CourseRepository
        //{
        //    get
        //    {

        //        if (_fileRepository == null)
        //        {
        //            _fileRepository = new Repository<File>(_context);
        //        }
        //        return _fileRepository;
        //    }
        //}

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
