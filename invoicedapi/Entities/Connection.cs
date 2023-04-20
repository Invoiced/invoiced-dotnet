using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Invoiced
{
    public class Connection
    {
        private const string JsonAccept = "application/json";
        private readonly string _apikey;

        private HttpClient _client;
        private readonly Environment _env;

        public Connection(string apikey, Environment env)
        {
            _apikey = apikey;
            _env = env;
            _client = Client.httpClient;
        }

        public void TestClient(HttpClient testClient)
        {
            if (_env == Environment.test) _client = testClient;
        }

        public Charge NewCharge()
        {
            return new Charge(this);
        }

        public Coupon NewCoupon()
        {
            return new Coupon(this);
        }

        public CreditBalanceAdjustment NewCreditBalanceAdjustment()
        {
            return new CreditBalanceAdjustment(this);
        }

        public CreditNote NewCreditNote()
        {
            return new CreditNote(this);
        }

        public Customer NewCustomer()
        {
            return new Customer(this);
        }

        public Estimate NewEstimate()
        {
            return new Estimate(this);
        }

        public Event NewEvent()
        {
            return new Event(this);
        }

        public File NewFile()
        {
            return new File(this);
        }

        public Invoice NewInvoice()
        {
            return new Invoice(this);
        }

        public Item NewItem()
        {
            return new Item(this);
        }

        public Note NewNote()
        {
            return new Note(this);
        }

        public Payment NewPayment()
        {
            return new Payment(this);
        }

        public Plan NewPlan()
        {
            return new Plan(this);
        }

        public Refund NewRefund()
        {
            return new Refund(this);
        }

        public Subscription NewSubscription()
        {
            return new Subscription(this);
        }

        public Task NewTask()
        {
            return new Task(this);
        }

        public TaxRate NewTaxRate()
        {
            return new TaxRate(this);
        }

        internal string Post(string endpoint, Dictionary<string, object> queryParams, string jsonBody)
        {
            var uri = AddQueryParamsToUri(BaseUrl() + endpoint, queryParams);
            var response = ExecuteRequest(HttpMethod.Post, uri, jsonBody);
            return ProcessResponse(response);
        }
        internal async Task<string> PostAsync(string endpoint, Dictionary<string, object> queryParams, string jsonBody, CancellationToken ct = default)
        {
            var uri = AddQueryParamsToUri(BaseUrl() + endpoint, queryParams);
            var response = await ExecuteRequestAsync(HttpMethod.Post, uri, jsonBody, ct);
            return await ProcessResponseAsync(response);
        }

        internal string Patch(string endpoint, string jsonBody)
        {
            var httpPatch = new HttpMethod("PATCH");
            var response = ExecuteRequest(httpPatch, BaseUrl() + endpoint, jsonBody);
            return ProcessResponse(response);
        }
        internal async Task<string> PatchAsync(string endpoint, string jsonBody, CancellationToken ct = default)
        {
            var httpPatch = new HttpMethod("PATCH");
            var response = await ExecuteRequestAsync(httpPatch, BaseUrl() + endpoint, jsonBody, ct);
            return await ProcessResponseAsync(response);
        }

        internal string Get(string endpoint, Dictionary<string, object> queryParams)
        {
            var uri = AddQueryParamsToUri(BaseUrl() + endpoint, queryParams);
            var response = ExecuteRequest(HttpMethod.Get, uri, null);

            return ProcessResponse(response);
        }
        internal async Task<string> GetAsync(string endpoint, Dictionary<string, object> queryParams, CancellationToken ct = default)
        {
            var uri = AddQueryParamsToUri(BaseUrl() + endpoint, queryParams);
            var response = await ExecuteRequestAsync(HttpMethod.Get, uri, null,ct);

            return await ProcessResponseAsync(response);
        }

        internal ListResponse GetList(string url, Dictionary<string, object> queryParams)
        {
            var uri = AddQueryParamsToUri(url, queryParams);
            var response = ExecuteRequest(HttpMethod.Get, uri, null);
            var responseText = ProcessResponse(response);
            var linkString = HttpUtil.GetHeaderFirstValue(response, "Link");
            var totalCount = int.Parse(HttpUtil.GetHeaderFirstValue(response, "X-Total-Count"));

            var links = CommonUtil.ParseLinks(linkString);

            return new ListResponse(responseText, links, totalCount);
        }
        internal async Task<ListResponse> GetListAsync(string url, Dictionary<string, object> queryParams, CancellationToken ct = default)
        {
            var uri = AddQueryParamsToUri(url, queryParams);
            var response = await ExecuteRequestAsync(HttpMethod.Get, uri, null, ct);
            var responseText = await ProcessResponseAsync(response);
            var linkString = HttpUtil.GetHeaderFirstValue(response, "Link");
            var totalCount = int.Parse(HttpUtil.GetHeaderFirstValue(response, "X-Total-Count"));

            var links = CommonUtil.ParseLinks(linkString);

            return new ListResponse(responseText, links, totalCount);
        }

        internal void Delete(string endpoint)
        {
            var response = ExecuteRequest(HttpMethod.Delete, BaseUrl() + endpoint, null);
            ProcessResponse(response);
        }
        internal async System.Threading.Tasks.Task DeleteAsync(string endpoint, CancellationToken ct = default)
        {
            var response = await ExecuteRequestAsync(HttpMethod.Delete, BaseUrl() + endpoint, null, ct);
            await ProcessResponseAsync(response);
        }

        internal string BaseUrl()
        {
            if (_env == Environment.local)
                return ConnectionURL.invoicedLocal;
            if (_env == Environment.sandbox)
                return ConnectionURL.invoicedSandbox;
            if (_env == Environment.production)
                return ConnectionURL.invoicedProduction;
            if (_env == Environment.test)
                return ConnectionURL.invoicedTest;
            throw new ConnException("Environment not recognized");
        }

        private HttpResponseMessage ExecuteRequest(HttpMethod method, string url, string jsonBody)
        {
            var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrEmpty(jsonBody))
                request.Content = new StringContent(jsonBody, Encoding.UTF8, JsonAccept);
            request.Headers.Add("Authorization", "Basic " + HttpUtil.BasicAuth(_apikey, ""));
            request.Headers.Add("User-Agent", "Invoiced .NET/" + GetVersion());

            return _client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private Task<HttpResponseMessage> ExecuteRequestAsync(HttpMethod method, string url, string jsonBody, CancellationToken ct = default)
        {
            var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrEmpty(jsonBody))
                request.Content = new StringContent(jsonBody, Encoding.UTF8, JsonAccept);
            request.Headers.Add("Authorization", "Basic " + HttpUtil.BasicAuth(_apikey, ""));
            request.Headers.Add("User-Agent", "Invoiced .NET/" + GetVersion());

            return _client.SendAsync(request,ct);
        }

        private string ProcessResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent) return "";
            var responseText = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode) throw HandleApiError((int) response.StatusCode, responseText);

            return responseText;
        }
        private async Task<string> ProcessResponseAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent) return "";
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) throw HandleApiError((int) response.StatusCode, responseText);

            return responseText;
        }
        
        public string GetVersion()
        {
            var version = GetType().Assembly.GetName().Version;
            
            return version != null ? version.ToString() : "";
        }

        private string AddQueryParamsToUri(string uri, Dictionary<string, object> queryParams)
        {
            var builder = new UriBuilder(uri);

            if (queryParams != null)
            {
                var querySegments = new List<string>();
                foreach (var param in queryParams)
                    querySegments.Add(WebUtility.UrlEncode(param.Key) + "=" +
                                      WebUtility.UrlEncode(param.Value.ToString()));
                builder.Query = string.Join("&", querySegments);
            }

            return builder.ToString();
        }

        private InvoicedException HandleApiError(int responseCode, string responseBody)
        {
            if (responseCode == 401)
                return new AuthException(responseBody);
            if (responseCode == 400)
                return new InvalidRequestException(responseBody);
            if (responseCode == 429)
                return new RateLimitException(responseBody);
            if (responseCode >= 500)
                return new InternalServerException(responseBody);
            return new ApiException(responseBody);
        }
    }
}