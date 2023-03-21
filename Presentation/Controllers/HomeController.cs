using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure;

namespace Project.Application.Presentation;

[ApiController]
[Route("home")]
public class HomeController : ControllerBase
{
    private readonly MyDbContext _myDbContext;
    private readonly DbSet<Facility> _facilitySet;
    private readonly DbSet<User> _userSet;

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
            Longitude = lon
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
            Longitude = lon
        });

        _myDbContext.SaveChanges();

        return Ok("this is a test");
    }

    [HttpGet("get-facilities")]
    public async Task<IActionResult> GetFacilities()
    {
        var allFacilities = _facilitySet.ToList();

        return Ok(allFacilities);
    }
    

    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok("this is a test");
    }
    
}