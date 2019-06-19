using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMusicStore.Contracts
{
	public interface FileStorage
	{
		Task<string> Upload(IFormFile file, string fileName);
	}
}
