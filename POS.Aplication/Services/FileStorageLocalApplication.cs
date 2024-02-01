﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using POS.Aplication.Interfaces;
using POS.Infraestructure.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Aplication.Services
{
    public class FileStorageLocalApplication : IFileStorageLocalApplication
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileStorageLocal _fileStorageLocal;

        public FileStorageLocalApplication(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, IFileStorageLocal fileStorageLocal = null)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _fileStorageLocal = fileStorageLocal;
        }

        public async Task<string> SaveFiles(string container, IFormFile file)
        {
            var webRootPath = _env.WebRootPath;
            var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;

            return await _fileStorageLocal.SaveFile(container, file, webRootPath, scheme, host.Value);

        }
        public async Task<string> EditFile(string container, IFormFile file, string route)
        {
            var webRootPath = _env.WebRootPath;
            var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;

            return await _fileStorageLocal.EditFile(container, file, route, webRootPath, scheme, host.Value);
        }

        public async Task RemoveFile(string route, string container)
        {
            var webRootPath = _env.WebRootPath;

            await _fileStorageLocal.RemoveFile(route, container, webRootPath);
        }


    }
}
