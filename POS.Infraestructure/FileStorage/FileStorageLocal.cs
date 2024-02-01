using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.FileStorage
{
    public class FileStorageLocal : IFileStorageLocal
    {

        public async Task<string> SaveFile(string container, IFormFile file, string webRootPath, string sheme, string host)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(webRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string path=Path.Combine(folder, fileName);
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                await File.WriteAllBytesAsync(path, content);
            }

            var currentUrl = $"{sheme}://{host}";
            var pathDb= Path.Combine(currentUrl,container,fileName).Replace('\\', '/');
            return pathDb;
        }

        public async Task<string> EditFile(string container, IFormFile file, string route, string webRootPath, string sheme, string host)
        {
            await RemoveFile(route, container, webRootPath);

            return await SaveFile(container,file, webRootPath, sheme, host);
        }

        public Task RemoveFile(string route, string container, string webRootPath)
        {
            if (string.IsNullOrEmpty(route))
            {
                return Task.CompletedTask;
            }

            var fileName = Path.GetFileName(route);
            var directoryFile=Path.Combine(webRootPath, container,fileName);

            if (File.Exists(directoryFile))
            {
                File.Delete(directoryFile);
            }

            return Task.CompletedTask;
        }

    }
}
