CREATE EXTENSION cube;
CREATE EXTENSION earthdistance;

CREATE INDEX facility_location_idx
ON "public"."facility_view"
USING GIST (ll_to_earth(latitude, longitude));

SELECT ST_Distance(location, ST_GeographyFromText('POINT(-74.044635 40.689252)')) FROM public.facility;
SELECT earth_distance(ll_to_earth(40.689252,-74.044635), ll_to_earth(latitude,longitude)) FROM public.facility;