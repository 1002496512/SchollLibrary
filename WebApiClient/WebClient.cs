using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiClient
{
    public class WebClient<T> : IWebClient<T>
    {
        HttpClient httpClient;
        UriBuilder uriBuilder;//

        public WebClient()
        {
            this.httpClient = new HttpClient();
            this.uriBuilder = new UriBuilder();
        }   

        public string Scheme
        {
            set
            {
                this.uriBuilder.Scheme = value;
            }
        }

        public string Host
        {
            set
            {
                this.uriBuilder.Host = value;
            }
        }
        public int Port
        {
            set
            {
                this.uriBuilder.Port = value;
            }
        }
        public string Path
        {
            set
            {
                this.uriBuilder.Path = value;
            }
        }

        public void AddParameter(string key, string value)
        {
            if(this.uriBuilder.Query != string.Empty)
            {
                this.uriBuilder.Query += "&" + key + "=" + value;
            }
            else
            {
                this.uriBuilder.Query += key + "=" + value;
            }
        }


        public T Get()
        {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Method  = HttpMethod.Get;
                    requestMessage.RequestUri = this.uriBuilder.Uri;
                    using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                    {
                        if (responseMessage.IsSuccessStatusCode==true)
                        {
                            string result = responseMessage.Content.ReadAsStringAsync().Result;
                            T data = JsonSerializer.Deserialize<T>(result);
                            return data;
                         }
                        else
                        {
                           return default(T);   
                        }
                    }
                }
        }

        public async Task<T> GetAsync()
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Get;
                requestMessage.RequestUri = this.uriBuilder.Uri;
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(requestMessage))
                {
                    if (responseMessage.IsSuccessStatusCode == true)
                    {
                        string result = await responseMessage.Content.ReadAsStringAsync();
                        T data = JsonSerializer.Deserialize<T>(result);
                        return data;
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
        }

        public bool Post(T data)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;
                requestMessage.RequestUri = this.uriBuilder.Uri;
                string jsonData = JsonSerializer.Serialize(data);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return responseMessage.IsSuccessStatusCode;
                }

            }
        }

        public bool Post(T data, FileStream file)
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Method = HttpMethod.Post;
                    requestMessage.RequestUri = this.uriBuilder.Uri;
                    MultipartFormDataContent multipartContent = new MultipartFormDataContent();
                    string jsonData = JsonSerializer.Serialize(data);
                    StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    multipartContent.Add(stringContent, "data");
                    StreamContent fileContent = new StreamContent(file);
                    multipartContent.Add(fileContent, "file", "fileName");
                    requestMessage.Content = multipartContent;
                    using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                    {
                        return responseMessage.IsSuccessStatusCode;
                    }
                }
            }

        public bool Post(T data, List<FileStream> files)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;
                requestMessage.RequestUri = this.uriBuilder.Uri;
                MultipartFormDataContent multipartContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multipartContent.Add(stringContent, "data");
                foreach (var file in files)
                {
                    StreamContent fileContent = new StreamContent(file);
                    multipartContent.Add(fileContent, "files", "fileName");
                }
                
                requestMessage.Content = multipartContent;
                using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }

        public async Task<bool> PostAsync(T data)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;
                requestMessage.RequestUri = this.uriBuilder.Uri;
                string jsonData = JsonSerializer.Serialize(data);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(requestMessage))
                {
                    return responseMessage.IsSuccessStatusCode;
                }

            }
        }
        public async Task<bool> PostAsync(T data, FileStream file)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;
                requestMessage.RequestUri = this.uriBuilder.Uri;
                MultipartFormDataContent multipartContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multipartContent.Add(stringContent, "data");
                StreamContent fileContent = new StreamContent(file);
                multipartContent.Add(fileContent, "file", "fileName");
                requestMessage.Content = multipartContent;
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(requestMessage))
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }
        public async Task<bool> PostAsync(T data, List<FileStream> files)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;
                requestMessage.RequestUri = this.uriBuilder.Uri;
                MultipartFormDataContent multipartContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multipartContent.Add(stringContent, "data");
                foreach (var file in files)
                {
                    StreamContent fileContent = new StreamContent(file);
                    multipartContent.Add(fileContent, "files", "fileName");
                }

                requestMessage.Content = multipartContent;
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(requestMessage))
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }
    }
}
