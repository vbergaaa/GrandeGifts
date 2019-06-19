using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RyanGrandeGifts.ApiModels;
using RyanGrandeGifts.Models;
using RyanGrandeGifts.Services;

namespace RyanGrandeGifts.Controllers
{
    [Route("api/hampers")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IDataService<Category> _categoryManager;
        private readonly IDataService<Hamper> _HamperManager;

        public ApiController(IDataService<Category> categoryManager,
            IDataService<Hamper> _hamperManager)
        {
            _categoryManager = categoryManager;
            _HamperManager = _hamperManager;
        }

        [HttpGet]
        public IEnumerable<HamperApiModel> GetAllHampers()
        {
            return _HamperManager.GetAll().Where(h => h.Active).Select(h => new HamperApiModel
            {
                HamperName = h.HamperName,
                HamperPrice = h.HamperPrice,
                HamperDescription = h.HamperDescription,
                Picture = h.Picture
            });
        }
        [HttpGet("{keyword}")]
        public IEnumerable<HamperApiModel> GetHampersByCategory(string keyword)
        {
            return _HamperManager.GetAll()
                .Include(h=>h.Category)
                .Where(h=>h.Category.CategoryName.Contains(keyword) && h.Active)
                .Select(h => new HamperApiModel
                    {
                        HamperName = h.HamperName,
                        HamperPrice = h.HamperPrice,
                        HamperDescription = h.HamperDescription,
                        Picture = h.Picture
                    }
                );
        }
    }
}