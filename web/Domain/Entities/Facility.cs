using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Domain;

public class Facility
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }

    [Column(TypeName = "geography")]
    public Point Location { get; set; }
}

// PostGis Way
public class AFacilityA
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }

    [Column(TypeName = "geography")]
    public Point Location { get; set; }
}

// Earthdistance Way
public class BFacilityB
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
}

// Local calc Way
public class CFacilityC
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
}