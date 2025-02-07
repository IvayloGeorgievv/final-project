﻿using FinalProject.DataBase;
using FinalProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class MobilePhoneRepository
    {
        private readonly ApplicationDbContext _context;

        public MobilePhoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MobilePhone>> GetMobilePhones(List<string> brands = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var query = _context.MobilePhones.AsQueryable();

            if (brands != null && brands.Any())
            {
                query = query.Where(m => brands.Contains(m.Brand));
            }

            if(minPrice != null)
            {
                query = query.Where(m => m.Price >= minPrice.Value);
            }

            if(maxPrice != null)
            {
                query = query.Where(m => m.Price <= maxPrice.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<MobilePhone>> GetDiscountedMobilePhones()
        {
            return await _context.MobilePhones
                .Where(m => m.DiscountPrice != null)
                .ToListAsync();
        }

        public async Task<IEnumerable<MobilePhone>> GetNewestMobilePhones(int count)
        {
            return await _context.MobilePhones
                .OrderByDescending(m => m.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<MobilePhone> GetMobilePhoneById(int id)
        {
            return await _context.MobilePhones.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MobilePhone>> SeachMobilePhones(string searchTerm)
        {
            return await _context.MobilePhones
                .Where(m => m.Brand.Contains(searchTerm) || m.Model.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<bool> MobilePhoneExists(int id)
        {
            return await _context.MobilePhones.AnyAsync(m => m.Id == id);
        }

        //It is used to get the newest id before the new phone is added to db for the mainImage upload
        public async Task<int> GetLatestPhoneId()
        {
            return await _context.MobilePhones
            .OrderByDescending(phone => phone.Id)
            .Select(phone => phone.Id)
            .FirstOrDefaultAsync();
        }

        public async Task<MobilePhone> AddMobilePhone(MobilePhone mobilePhone)
        {
            await _context.MobilePhones.AddAsync(mobilePhone);
            await _context.SaveChangesAsync();

            return mobilePhone;
        }

        public async Task<MobilePhone> UpdateMobilePhone(MobilePhone mobilePhone)
        {
            var existingPhone = await _context.MobilePhones.FirstOrDefaultAsync(m => m.Id == mobilePhone.Id);

            if (existingPhone != null)
            {
                existingPhone.Brand = mobilePhone.Brand;
                existingPhone.Model = mobilePhone.Model;
                existingPhone.Specifications = mobilePhone.Specifications;
                existingPhone.Price = mobilePhone.Price;
                existingPhone.DiscountPrice = mobilePhone.DiscountPrice;
                existingPhone.MainImage = mobilePhone.MainImage;

                await _context.SaveChangesAsync();

                return existingPhone;
            }

            return null;
        }

        public async Task<bool> DeleteMobilePhone(int id)
        {
            var mobilePhone = await _context.MobilePhones.FindAsync(id);

            if(mobilePhone == null)
            {
                return false;
            }

            _context.MobilePhones.Remove(mobilePhone);
            await _context.SaveChangesAsync();
            return true;
        }


        /*
         *      MobilePhoneImages Methods
         */

        public async Task<IEnumerable<MobilePhoneImage>> GetImagesByPhoneId(int phoneId)
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
