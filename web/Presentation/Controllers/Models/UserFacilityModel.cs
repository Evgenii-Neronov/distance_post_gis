using Domain;

namespace distance_post_gis.Presentation.Controllers.Models;

public class FacilityModel
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }

    public FacilityModel(Facility facility)
    {
        Id = facility.Id;
        Latitude = facility.Latitude;
        Longitude = facility.Longitude;
        Name = facility.Name;
    }

    public FacilityModel(AFacilityA facility)
    {
        Id = facility.Id;
        Latitude = facility.Latitude;
        Longitude = facility.Longitude;
        Name = facility.Name;
    }

    public FacilityModel(BFacilityB facility)
    {
        Id = facility.Id;
        Latitude = facility.Latitude;
        Longitude = facility.Longitude;
        Name = facility.Name;
    }

    public FacilityModel(CFacilityC facility)
    {
        Id = facility.Id;
        Latitude = facility.Latitude;
        Longitude = facility.Longitude;
        Name = facility.Name;
    }
}

public class UserModel
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }

    public UserModel(User user)
    {
        Id = user.Id;
        Latitude = user.Latitude;
        Longitude = user.Longitude;
        Name = user.Name;
    }
}

public class UserFacilityModel
{
    public double Distance { get; set; }
    public FacilityModel Facility { get; set; }
}
