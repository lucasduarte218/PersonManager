namespace PersonManager.Domain.Entities
{
    public class RequestResponseLog
    {
        public long Id { get; set; }
        public string Path { get; set; } = default!;
        public string Method { get; set; } = default!;
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
        public string? IpAddress { get; set; }
        public string? LogType { get; set; }
        public string? RequestHeaders { get; set; }
        public string? ResponseHeaders { get; set; }
    }
}