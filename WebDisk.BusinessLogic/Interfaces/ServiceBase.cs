using System;
using WebDisk.BusinessLogic.Common;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.BusinessLogic.Interfaces
{
    public class ServiceBase : IDisposable
    {
        protected WebDiskDbContext _context;

        //Authentication Manager
        internal AuthenticationManager _authManager;
        public ServiceBase()
        {
            _context = new WebDiskDbContext();
            _authManager = new AuthenticationManager(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
