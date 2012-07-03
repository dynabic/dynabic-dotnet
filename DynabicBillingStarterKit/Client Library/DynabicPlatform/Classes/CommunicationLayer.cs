#pragma warning disable 1591

using System;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using DynabicPlatform.Exceptions;
using DynabicPlatform.Interfaces;
using Microsoft.Http;
using Newtonsoft.Json;


namespace DynabicPlatform.Classes
{
    public class CommunicationLayer
    {
        protected HttpClient _httpClient;

        public String ApiVersion = "1";

        protected Configuration Configuration;

        public IDynabicEnvironment Environment
        {
            get { return Configuration.Environment; }
        }

        public String PublicKey
        {
            get { return Configuration.PublicKey; }
        }

        public String PrivateKey
        {
            get { return Configuration.PrivateKey; }
        }

        public CommunicationLayer(Configuration configuration)
        {
            this.Configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.TransportSettings.ReadWriteTimeout = TimeSpan.FromHours(1);
            if ((string.IsNullOrEmpty(configuration.PublicKey) || string.IsNullOrEmpty(configuration.PrivateKey))
                && configuration.AuthenticationCookie != null)
            {
                _httpClient.TransportSettings.Cookies = new System.Net.CookieContainer();
                _httpClient.TransportSettings.Cookies.Add(configuration.AuthenticationCookie);
            }
        }

        private HttpContent EmptyContent
        {
            get { return HttpContent.Create(" "); }
        }

        private HttpResponseMessage ExecuteMethod(Func<HttpResponseMessage> method)
        {
            var response = method();
            /*if (response.StatusCode == HttpStatusCode.Forbidden)
                response = method();*/
            return response;
        }

        public TResponse Get<TResponse>(string url)
        {
            var response = ExecuteMethod(() => { return _httpClient.Get(SignUrl(url)); });
            ThrowExceptionIfErrorStatusCode(response.StatusCode, string.Empty);

            if (response.Content.ContentType.ToLower().Contains(ContentFormat.JSON))
            {
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsString());
            }
            else
            {
                return response.Content.ReadAsDataContract<TResponse>();
            }
        }

        public TResponse Post<TRequest, TResponse>(string url, TRequest request)
        {
            var response = ExecuteMethod(() =>
            {
                var content = HttpContentExtensions.CreateDataContract<TRequest>(request);
                SignHttpContent(content);
                return _httpClient.Post(url, content);
            });
            ThrowExceptionIfErrorStatusCode(response.StatusCode, string.Empty);

            if (response.Content.ContentType.ToLower().Contains(ContentFormat.JSON))
            {
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsString());
            }
            else
            {
                return response.Content.ReadAsDataContract<TResponse>();
            }
        }

        public void Post<TRequest>(string url, TRequest request)
        {
            var response = ExecuteMethod(() =>
            {
                var content = HttpContentExtensions.CreateDataContract<TRequest>(request);
                SignHttpContent(content);
                return _httpClient.Post(url, content);
            });
            ThrowExceptionIfErrorStatusCode(response.StatusCode, string.Empty);
        }

        public TResponse Put<TRequest, TResponse>(string url, TRequest request)
        {
            var response = ExecuteMethod(() =>
            {
                var content = HttpContentExtensions.CreateDataContract<TRequest>(request);
                SignHttpContent(content);
                return _httpClient.Put(url, content);
            });
            ThrowExceptionIfErrorStatusCode(response.StatusCode, string.Empty);

            if (response.Content.ContentType.ToLower().Contains(ContentFormat.JSON))
            {
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsString());
            }
            else
            {
                return response.Content.ReadAsDataContract<TResponse>();
            }
        }

        public TResponse Put<TResponse>(string url)
        {
            var response = ExecuteMethod(() =>
            {
                return _httpClient.Put(SignUrl(url), this.EmptyContent);
            });
            ThrowExceptionIfErrorStatusCode(response.StatusCode, string.Empty);

            if (response.Content.ContentType.ToLower().Contains(ContentFormat.JSON))
            {
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsString());
            }
            else
            {
                return response.Content.ReadAsDataContract<TResponse>();
            }
        }

        public HttpResponseMessage Put(string url)
        {
            var response = ExecuteMethod(() =>
            {
                return _httpClient.Put(SignUrl(url), this.EmptyContent);
            });
            ThrowExceptionIfErrorStatusCode(response.StatusCode, string.Empty);
            return response;
        }

        public void Put<TRequest>(string url, TRequest request)
        {
            var response = ExecuteMethod(() =>
            {
                var content = HttpContentExtensions.CreateDataContract<TRequest>(request);
                SignHttpContent(content);
                return _httpClient.Put(url, content);
            });
            ThrowExceptionIfErrorStatusCode(response.StatusCode, string.Empty);
        }

        public HttpStatusCode Delete(string url)
        {
            var response = ExecuteMethod(() =>
            {
                return _httpClient.Delete(SignUrl(url));
            });
            ThrowExceptionIfErrorStatusCode(response.StatusCode, string.Empty);
            return response.StatusCode;
        }

        public static void ThrowExceptionIfErrorStatusCode(HttpStatusCode httpStatusCode, String message)
        {
            if (httpStatusCode != HttpStatusCode.OK && httpStatusCode != HttpStatusCode.Created)
            {
                switch (httpStatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException();
                    case HttpStatusCode.Forbidden:
                        throw new AuthorizationException(message);
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException();
                    case HttpStatusCode.InternalServerError:
                        throw new ServerException();
                    case HttpStatusCode.ServiceUnavailable:
                        throw new DownForMaintenanceException();
                    case (HttpStatusCode)426:
                        throw new UpgradeRequiredException();
                    default:
                        var exception = new UnexpectedException();
                        exception.Source = "Unexpected HTTP_RESPONSE " + httpStatusCode;
                        throw exception;
                }
            }
        }

        protected void SignHttpContent(HttpContent content)
        {
            if (string.IsNullOrEmpty(this.PublicKey) || string.IsNullOrEmpty(this.PrivateKey))
            {
                return;
            }
            content.LoadIntoBuffer();
            var stringContent = Encoding.UTF8.GetString(content.ReadAsByteArray());
            var signature = Crypto.SignString(stringContent, PrivateKey);
            _httpClient.DefaultHeaders[Crypto.SIGNATURE_PARAM] = signature;
            _httpClient.DefaultHeaders[Crypto.CLIENT_KEY_PARAM] = PublicKey;
        }

        protected string SignUrl(string url)
        {
            if (string.IsNullOrEmpty(this.PublicKey) || string.IsNullOrEmpty(this.PrivateKey))
            {
                return url;
            }
            return Crypto.SignUrl(url, PublicKey, PrivateKey);
        }
    }
}
