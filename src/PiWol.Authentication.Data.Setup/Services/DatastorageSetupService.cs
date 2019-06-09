using Microsoft.Extensions.FileProviders;
using PiWol.DataStorage.Core;

namespace PiWol.Authentication.Data.Setup.Services
{
    /// <summary>
    ///     Creates the datastorage on the filesystem.
    /// </summary>
    internal sealed class DataStorageSetupService : DataStorageSetupBase
    {
        /// <summary>
        ///     Gets the filenames of the file repositories.
        /// </summary>
        private static readonly string[] RepositoryFiles = {"auth-ips.json"};

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