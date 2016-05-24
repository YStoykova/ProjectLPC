using System;
using Lopoca.Data.Models;
using System.Linq;
using System.Linq.Expressions;

namespace Lopoca.Data.Repository
{
    public interface IFileRepository : IDisposable
    {
        void Add(File file);
        void Delete(Guid fileId);
        IQueryable<File> FindBy(Expression<Func<File, bool>> predicate);
        IQueryable<File> GetAllFiles();
        bool Save();
        void AddHistory(FileHistory history);
        IQueryable<FileHistory> GetFileHistoryBy(Expression<Func<FileHistory, bool>> predicate);
    }
}
