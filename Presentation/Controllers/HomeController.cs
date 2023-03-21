﻿using distance_post_gis.Presentation.Controllers.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure;
using NetTopologySuite.Geometries;

namespace Project.Application.Presentation;

[ApiController]
[Route("home")]
public class HomeController : ControllerBase
{
    private readonly MyDbContext _myDbContext;
    private readonly DbSet<Facility> _facilitySet;
    private readonly DbSet<User> _userSet;

    private readonly Guid JohnId = Guid.Parse("ef303c96-50ab-4ab7-a89b-9b995f2b4039");

    public HomeController(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
        _userSet = _myDbContext.Set<User>();
        _facilitySet = _myDbContext.Set<Facility>();
    }

    
    [HttpPost("addFacility")]
    public async Task<IActionResult> AddFacility(string name, double lat, double lon)
    {
        _facilitySet.Add(new Facility()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Latitude = lat,
            Longitude = lon,
            Location = new Point(lat, lon)
        });

        _myDbContext.SaveChanges();

        return Ok("this is a test");
    }

    [HttpPost("addUser")]
    public async Task<IActionResult> AddUser(string name, double lat, double lon)
    {
        _userSet.Add(new User()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Latitude = lat,
            Longitude = lon,
            Location = new Point(lat, lon)
        });

        _myDbContext.SaveChanges();

        return Ok("this is a test");
    }

    [HttpGet("get-facilities")]
    public async Task<IActionResult> GetFacilities()
    {
        var allFacilities = _facilitySet.Select(x=>new FacilityModel(x)).ToList();
        
        return Ok(allFacilities);
    }

    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsers()
    {
        var allUsers = _userSet.Select(x => new UserModel(x)).ToList();

        return Ok(allUsers);
    }

    [HttpGet("get-user-facilities")]
    public async Task<IActionResult> GetUserFacilities(Guid userId)
    {
        var user = _userSet.FirstOrDefault(x => x.Id == userId);

        var allFacilities = _facilitySet
            .Select(x=>new UserFacilityModel()
            {
                Distance = x.Location.Distance(user.Location.PointOnSurface),
                Facility = new FacilityModel(x),
            })
            .ToList();

        return Ok(allFacilities);
    }

    [HttpGet("get-john-facilities")]
    public async Task<IActionResult> GetJohnFacilities()
    {
        var user = _userSet.FirstOrDefault(x => x.Id == JohnId);


        var distanceInMetres = 1000; // 1 km

        var allFacilities = _facilitySet
            .Select(x => new UserFacilityModel()
            {
                Distance = x.Location.Distance(user.Location.PointOnSurface) ,
                Facility = new FacilityModel(x),
            })
            .ToList();

        return Ok(allFacilities);
    }
    // var distanceInMetres = 1000; // 1 km
    // var entities = context.MyEntities.Select(x => x.Location.Distance(new Point(1,1)) <= distanceInMetres) .ToArray();


    [HttpGet("InitTestData")]
    public async Task<IActionResult> Test()
    {
        // Business Center BS5
        var lat = 59.92978310854247;
        var lon = 30.37464936331212;

        _facilitySet.RemoveRange(_facilitySet.Select(x => x).ToArray());

        _facilitySet.Add(new Facility()
        {
            Id = Guid.NewGuid(),
            Name = "BC5",
            Latitude = lat,
            Longitude = lon,
            Location = new Point(lon, lat)
        });

        // The Statue of Liberty
        lat = 40.689252;
        lon = -74.044635;

        _userSet.RemoveRange(_userSet.Select(x=>x).ToArray());

        _userSet.Add(new User()
        {
            Id = JohnId,
            Name = "John",
            Latitude = lat,
            Longitude = lon,
            Location = new Point(lon, lat)
        });

        _myDbContext.SaveChanges();

        return Ok("this is a test");
    }
    
}