using Microsoft.Extensions.FileProviders;
using PiWol.DataStorage.Core;

namespace PiWol.WakeOnLanService.Data.Setup.Services
{
    /// <summary>
    ///     Creates the datastorage on the filesystem.
    /// </summary>
    internal sealed class DataStorageSetupService : DataStorageSetupBase
    {
        /// <summary>
        ///     Gets the filenames of the file repositories.
        /// </summary>
        private static readonly string[] RepositoryFiles = {"hosts.json"};


        public DataStorageSetupService(IFileProvider fileProvider) : base(fileProvider)
        {
        }

        /// <inheritdoc />
        public override void CreateOrMigrateDataStorage()
        {
            CreateDataDirectoryIfNotExists();
            CreateFilesIfNotExists(RepositoryFiles);
        }
    }
}