using SixLabors.ImageSharp.Formats.Webp;

namespace FinalProject.Utilities
{
    public class ImagesConverter
    {
        //Using FileStream to send the file to wanted location for uploads
        public static async Task<string> SaveImageToDisk(IFormFile image, int mobilePhoneId)
        {
            var uploadPath = Path.Combine("wwwroot", "uploads", mobilePhoneId.ToString());

            if(!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
           
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


        //Converting image to Webp format using SixLabors.ImageSharp Package
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

            //Deleting the original file so images don't occupy too much space
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            return webpFilePath;
        }
    }
}
