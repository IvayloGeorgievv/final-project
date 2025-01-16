﻿using FinalProject.Repositories;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class MobilePhoneController : Controller
    {
        private readonly MobilePhoneService _mobilePhoneService;

        public MobilePhoneController(MobilePhoneService mobilePhoneService)
        {
            _mobilePhoneService = mobilePhoneService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMobilePhones([FromQuery] string brand, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var brands = !string.IsNullOrEmpty(brand)
                ? brand.Split(',').Select(b => b.Trim()).ToList()
                : null;

            var phones = await _mobilePhoneService.GetMobilePhones(brands, minPrice, maxPrice);

            return Ok(phones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMobilePhoneById(int id)
        {
            var phone = await _mobilePhoneService.GetMobilePhoneById(id);
            if (phone == null)
            {
                return NotFound($"Mobile phone with ID {id} not found.");
            }

            return Ok(phone);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMobilePhones([FromQuery] string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest(new { Message = "Search keyword required." });
            }

            var result = await _mobilePhoneService.SearchMobilePhones(keyword);

            return Ok(result);
        }

        [HttpGet("discounted")]
        public async Task<IActionResult> GetDiscountedMobilePhones()
        {
            var discountedPhones = await _mobilePhoneService.GetDiscountedMobilePhones();
            return Ok(discountedPhones);
        }

        [HttpGet("newest")]
        public async Task<IActionResult> GetNewestMobilePhones()
        {
            var newestPhones = await _mobilePhoneService.GetNewestMobilePhones(3);
            return Ok(newestPhones);
        }
    }
}
