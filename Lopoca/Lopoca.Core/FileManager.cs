using System;
using System.Collections.Generic;
using Lopoca.Data;
using Lopoca.Data.Models;
using Lopoca.Data.Repository;
using System.Linq;

namespace Lopoca.Core
{
    public class FileManager : IFileManager
    {
        protected readonly IFileRepository fileRepository;

        public FileManager(IFileRepository repository)
        {
            fileRepository = repository;
        }

        /// <summary>
        /// Save a file and file history when the user upload a file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public void Upload(File file)
        {
            fileRepository.Add(file);

            SaveHistory(file.Id, file.UserId, StatusTypes.Upload);

            fileRepository.Save();
        }     

        /// <summary>
        /// Change a file status type to deleted
        /// </summary>
        /// <param name="file"></param>
        public void Delete(Guid fileId, string userId)
        {
            fileRepository.Delete(fileId);
            SaveHistory(fileId, userId, StatusTypes.Delete);
            fileRepository.Save();
        }

        /// <summary>
        /// Return all files by admin role user.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<File> GetAllFiles()
        {
            return fileRepository.GetAllFiles().AsEnumerable();
        }

        /// <summary>
        /// Return all files by user Id.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<File> FindBy(string userId)
        {
            IEnumerable<File> query = fileRepository.FindBy(x => x.UserId == userId && !x.IsDeleted);
            return query;
        }

        /// <summary>
        /// Return file by Id.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public File FindFileBy(string fileId)
        {
            File file = fileRepository.FindBy(x => x.Id == new Guid(fileId)).FirstOrDefault();
            return file;
        }

        /// <summary>
        /// Add a record to history table when the user open a file
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="userId"></param>
        public void AddHistory(Guid fileId, string userId)
        {
            SaveHistory(fileId, userId, StatusTypes.Open);  
            fileRepository.Save();
        }

        public IEnumerable<FileHistory> GetFileHistoryBy(Guid fileId)
        {
            IEnumerable<FileHistory> query = fileRepository.GetFileHistoryBy(x => x.FileId == fileId);
            return query;
        }

        private void SaveHistory(Guid fileId, string userId, StatusTypes type)
        {
            FileHistory history = new FileHistory();
            history.FileId = fileId;
            history.ActionTime = DateTime.Now;
            history.UserId = userId;
            history.StatusTypeId = (byte)type;
            fileRepository.AddHistory(history);
        }
    }
}
