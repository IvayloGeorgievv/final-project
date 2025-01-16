using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class MobilePhoneImageRepository
    {
        private readonly ApplicationDbContext _context;

        public MobilePhoneImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }   

        public async Task<IEnumerable<MobilePhoneImage>> GetImagesById(int phoneId)
        {
            return await _context.MobilePhoneImages
                .Where(m => m.MobilePhoneId == phoneId)
                .ToListAsync();
        }

        public async Task<List<MobilePhoneImage>> AddMobilePhoneImages(List<MobilePhoneImage> images)
        {
            await _context.MobilePhoneImages.AddRangeAsync(images);
            await _context.SaveChangesAsync();

            return images;
        }

        public async Task DeleteMobilePhoneImages(int phoneId)
        {
            var images = await _context.MobilePhoneImages
                .Where(m => m.MobilePhoneId == phoneId)
                .ToListAsync();

            if (images.Any())
            {
                _context.MobilePhoneImages.RemoveRange(images);

                await _context.SaveChangesAsync();
            }
        }
    }
}
