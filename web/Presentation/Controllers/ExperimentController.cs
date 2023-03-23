using System.Diagnostics;
using distance_post_gis.Infrastructure;
using Domain;
using Geolocation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure;
using NetTopologySuite.Geometries;
using NpgsqlTypes;

namespace Project.Application.Presentation;

[ApiController]
[Route("experiment")]
public class ExperimentController : ControllerBase
{
    private const int DistanceLimit = 1000000;

    private readonly MyDbContext _myDbContext;
    private readonly DbSet<Facility> _facilitySet;
    private readonly DbSet<User> _userSet;

    private readonly DbSet<AFacilityA> _facilityASet;
    private readonly DbSet<BFacilityB> _facilityBSet;
    private readonly DbSet<CFacilityC> _facilityCSet;

    private readonly Guid JohnId = Guid.Parse("ef303c96-50ab-4ab7-a89b-9b995f2b4039");

    public ExperimentController(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
        _userSet = _myDbContext.Set<User>();
        _facilitySet = _myDbContext.Set<Facility>();

        _facilityASet = _myDbContext.Set<AFacilityA>();
        _facilityBSet = _myDbContext.Set<BFacilityB>();
        _facilityCSet = _myDbContext.Set<CFacilityC>();
    }

    [HttpGet("POSTGIS-calculate-count")]
    public async Task<IActionResult> PostGisCalculateCount()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        var user = _userSet.FirstOrDefault(x => x.Id == JohnId);

        var count = _facilityASet
            .Where(x => x.Location.Distance(user.Location.PointOnSurface) < DistanceLimit)
            .Count();

        return Ok($"{count} - {sw.Elapsed}");
    }

    [HttpGet("EARTH-DIST-calculate-count")]
    public async Task<IActionResult> EarthDistanceCalculateCount()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        var user = _userSet.FirstOrDefault(x => x.Id == JohnId);

        var pp = GeoHelper.ToEarth(1, 1);

        //var ppp = _facilityBSet.Where(x=> MyDbContext.GetDistance());

        /*
        var count = _facilityBSet
            .Select(x=>MyDbContext.Abc123(555)).FirstOrDefault();
        */

        var count = _facilityBSet.Select(x => MyDbContext.GetDistance(new NpgsqlPoint(1, 1), new NpgsqlPoint(2, 2))).FirstOrDefault();

        return Ok($"{count} - {sw.Elapsed}");
    }

    // GetDistance


    [HttpGet("APP-calculate-count")]
    public async Task<IActionResult> AppCalculateCount()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        var user = _userSet.FirstOrDefault(x => x.Id == JohnId);

        var count = _facilityCSet
            .ToList()
            .Select(x => GeoCalculator.GetDistance(x.Latitude, x.Longitude, user.Latitude, user.Longitude, 1, DistanceUnit.Meters))
            .Count(x => x < DistanceLimit);

        return Ok($"{count} - {sw.Elapsed}");
    }
}