using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace PiWol.DataStorage.Core
{
    public abstract class JsonRepositoryBase<TEntity>
    {
        private const string DataDirectory = "data";

        private readonly IFileProvider _fileProvider;

        protected IList<TEntity> Data;

        protected IQueryable<TEntity> DataSource;

        protected JsonRepositoryBase(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
            LoadDataSource();
        }

        protected abstract string DataFile { get; set; }

        protected void WriteToFile()
        {
            DataSource = Data.AsQueryable();

            var serialize = JsonConvert.SerializeObject(Data);
            var fi = _fileProvider.GetFileInfo(Path.Combine(DataDirectory, DataFile));

            File.WriteAllText(fi.PhysicalPath, serialize);
        }


        protected void LoadDataSource()
        {
            var fi = _fileProvider.GetFileInfo(Path.Combine(DataDirectory, DataFile));

            Data = JsonConvert.DeserializeObject<IList<TEntity>>(File.ReadAllText(fi.PhysicalPath));
            DataSource = Data.AsQueryable();
        }
    }
}