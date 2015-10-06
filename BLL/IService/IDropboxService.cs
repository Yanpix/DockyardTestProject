using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.IService
{
    public interface IDropboxService
    {
        Task<Uri> AuthorizeAsync();
        Task SetCode(string code, string error);
        Task<List<DropBoxFile>> FindFilesByPath(string path);
    }
}