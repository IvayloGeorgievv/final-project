using SixLabors.ImageSharp.Formats.Webp;

namespace FinalProject.Utilities
{
    public class ImagesHelper
    {
        public static async Task<string> SaveImageToDisk(IFormFile image, int mobilePhoneId)
        {
            var uploadPath = Path.Combine("wwwroot", "uploads", mobilePhoneId.ToString());
            Directory.CreateDirectory(uploadPath);

            var tempfileExtension = Path.GetExtension(image.FileName);
            var tempfileName = $"{Guid.NewGuid()}{tempfileExtension}";
            var tempfilePath = Path.Combine(uploadPath, tempfileName);

            using (var stream = new FileStream(tempfilePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            var webpFilePath = await ConvertToWebp(tempfilePath, mobilePhoneId);

            return webpFilePath;
        }

        public static async Task<string> ConvertToWebp(string imagePath, int mobilePhoneId)
        {
            var uploadPath = Path.Combine("wwwroot", "uploads", mobilePhoneId.ToString());
            Directory.CreateDirectory(uploadPath);

            var fileName = $"{Path.GetFileNameWithoutExtension(imagePath)}.webp";
            var webpFilePath = Path.Combine(uploadPath, fileName);

            using (var image = await Image.LoadAsync(imagePath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(1024, 0)
                }));

                await image.SaveAsync(webpFilePath, new WebpEncoder { Quality = 75 });
            }

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            return webpFilePath;
        }
    }
}
