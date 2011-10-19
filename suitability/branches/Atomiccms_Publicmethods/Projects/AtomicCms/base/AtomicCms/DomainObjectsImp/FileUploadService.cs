namespace AtomicCms.Core.DomainObjectsImp
{
    using System.Collections.Generic;
    using System.IO;
    using Common.Abstract.Models;

    public class FileUploadService : IFileUploadService
    {
        #region Implementation of IFileUploadService

        public ICollection<FileInfo> GetFiles(string uploadsFolder)
        {
            if (Directory.Exists(uploadsFolder))
            {
                DirectoryInfo info = new DirectoryInfo(uploadsFolder);
                return info.GetFiles();
            }

            return new List<FileInfo>();
        }

        #endregion
    }
}