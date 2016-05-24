using System;
using System.Collections.Generic;
using Lopoca.Data.Models;

namespace Lopoca.Core
{
    public interface IFileManager
    {
        void Upload(File file);
        void AddHistory(Guid fileId, string userId);
        void Delete(Guid fileId, string userId);
        IEnumerable<File> FindBy(string userId);
        File FindFileBy(string fileId);
        IEnumerable<File> GetAllFiles();
        IEnumerable<FileHistory> GetFileHistoryBy(Guid fileId);
    }
}
