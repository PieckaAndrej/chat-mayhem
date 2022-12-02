namespace PersonServiceClientDesktop.Servicelayer
{
    public class ServiceConnection : IServiceConnection
    {
        public ServiceConnection(String baseUrl)
        {
            HttpEnabler = new HttpClient();
            BaseUrl = baseUrl;
            UseUrl = BaseUrl;
        }
        public HttpClient HttpEnabler { private get; init;}
        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }
        public async Task<HttpResponseMessage?> Get()
        {
            HttpResponseMessage? response = null;
            if (UseUrl != null)
            {
                response = await HttpEnabler.GetAsync(UseUrl);
            }
            return response;
        }
        public async Task<HttpResponseMessage?> Post(StringContent postJson)
        {
            HttpResponseMessage? response = null;
            if (UseUrl != null)
            {
                response = await HttpEnabler.PostAsync(UseUrl, postJson);
            }
            return response;
        }
        public async Task<HttpResponseMessage?> Put(StringContent putJson)
        {
            HttpResponseMessage? response = null;
            if (UseUrl != null)
            {
                response = await HttpEnabler.PutAsync(UseUrl, putJson);
            }
            return response;
        }
        public async Task<HttpResponseMessage?> Delete()
        {
            HttpResponseMessage? response = null;
            if (UseUrl != null)
            {
                response = await HttpEnabler.DeleteAsync(UseUrl);
            }
            return response;
        }
    }
}