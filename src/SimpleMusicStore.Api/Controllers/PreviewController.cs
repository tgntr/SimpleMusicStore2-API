using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.ModelValidations;

namespace SimpleMusicStore.Api.Controllers
{
    public class PreviewController : Controller
    {
        private readonly FileStorage _cloud;

        public PreviewController(FileStorage cloud) : base()
        {
            _cloud = cloud;
        }
        [HttpPost]
        public Task Upload(string fileName, [Required, NonEmptyMp3File]IFormFile file)
        {
            return _cloud.Upload(file, fileName);
        }
    }
}