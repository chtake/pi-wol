using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace PiWol.DataStorage.Core
{
    public abstract class DataStorageSetupBase
    {
        /// <summary>
        ///     Gets the data directory where all data is stored.
        /// </summary>
        protected const string DataDirectory = "data";

        protected readonly IFileProvider FileProvider;

        protected DataStorageSetupBase(IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
        }

        protected void CreateDataDirectoryIfNotExists()
        {
            var directory = FileProvider.GetDirectoryContents(DataDirectory);
            if (directory.Exists == false)
            {
                var di = new DirectoryInfo(FileProvider.GetFileInfo(DataDirectory).PhysicalPath);
                di.Create();
            }
        }
        
        protected void CreateFilesIfNotExists(IEnumerable<string> repositoryFiles)
        {
            var directory = FileProvider.GetDirectoryContents(DataDirectory).ToList();
            foreach (var file in repositoryFiles)
            {
                var exists = directory.Any(x => string.Equals(x.Name, file, StringComparison.InvariantCulture));
                if (exists == false)
                {
                    var fi = new FileInfo(FileProvider.GetFileInfo(Path.Combine(DataDirectory, file)).PhysicalPath);
                    using (var fs = fi.Create())
                    {
                        fs.Write(new UTF8Encoding(true).GetBytes("[]"));
                    }
                }
            }
        }

        /// <summary>
        ///     Starts the creation of data files and folders.
        /// </summary>
        public abstract void CreateOrMigrateDataStorage();
    }
}