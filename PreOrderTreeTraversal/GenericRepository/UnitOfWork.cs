using System;
using System.Data.Entity;

namespace PreOrderTreeTraversal.GenericRepository
{
    public class UnitOfWork : IDisposable
    {
        protected DbContext context { get; set; }

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }

        public bool IsDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}