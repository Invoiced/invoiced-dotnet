using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;

namespace Invoiced
{
    public class Connection
    {

    private static string jsonAccept = "application/json";
    private string apikey;
    private Environment env;

   public Connection(string apikey,Environment env) {
        this.apikey = apikey;
        this.env = env;

    }

    public Customer NewCustomer() {
        Customer customer = new Customer(this);
        return customer;
    }

    public Invoice NewInvoice() {
        Invoice invoice = new Invoice(this);
        return invoice;
    }

    public Transaction NewTransaction() {
        Transaction transaction = new Transaction(this);
        return transaction;
    }

    public Subscription  NewSubscription() {
        Subscription subscription = new Subscription(this);
        return subscription;
    }

    internal string post(string url, Dictionary<string,Object> queryParams, string jsonBody) {

        string uri = addQueryParmsToURI(url,queryParams);
        var response = executeRequest(HttpMethod.Post,uri, jsonBody);
        var responseText = processResponse(response);


        return responseText;

    }

    internal string patch(string url, string jsonBody) {

        var httpPatch = new HttpMethod("PATCH");
        var response = executeRequest(httpPatch,url, jsonBody);
        var responseText = processResponse(response);

        return responseText;
    }

    internal string get(string url, Dictionary<string,Object> queryParams) {
        
        string uri = addQueryParmsToURI(url,queryParams);
        var response = executeRequest(HttpMethod.Get,uri, null);
        Console.WriteLine(response);
        var responseText = processResponse(response);


        return responseText;
    }

   internal ListResponse getList(string url, Dictionary<string,Object> queryParams) {

        string uri = addQueryParmsToURI(url,queryParams);
        var response = executeRequest(HttpMethod.Post,uri, null);
        var responseText = processResponse(response);
        var linkString = HttpUtil.GetHeaderFirstValue(response,"Link");
        int totalCount = Int32.Parse(HttpUtil.GetHeaderFirstValue(response,"X-Total-Count"));
        var links = CommonUtil.parseLinks(linkString);

        var listReponse =  new ListResponse(responseText,links,totalCount);


       return listReponse;
    }

    internal void delete(string url) {

        var response = executeRequest(HttpMethod.Delete,url, null);
        var responseText = processResponse(response);

    }

    internal String baseUrl() {

		if (this.env == Environment.local) {
			return ConnectionURL.invoicedLocal;
		} else if (this.env == Environment.sandbox) {
            return ConnectionURL.invoicedSandbox;
        } else if (this.env == Environment.production) {
            return ConnectionURL.invoicedProduction;
        } else {
            throw new ConnException("Environment not recognized");
        }
    }

    private HttpResponseMessage executeRequest(HttpMethod method, string url, string jsonBody) {
        var request = new HttpRequestMessage(method,url);


        if (!string.IsNullOrEmpty(jsonBody)) {
        request.Content = new StringContent(jsonBody,Encoding.UTF8,jsonAccept);
        }
        request.Headers.Add("Authorization", "Basic " + HttpUtil.BasicAuth(apikey,""));
    
        var response = Client.httpClient.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
   
        return response;

    }


    private string processResponse(HttpResponseMessage response) {
        var responseText = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        
        if (!response.IsSuccessStatusCode) {
            throw handleApiError((int)response.StatusCode,responseText);
        }
        Console.WriteLine(responseText);
        return responseText;
    }

    private string addQueryParmsToURI(string uri, Dictionary<string,Object> queryParams ) {

         var builder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(builder.Query);

		if (queryParams != null) {
        foreach (var param in queryParams) {
            query.Add(param.Key.ToString(),param.Value.ToString());
        }

        builder.Query = query.ToString();
		}
        
        return builder.ToString();
    }

    	protected InvoicedException handleApiError(int responseCode, String responseBody) {
		if (responseCode == 401) {
			return new AuthException(responseBody);
		} else if (responseCode == 400) {
			return new InvalidRequestException(responseBody);
		} else if (responseCode == 429) {
			return new RateLimitException(responseBody);
		}  else if (responseCode >= 500) {
			return new InternalServerException(responseBody);
		} else {
            return new ApiException(responseBody);
        }
	}

}

 
}

   
    
