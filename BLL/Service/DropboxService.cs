using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IService;
using BLL.Models;
using DropboxRestAPI;

namespace BLL.Service
{
    public class DropboxService : IDropboxService
    {
        private readonly IClient _client;

        public DropboxService(IClient client)
        {
            _client = client;
        }

        public async Task<Uri> AuthorizeAsync()
        {
            // Get the OAuth Request Url
            return await _client.Core.OAuth2.AuthorizeAsync("code");
        }

        public async Task SetCode(string code, string error)
        {
            // Exchange the Authorization Code with Access/Refresh tokens
            await _client.Core.OAuth2.TokenAsync(code);
        }

        public async Task<List<DropBoxFile>> FindFilesByPath(string path)
        {
            var files = new List<DropBoxFile>();
            // Get root folder with content
            var rootFolder = await _client.Core.Metadata.MetadataAsync(path != null ? string.Format("/{0}", path) : "/");

            if (rootFolder.contents != null)
            {
                files.AddRange(rootFolder.contents.Select(x => new DropBoxFile
                {
                    Name = x.Name,
                    Path = x.path,
                    Size = x.size,
                    Icon = x.icon.GetIconByMimeType(),
                    ModifiedDate = DateTime.Parse(x.modified)
                }));
                return files;
            }
            return files;
        }
    }
}