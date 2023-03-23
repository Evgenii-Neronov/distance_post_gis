using distance_post_gis.Presentation.Controllers.Models;
using Domain;
using Geolocation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure;
using NetTopologySuite.Geometries;

namespace Project.Application.Presentation;

[ApiController]
[Route("home")]
public class HomeController : ControllerBase
{
    private const int DistanceLimit = 1000000;

    private readonly MyDbContext _myDbContext;
    private readonly DbSet<Facility> _facilitySet;
    private readonly DbSet<User> _userSet;

    private readonly DbSet<AFacilityA> _facilityASet;
    private readonly DbSet<BFacilityB> _facilityBSet;
    private readonly DbSet<CFacilityC> _facilityCSet;

    private readonly Guid JohnId = Guid.Parse("ef303c96-50ab-4ab7-a89b-9b995f2b4039");

    public HomeController(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
        _userSet = _myDbContext.Set<User>();
        _facilitySet = _myDbContext.Set<Facility>();

        _facilityASet = _myDbContext.Set<AFacilityA>();
        _facilityBSet = _myDbContext.Set<BFacilityB>();
        _facilityCSet = _myDbContext.Set<CFacilityC>();
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

    [HttpGet("get-john-facilities-local")]
    public async Task<IActionResult> GetJohnFacilitiesLocal()
    {
        var user = _userSet.FirstOrDefault(x => x.Id == JohnId);
        var distanceInMetres = 1000; // 1 km

        var allFacilities = _facilitySet
            .Select(x => new UserFacilityModel()
            {
                Distance = GeoCalculator.GetDistance(x.Latitude, x.Longitude, user.Latitude, user.Longitude, 1, DistanceUnit.Meters),
                Facility = new FacilityModel(x),
            })
            .ToList();

        return Ok(allFacilities);
    }

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