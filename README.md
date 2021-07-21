# Xero-NetStandard-custom-connections-starter
This is a starter app with the code to perform OAuth 2.0 authentication flow for the `client_credentials` grant.

Custom Connections are a Xero [premium option](https://developer.xero.com/documentation/oauth2/custom-connections) for building M2M integrations to a single Xero Organisation.

## Create a Xero App
To obtain your API keys, follow these steps and create a Xero app

* Login or create a [free Xero user account](https://www.xero.com/us/signup/api/)
* Login to [Xero developer center](https://developer.xero.com/app/manage)
* Click "New App" link with the "Custom connection" selection
* Agree to terms and condition and click "Create App".
* Complete the steps for Custom Connections
* Click "Generate a secret" button
* Click the "Save" button. You secret is now hidden.

## Getting Started
```bash
mv .env.sample .env
```

### Running the app
```
dotnet run
```

There is one main file.
- Program.cs

Note that the `var xeroTenantId = "";` must still be passed to the function but will not be evaluated under a XeroClient that is working with a Custom Connection.

```csharp
XeroConfiguration XeroConfig = new XeroConfiguration
  {
    ClientId = System.Environment.GetEnvironmentVariable("CLIENT_ID"),
    ClientSecret = System.Environment.GetEnvironmentVariable("CLIENT_SECRET"),
  };

  var client = new XeroClient(XeroConfig);
  var xeroToken = await client.RequestClientCredentialsTokenAsync();

  try {
    var apiInstance = new AccountingApi();
    var ifModifiedSince = DateTime.Parse("2000-02-06T12:17:43.202-08:00");
    var where = "Status==\"ACTIVE\"";
    var xeroTenantId = "";
    var result = await apiInstance.GetAccountsAsync(xeroToken.AccessToken, xeroTenantId, ifModifiedSince, where, null);
    return result.ToJson();
  }
  catch (Exception e)
  {
    Console.WriteLine("Exception when calling apiInstance.GetInvoice: " + e.Message );
    return e.ToString();
  }
```

## License

This software is published under the [MIT License](http://en.wikipedia.org/wiki/MIT_License).

	Copyright (c) 2020 Xero Limited

	Permission is hereby granted, free of charge, to any person
	obtaining a copy of this software and associated documentation
	files (the "Software"), to deal in the Software without
	restriction, including without limitation the rights to use,
	copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the
	Software is furnished to do so, subject to the following
	conditions:

	The above copyright notice and this permission notice shall be
	included in all copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
	OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
	NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
	HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
	WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
	FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
	OTHER DEALINGS IN THE SOFTWARE.
