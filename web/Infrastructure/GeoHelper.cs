namespace distance_post_gis.Infrastructure;

public static class GeoHelper
{
    public static double Radius = 6371;

    public static double ToEarth(double latitude, double longitude)
    {
        return Radius * Math.Cos(latitude) * Math.Cos(longitude);
    }
}
