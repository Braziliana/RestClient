using System;
using System.Net.Http;

namespace RestApiClient
{
    public enum RequestMethod
    {
        Get,
        Post,
        Delete,
        Put
    }

    public class RestApiClient
    {
        private string _url;
        private HttpRequestMessage _httpRequestMessage;
        private RequestMethod _requestMethod;
        public RestApiClient(string url, RequestMethod method = RequestMethod.Get)
        {
            this._url = url;
            this._requestMethod = method;
            this._httpRequestMessage = new HttpRequestMessage();
            SetMethod();
        }

        public RestApiClient AddSegment(string segment)
        {
            if (segment.Length == 0)
            {
                return this;
            }

            if (segment[0] != '/')
            {
                segment = '/' + segment;
            }

            this._url += segment;

            return this;
        }

        public RestApiClient AddSegment<TValue>(string segment, TValue value)
        {
            if (segment == null)
                segment = "";
            return AddSegment(segment + value.ToString());
        }

        public RestApiClient AddSegment<TValue>(TValue value)
        {
            return AddSegment("/" + value.ToString());
        }

        public RestApiClient AddParameter<TParam>(string paramName, TParam parameter)
        {
            if(this._requestMethod == RequestMethod.Post)
            {

            }
            else
            {
                AddParameterGet(paramName, parameter);
            }
            return this;
        }

        private void AddParameterGet<TParam>(string paramName, TParam parameter)
        {
            if (!this._url.EndsWith("?"))
            {
                this._url += '?';
            }
            this._url += '&' + paramName + "=" + parameter.ToString();
        }

        public RestApiClient AddHeader<TValue>(string header, TValue value)
        {

            return this;
        }

        public RestApiClient Authenticator()
        {

            return this;
        }

        private void SetMethod()
        {
            switch (this._requestMethod)
            {
                case RequestMethod.Get:
                    this._httpRequestMessage.Method = HttpMethod.Get;
                    break;

                case RequestMethod.Post:
                    this._httpRequestMessage.Method = HttpMethod.Post;
                    break;

                case RequestMethod.Delete:
                    this._httpRequestMessage.Method = HttpMethod.Get;
                    break;

                case RequestMethod.Put:
                    this._httpRequestMessage.Method = HttpMethod.Get;
                    break;
            }
        }
    }
}
