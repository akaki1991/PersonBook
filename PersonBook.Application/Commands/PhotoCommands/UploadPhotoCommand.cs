using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PersonBook.Application.Infrastructure;
using PersonBook.Domain.PhotoAggregate;
using SixLabors.ImageSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonBook.Application.Commands.PhotoCommands
{
    [Validator(typeof(UploadPhotoCommandValidator))]
    public class UploadPhotoCommand : Command
    {
        public IFormFile Photo { get; set; }

        public async override Task<CommandExecutionResult> ExecuteAsync()
        {
            var hostingEnv = GetService<IHostingEnvironment>();

            var uploadsFolder = Path.Combine(hostingEnv.ContentRootPath, $"Files/Images");

            var extension = "." + Photo.FileName.Split('.').Last();

            var fileName = Guid.NewGuid().ToString() + extension;

            var filePath = Path.Combine(uploadsFolder, fileName);

            var width = default(int);
            var height = default(int);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Photo.CopyTo(stream);

                var image = Image.Load(Photo.OpenReadStream());
                width = image.Width;
                height = image.Height;
            }

            var photo = new Photo(Path.Combine(uploadsFolder, fileName), width, height);

            await SaveAsync(photo, _photoRepository);

            return await OkAsync(DomainOperationResult.Create(photo.Id));
        }
    }

    public class UploadPhotoCommandValidator : AbstractValidator<UploadPhotoCommand>
    {
        public UploadPhotoCommandValidator()
        {
            RuleFor(x => x.Photo).NotEmpty();
        }
    }
}
