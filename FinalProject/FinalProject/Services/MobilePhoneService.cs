using FinalProject.Dtos.MobilePhone;
using FinalProject.Dtos.MobilePhoneImage;
using FinalProject.Models;
using FinalProject.Repositories;
using FinalProject.Utilities;

namespace FinalProject.Services
{
    public class MobilePhoneService
    {
        private readonly MobilePhoneRepository _mobilePhoneRepository;
        private readonly MobilePhoneImageRepository _mobilePhoneImageRepository;
        private readonly IConfiguration _configuration;

        public MobilePhoneService(MobilePhoneRepository mobilePhoneRepository, MobilePhoneImageRepository mobilePhoneImageRepository, IConfiguration configuration)
        {
            _mobilePhoneRepository = mobilePhoneRepository;
            _mobilePhoneImageRepository = mobilePhoneImageRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<MobilePhoneDTO>> GetMobilePhones(List<string> brands = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var phones = await _mobilePhoneRepository.GetMobilePhones(brands, minPrice, maxPrice);
            return phones.Select(phone => new MobilePhoneDTO
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

        public async Task<MobilePhoneDTO> GetMobilePhoneById(int id)
        {
            var phone = await _mobilePhoneRepository.GetMobilePhoneById(id);

            if(phone == null)
            {
                return null;
            }

            return new MobilePhoneDTO
            {
                Id = phone.Id,
                Brand = phone.Brand,
                Model = phone.Model,
                Specifications = phone.Specifications,
                Price = phone.Price,
                DiscountPrice = phone.DiscountPrice,
                MainImage = phone.MainImage,
                CreatedAt = phone.CreatedAt
            };
        }

        public async Task<IEnumerable<MobilePhoneDTO>> SearchMobilePhones(string keyword)
        {
            var phones = await _mobilePhoneRepository.SeachMobilePhones(keyword);

            return phones.Select(phone => new MobilePhoneDTO
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

        public async Task<IEnumerable<MobilePhoneDTO>> GetDiscountedMobilePhones()
        {
            var phones = await _mobilePhoneRepository.GetDiscountedMobilePhones();

            return phones.Select(phone => new MobilePhoneDTO
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

        public async Task<IEnumerable<MobilePhoneDTO>> GetNewestMobilePhones(int count)
        {
            var phones = await _mobilePhoneRepository.GetNewestMobilePhones(count);

            return phones.Select(phone => new MobilePhoneDTO
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

        public async Task<MobilePhoneDTO> AddMobilePhone(CreateMobilePhoneDTO mobilePhoneDTO)
        {
            var mobilePhone = new MobilePhone
            {
                Brand = mobilePhoneDTO.Brand,
                Model = mobilePhoneDTO.Model,
                Price = mobilePhoneDTO.Price,
                DiscountPrice = mobilePhoneDTO.DiscountPrice,
                MainImage = mobilePhoneDTO.MainImage,
                Specifications = mobilePhoneDTO.Specifications
            };

            var addedPhone = await _mobilePhoneRepository.AddMobilePhone(mobilePhone);

            return new MobilePhoneDTO
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

        public async Task<MobilePhoneDTO> UpdateMobilePhone(int id, UpdateMobilePhoneDTO updateMobilePhoneDTO)
        {

            var existingPhone = await _mobilePhoneRepository.GetMobilePhoneById(id);

            if(existingPhone == null)
            {
                return null;
            }

            existingPhone.Brand = updateMobilePhoneDTO.Brand ?? existingPhone.Brand;
            existingPhone.Model = updateMobilePhoneDTO.Model ?? existingPhone.Model;
            existingPhone.Specifications = updateMobilePhoneDTO.Specifications ?? existingPhone.Specifications;
            existingPhone.Price = updateMobilePhoneDTO.Price ?? existingPhone.Price;
            existingPhone.DiscountPrice = updateMobilePhoneDTO.DiscountPrice ?? existingPhone.DiscountPrice;
            existingPhone.MainImage = updateMobilePhoneDTO.MainImage ?? existingPhone.MainImage;

            var updatedPhone = await _mobilePhoneRepository.UpdateMobilePhone(existingPhone);

            return new MobilePhoneDTO
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
            return await _mobilePhoneRepository.DeleteMobilePhone(id);
        }

        public async Task<List<MobilePhoneImageDTO>> AddMobilePhoneImages(int mobilePhoneId, List<IFormFile> images)
        {
            var savedImages = new List<MobilePhoneImage>();

            foreach (var image in images)
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
                ImageUrl = image.ImageUrl,
                UploadedAt = image.UploadedAt
            }).ToList();
        }

    }
}
