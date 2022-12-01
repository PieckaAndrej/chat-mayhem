namespace PersonServiceClientDesktop.Servicelayer
{
    public interface IServiceConnection
    {
        public string? BaseUrl{ get; init;}
        public string? UseUrl { get; set; }
        Task<HttpResponseMessage?> Get();
        Task<HttpResponseMessage?> Post(StringContent postJson);
        Task<HttpResponseMessage?> Put(StringContent putJson);
        Task<HttpResponseMessage?> Delete();
    }
}