namespace Sora.Hospital.Infrastructure.VirtualObject
{
    /// <summary>
    /// JSend Standar, using for JSon API  response
    /// </summary>
    public class JSonResponse
    {
        public static class Status
        {
            public static string Success = "success";
            public static string Fail = "fail";
            public static string Error = "error";
        }

        public string status { get; set; }
        public object data { get; set; }
        public string message { get; set; }
    }
}