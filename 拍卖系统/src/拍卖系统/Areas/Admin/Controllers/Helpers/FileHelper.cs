using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Areas.Admin.Controllers.Helpers
{
	public static class FileHelper
	{
		public static async Task SaveAsAsync(this IFormFile file, string filepath)
		{
			//保存文件
			using (var fileStream = new FileStream(filepath, FileMode.Create))
			{
				var inputStream = file.OpenReadStream();
				await inputStream.CopyToAsync(fileStream);
			}
		}
	}
}
