using FinalProject.Dtos.MobilePhoneImage;
using FinalProject.Models;
using FinalProject.Repositories;
using FinalProject.Utilities;

namespace FinalProject.Services
{
    public class MobilePhoneImageService
    {
        private readonly MobilePhoneImageRepository _mobilePhoneImageRepository;

        public MobilePhoneImageService(MobilePhoneImageRepository mobilePhoneImageRepository)
        {
            _mobilePhoneImageRepository = mobilePhoneImageRepository;
        }

        public async Task<List<MobilePhoneImageDTO>> AddMobilePhoneImages(int mobilePhoneId, List<IFormFile> images)
        {
            var savedImages = new List<MobilePhoneImage>();

            foreach(var image in images)
            {
                var savedPath = await ImagesHelper.SaveImageToDisk(image, mobilePhoneId);

                savedImages.Add(new MobilePhoneImage
                {
                    MobilePhoneId = mobilePhoneId,
                    ImageUrl = savedPath,
                    UploadedAt = DateTime.UtcNow,
                });
            }

            var addedImages = await _mobilePhoneImageRepository.AddMobilePhoneImages(savedImages);

            return addedImages.Select(image => new MobilePhoneImageDTO
            {
                Id = image.Id,
                MobilePhoneId = image.MobilePhoneId,
                ImageUrl= image.ImageUrl,
                UploadedAt = DateTime.UtcNow,
            }).ToList();
        }

        public async Task<IEnumerable<MobilePhoneImageDTO>> GetImagesByPhoneId(int mobilePhoneId)
        {
            var images = await _mobilePhoneImageRepository.GetImagesById(mobilePhoneId);

            return images.Select(image => new MobilePhoneImageDTO
            {
                Id = image.Id,
                MobilePhoneId = image.MobilePhoneId,
                ImageUrl = image.ImageUrl,
                UploadedAt = DateTime.UtcNow,
            });
        }

        public async Task DeleteImagesByPhoneId(int mobilePhoneId)
        {
            await _mobilePhoneImageRepository.DeleteMobilePhoneImages(mobilePhoneId);
        }
    }
}
