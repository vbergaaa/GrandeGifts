using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanGrandeGifts.Services
{
    public static class SeedHelper
    {
        //public static async Task Seed(IServiceProvider provider)
        //{
        //    IServiceScopeFactory scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

        //    using (var scope = scopeFactory.CreateScope())
        //    {
        //        //add sample categories
        //        DataService<Category> _categoryDataService = new DataService<Category>();
        //        Category cat1 = new Category
        //        {
        //            CategoryName = "Dinner",
        //            CategoryDescription = "Hampers with food suitable for dinner"
        //        };
        //        Category cat2 = new Category
        //        {
        //            CategoryName = "Lunch",
        //            CategoryDescription = "Hampers with food suitable for lunch "
        //        };
        //        Category cat3 = new Category
        //        {
        //            CategoryName = "Breakfast",
        //            CategoryDescription = "Hampers with food suitable for breaky"
        //        };
        //        Category cat4 = new Category
        //        {
        //            CategoryName = "Add Day",
        //            CategoryDescription = "These Hampers have food suitable for all day round",
        //        };
        //        _categoryDataService.Add(cat1);
        //        _categoryDataService.Add(cat2);
        //        _categoryDataService.Add(cat3);
        //        _categoryDataService.Add(cat4);

        //        //add sample Hampers
        //        DataService<Hamper> _hamperDataService = new DataService<Hamper>();
        //        Hamper h1 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Dinner").CategoryId,
        //            HamperDescription = "This Hamper has a wonderful selection of pasta-based dishes",
        //            HamperName = "Pasta Suprise",
        //            HamperPrice = 30,
        //        };
        //        Hamper h2 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Dinner").CategoryId,
        //            HamperDescription = "Snag, Steaks, Schnittys, what more could you need?",
        //            HamperName = "BBQ Meats",
        //            HamperPrice = 50,
        //        };
        //        Hamper h3 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Dinner").CategoryId,
        //            HamperDescription = "50 burgers containing chicken, beef or lamb meat",
        //            HamperName = "50 Burgers",
        //            HamperPrice = 70,
        //        };
        //        Hamper h4 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Lunch").CategoryId,
        //            HamperDescription = "Forget vegimite and peanut butter, this sandwich package is fresh!",
        //            HamperName = "Cold Meat Sandwiches",
        //            HamperPrice = 30,
        //        };
        //        Hamper h5 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Lunch").CategoryId,
        //            HamperDescription = "This assortment of pies can be stored to eat on those cold days",
        //            HamperName = "30 pack pie supirise",
        //            HamperPrice = 40,
        //        };
        //        Hamper h6 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Lunch").CategoryId,
        //            HamperDescription = "Want a break from meat? or just to try something new? this is the hamper for you!",
        //            HamperName = "Vegetarian Variety",
        //            HamperPrice = 45,
        //        };

        //        Hamper h7 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Breakfast").CategoryId,
        //            HamperDescription = "40 serves of bread, bacon, eggs, mushrooms and tomatoes!",
        //            HamperName = "Big Breaky Basics",
        //            HamperPrice = 50,
        //        };
        //        Hamper h8 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Breakfast").CategoryId,
        //            HamperDescription = "Just like a chocolate milkshake, only crunchy!",
        //            HamperName = "15 pack of cereals",
        //            HamperPrice = 15,
        //        };
        //        Hamper h9 = new Hamper
        //        {
        //            Active = true,
        //            CategoryId = _categoryDataService.GetSingle(c => c.CategoryName == "Breakfast").CategoryId,
        //            HamperDescription = "Vegemite, Jam, and the rest are the best",
        //            HamperName = "Toast and spreads",
        //            HamperPrice = 15,
        //        };
        //        _hamperDataService.Add(h1);
        //        _hamperDataService.Add(h2);
        //        _hamperDataService.Add(h3);
        //        _hamperDataService.Add(h4);
        //        _hamperDataService.Add(h5);
        //        _hamperDataService.Add(h6);
        //        _hamperDataService.Add(h7);
        //        _hamperDataService.Add(h8);
        //        _hamperDataService.Add(h9);

        //        // Create Users 
        //        UserManager<ApplicationUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //        RoleManager<IdentityRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //        IdentityRole role1 = new IdentityRole("Admin");
        //        IdentityRole role2 = new IdentityRole("Customer");
        //        await _roleManager.CreateAsync(role1);
        //        await _roleManager.CreateAsync(role2);
        //        ApplicationUser u1 = new ApplicationUser { UserName = "Admin" };
        //        ApplicationUser u2 = new ApplicationUser { UserName = "vbergaaa" };
        //        ApplicationUser u3 = new ApplicationUser { UserName = "Test" };
        //        await _userManager.CreateAsync(u1,"Admin1");
        //        await _userManager.CreateAsync(u2, "Admin1");
        //        await _userManager.CreateAsync(u3, "Test123");
        //        await _userManager.AddToRoleAsync(u1, "Admin");
        //        await _userManager.AddToRoleAsync(u2, "Admin");
        //        await _userManager.AddToRoleAsync(u3, "Customer");

        //        // Add addresses
        //        DataService<Address> _addressDataService = new DataService<Address>();
        //        Address a1 = new Address
        //        {
        //            UnitNumber = 11,
        //            StreetNumber = 12,
        //            StreetName = "Crystal St",
        //            State = StateEnum.NSW,
        //            City = "Sydney",
        //            PostCode = "2000",
        //            Suburb = "Petersham",
        //            CustomerId = _userManager.Users.FirstOrDefault(u => u.UserName == "Test").Id,
        //        };
        //        Address a2 = new Address
        //        {
        //            StreetNumber = 17,
        //            StreetName = "Creedon St",
        //            State = StateEnum.VIC,
        //            City = "Carlton South",
        //            PostCode = "3053",
        //            Suburb = "Carlton South",
        //            CustomerId = _userManager.Users.FirstOrDefault(u => u.UserName == "Test").Id,
        //        };
        //        _addressDataService.Add(a1);
        //        _addressDataService.Add(a2);
        //    }
        //}
    }
    
}
