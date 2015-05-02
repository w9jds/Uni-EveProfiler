﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace EveProfiler.Core
{
    public class HttpHelper
    {
        private static void setUserAgent(ref HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "EveProfiler/Universal Application/by Chingy Chonga");
        }

        public static void get(string pathUri, Dictionary<string, string> queryParams, Action<HttpResponseMessage> response)
        {
            HttpClient getXml = new HttpClient();

            getXml.Timeout = TimeSpan.FromSeconds(20);
            getXml.BaseAddress = new Uri("https://api.eveonline.com");
            setUserAgent(ref getXml);

            if (queryParams != null)
            {
                string query = string.Join("&", queryParams
                    .Select(x => $"{x.Key}={x.Value}").ToArray());
                getXml.GetAsync($"{pathUri}?{query}")
                    .ContinueWith(t => response(t.Result));
            }
            else
                getXml.GetAsync(pathUri).ContinueWith(t => response(t.Result));
        }

        public static void get(string pathUri, Action<HttpResponseMessage> aResponse)
        {
            HttpClient getXml = new HttpClient();

            getXml.Timeout = TimeSpan.FromSeconds(20);
            getXml.BaseAddress = new Uri("https://api.eveonline.com");
            setUserAgent(ref getXml);

            getXml.GetAsync(pathUri).ContinueWith(t => aResponse(t.Result));
        }

        public static void get(string pathUri, string ID, int size, Action<HttpResponseMessage> aResponse)
        {

            HttpClient getBytes = new HttpClient();

            getBytes.Timeout = TimeSpan.FromSeconds(20);
            getBytes.BaseAddress = new Uri("http://image.eveonline.com");
            setUserAgent(ref getBytes);

            if (pathUri == @"/Character/")
            {
                pathUri += $"{ID}_{size}.jpg";
            }
            else
            {
                pathUri += $"{ID}_{size}.png";
            }

            getBytes.GetAsync(pathUri).ContinueWith(t => aResponse(t.Result));
        }
    }
}