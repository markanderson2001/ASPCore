namespace TheWorld.Services
{
    public class GeoCoordsResult
    {
        public bool Success { get; set; } //succesful or not
        public string Message { get; set; } //usualy on false success
        public double Longitude { get; set; }
        public double Latitude { get; set; }

    }
}