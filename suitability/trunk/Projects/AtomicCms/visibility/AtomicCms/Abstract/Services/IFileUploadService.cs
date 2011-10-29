namespace AtomicCms.Common.Abstract.Models
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFileUploadService
    {
        ICollection<FileInfo> GetFiles(string uploadsFolder);
    }
}