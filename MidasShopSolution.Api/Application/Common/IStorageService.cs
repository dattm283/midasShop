using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidasShopSolution.Api.Application.Common;

public interface IStorageService
{
    string GetFileUrl(string fileName);
    Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
    Task DeleteFileAsync(string fileName);
}
