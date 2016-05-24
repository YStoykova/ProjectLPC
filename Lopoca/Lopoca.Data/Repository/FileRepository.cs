using Lopoca.Data.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lopoca.Data.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly LopocaDBContext context;

        public FileRepository(LopocaDBContext context)
        {
            this.context = context;
        }
        public virtual IQueryable<File> GetAllFiles()
        {
            IQueryable<File> query = context.Set<File>();
            return query;
        }
        public IQueryable<File> FindBy(Expression<Func<File, bool>> predicate)
        {
            IQueryable<File> query = context.Set<File>().Where(predicate);
            return query;
        }      
        public virtual void Add(File file)
        {
            context.Set<File>().Attach(file);
            context.Set<File>().Add(file);
        }

        public virtual void Delete(Guid fileId)
        {
            File file = context.Set<File>().Where(x => x.Id == fileId).First();
            context.Entry(file).State = System.Data.Entity.EntityState.Modified;
            file.IsDeleted = true;
        }

        public virtual bool Save()
        {
            return context.SaveChanges() > 0;
        }

        public virtual void AddHistory(FileHistory history)
        {
            context.Set<FileHistory>().Attach(history);
            context.Set<FileHistory>().Add(history);
        }

        public IQueryable<FileHistory> GetFileHistoryBy(Expression<Func<FileHistory, bool>> predicate)
        {
            IQueryable<FileHistory> query = context.Set<FileHistory>().Where(predicate);
            return query;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
