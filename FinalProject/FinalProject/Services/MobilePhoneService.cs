using FinalProject.Domain.Models;
using FinalProject.Domain.ViewModels.MobilePhone;
using FinalProject.Domain.ViewModels.MobilePhoneImage;
using FinalProject.Repositories;
using FinalProject.Utilities;

namespace FinalProject.Services
{
    public class MobilePhoneService
    {
        private readonly MobilePhoneRepository _mobilePhoneRepository;
        private readonly IConfiguration _configuration;

        public MobilePhoneService(MobilePhoneRepository mobilePhoneRepository,IConfiguration configuration)
        {
            _mobilePhoneRepository = mobilePhoneRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<MobilePhoneVM>> GetMobilePhones(List<string> brands = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var phones = await _mobilePhoneRepository.GetMobilePhones(brands, minPrice, maxPrice);
            return phones.Select(phone => new MobilePhoneVM
            {
                Id = phone.Id,
                Brand = phone.Brand,
                Model = phone.Model,
                Specifications = phone.Specifications,
                Price = phone.Price,
                DiscountPrice = phone.DiscountPrice,
                MainImage = phone.MainImage,
                CreatedAt = phone.CreatedAt
            });
        }

        public async Task<MobilePhoneVM> GetMobilePhoneById(int id)
        {
            var phone = await _mobilePhoneRepository.GetMobilePhoneById(id);

            if(phone == null)
            {
                return null;
            }

            var images = await _mobilePhoneRepository.GetImagesByPhoneId(id);

            return new MobilePhoneVM
            {
                Id = phone.Id,
                Brand = phone.Brand,
                Model = phone.Model,
                Specifications = phone.Specifications,
                Price = phone.Price,
                DiscountPrice = phone.DiscountPrice,
                MainImage = phone.MainImage,
                CreatedAt = phone.CreatedAt,
                AdditionalImages = images.Select(img => new MobilePhoneImageVM
                {
                    Id = img.Id,
                    MobilePhoneId = img.MobilePhoneId,
                    ImageUrl = img.ImageUrl,
                    UploadedAt = img.UploadedAt
                }).ToList()
            };
        }

        public async Task<IEnumerable<MobilePhoneVM>> SearchMobilePhones(string keyword)
        {
            var phones = await _mobilePhoneRepository.SeachMobilePhones(keyword);

            return phones.Select(phone => new MobilePhoneVM
            {
                Id = phone.Id,
                Brand = phone.Brand,
                Model = phone.Model,
                Specifications = phone.Specifications,
                Price = phone.Price,
                DiscountPrice = phone.DiscountPrice,
                MainImage = phone.MainImage,
                CreatedAt = phone.CreatedAt
            });
        }

        public async Task<IEnumerable<MobilePhoneVM>> GetDiscountedMobilePhones()
        {
            var phones = await _mobilePhoneRepository.GetDiscountedMobilePhones();

            return phones.Select(phone => new MobilePhoneVM
            {
                Id = phone.Id,
                Brand = phone.Brand,
                Model = phone.Model,
                Specifications = phone.Specifications,
                Price = phone.Price,
                DiscountPrice = phone.DiscountPrice,
                MainImage = phone.MainImage,
                CreatedAt = phone.CreatedAt
            });
        }

        public async Task<IEnumerable<MobilePhoneVM>> GetNewestMobilePhones(int count)
        {
            var phones = await _mobilePhoneRepository.GetNewestMobilePhones(count);

            return phones.Select(phone => new MobilePhoneVM
            {
                Id = phone.Id,
                Brand = phone.Brand,
                Model = phone.Model,
                Specifications = phone.Specifications,
                Price = phone.Price,
                DiscountPrice = phone.DiscountPrice,
                MainImage = phone.MainImage,
                CreatedAt = phone.CreatedAt
            });
        }

        public async Task<MobilePhoneVM> AddMobilePhone(CreateMobilePhoneVM mobilePhoneVM)
        {
            int newId = await _mobilePhoneRepository.GetLatestPhoneId() + 1;

            var mainImgPath = await ImagesConverter.SaveImageToDisk(mobilePhoneVM.MainImage, newId);

            var mobilePhone = new MobilePhone
            {
                Brand = mobilePhoneVM.Brand,
                Model = mobilePhoneVM.Model,
                Price = mobilePhoneVM.Price,
                DiscountPrice = mobilePhoneVM.DiscountPrice,
                MainImage = mainImgPath,
                Specifications = mobilePhoneVM.Specifications
            };

            var addedPhone = await _mobilePhoneRepository.AddMobilePhone(mobilePhone);

            if(mobilePhoneVM.AdditionalImages != null && mobilePhoneVM.AdditionalImages.Any())
            {
                var images = new List<MobilePhoneImage>();

                foreach(var image in mobilePhoneVM.AdditionalImages)
                {
                    var imgPath = await ImagesConverter.SaveImageToDisk(image.Image, addedPhone.Id);

                    images.Add(new MobilePhoneImage
                    {
                        MobilePhoneId = addedPhone.Id,
                        ImageUrl = imgPath,
                        UploadedAt = DateTime.UtcNow
                    });
                }

                await _mobilePhoneRepository.AddMobilePhoneImages(images);
            }

            return new MobilePhoneVM
            {
                Id = addedPhone.Id,
                Brand = addedPhone.Brand,
                Model = addedPhone.Model,
                Specifications = addedPhone.Specifications,
                Price = addedPhone.Price,
                DiscountPrice = addedPhone.DiscountPrice,
                MainImage = addedPhone.MainImage,
                CreatedAt = addedPhone.CreatedAt
            };
        }

        public async Task<MobilePhoneVM> UpdateMobilePhone(int id, UpdateMobilePhoneVM updateMobilePhoneVM)
        {

            var existingPhone = await _mobilePhoneRepository.GetMobilePhoneById(id);

            if(existingPhone == null)
            {
                return null;
            }

            existingPhone.Brand = updateMobilePhoneVM.Brand ?? existingPhone.Brand;
            existingPhone.Model = updateMobilePhoneVM.Model ?? existingPhone.Model;
            existingPhone.Specifications = updateMobilePhoneVM.Specifications ?? existingPhone.Specifications;
            existingPhone.Price = updateMobilePhoneVM.Price ?? existingPhone.Price;
            existingPhone.DiscountPrice = updateMobilePhoneVM.DiscountPrice ?? existingPhone.DiscountPrice;
            existingPhone.MainImage = updateMobilePhoneVM.MainImage ?? existingPhone.MainImage;

            await _mobilePhoneRepository.DeleteMobilePhoneImages(id);

            if(updateMobilePhoneVM.NewImages != null && updateMobilePhoneVM.NewImages.Any())
            {
                var images = new List<MobilePhoneImage>();

                foreach(var image in updateMobilePhoneVM.NewImages)
                {
                    var imgPath = await ImagesConverter.SaveImageToDisk(image.Image, id);

                    images.Add(new MobilePhoneImage
                    {
                        MobilePhoneId = id,
                        ImageUrl = imgPath,
                        UploadedAt = DateTime.UtcNow,
                    });
                }

                await _mobilePhoneRepository.AddMobilePhoneImages(images);
            }

            var updatedPhone = await _mobilePhoneRepository.UpdateMobilePhone(existingPhone);

            return new MobilePhoneVM
            {
                Id = updatedPhone.Id,
                Brand = updatedPhone.Brand,
                Model = updatedPhone.Model,
                Specifications = updatedPhone.Specifications,
                Price = updatedPhone.Price,
                DiscountPrice = updatedPhone.DiscountPrice,
                MainImage = updatedPhone.MainImage,
                CreatedAt = updatedPhone.CreatedAt
            };
        }

        public async Task<bool> DeleteMobilePhone(int id)
        {
            await _mobilePhoneRepository.DeleteMobilePhoneImages(id);

            return await _mobilePhoneRepository.DeleteMobilePhone(id);
        }


        /*
         *      MobilePhoneImages Methods
         */

        public async Task<List<MobilePhoneImageVM>> AddMobilePhoneImages(int mobilePhoneId, List<IFormFile> images)
        {
            var savedImages = new List<MobilePhoneImage>();

            foreach (var image in images)
            {
                var savedPath = await ImagesConverter.SaveImageToDisk(image, mobilePhoneId);

                savedImages.Add(new MobilePhoneImage
                {
                    MobilePhoneId = mobilePhoneId,
                    ImageUrl = savedPath,
                    UploadedAt = DateTime.UtcNow,
                });
            }

            var addedImages = await _mobilePhoneRepository.AddMobilePhoneImages(savedImages);

            return addedImages.Select(image => new MobilePhoneImageVM
            {
                Id = image.Id,
                MobilePhoneId = image.MobilePhoneId,
                ImageUrl = image.ImageUrl,
                UploadedAt = image.UploadedAt
            }).ToList();
        }

    }
}
