/*
 * Copyright © 2017–2021 Daniel Aleksandersen
 * Copyright © 2021–present Hadi Alqattan
 * SPDX-License-Identifier: MIT
 * License-Filename: LICENSE
 */

using Microsoft.Win32;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;

namespace EdgeDeflector
{
    class Program
    {
        static bool IsUri(string uristring)
        {
            try
            {
                Uri uri = new Uri(uristring);
                return uri.IsWellFormedOriginalString();
            }
            catch (System.UriFormatException)
            {
                return false;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        static bool IsHttpUri(string uri)
        {
            uri = uri.ToUpperInvariant();
            return uri.StartsWith("HTTP://", StringComparison.OrdinalIgnoreCase) || uri.StartsWith("HTTPS://", StringComparison.OrdinalIgnoreCase);
        }

        static bool IsMsEdgeUri(string uri)
        {
            uri = uri.ToUpperInvariant();
            return uri.StartsWith("MICROSOFT-EDGE:", StringComparison.OrdinalIgnoreCase) && !uri.Contains(" ");
        }

        static bool IsNonAuthoritativeWithUrlQueryParameter(string uri)
        {
            return uri.Contains("microsoft-edge:?") && uri.Contains("&url=");
        }

        static string GetURIFromCortanaLink(string uri)
        {
            NameValueCollection queryCollection = HttpUtility.ParseQueryString(uri);
            return queryCollection["url"];
        }

        static string RewriteMsEdgeUriSchema(string uri)
        {
            string search_engine_name = "Google";  // WIP, hardcoded.
            string msedge_protocol_pattern = "^microsoft-edge:/*";

            Regex rgx = new Regex(msedge_protocol_pattern);
            string new_uri = rgx.Replace(uri, string.Empty);

            if (IsHttpUri(new_uri))
            {
                return ReplaceSearchEngine(new_uri, search_engine_name);
            }

            // May be new-style Cortana URI - try and split out
            if (IsNonAuthoritativeWithUrlQueryParameter(uri))
            {
                string cortanaUri = GetURIFromCortanaLink(uri);
                if (IsHttpUri(cortanaUri))
                {
                    // Correctly form the new URI before returning
                    return ReplaceSearchEngine(cortanaUri, search_engine_name);
                }
            }

            // defer fallback to web browser
            return "http://" + ReplaceSearchEngine(new_uri, search_engine_name);
        }

        static string ReplaceQuery(string uri, string newQuery)
        {
            return uri.Replace("bing.com/search?q=", newQuery);
        }

        static string ReplaceSearchEngine(string uri, string searchEngineName)
        {
            int index = uri.IndexOf("&");
            if (index > 0) { 
                uri = uri.Substring(0, index); 
            }

            switch (searchEngineName)
            {
                case "Google":  // Hardcoded on `RewriteMsEdgeUriSchema`.
                    return ReplaceQuery(uri, "google.com/search?q=");
                case "DuckDuckGo":  // WIP.
                    return ReplaceQuery(uri, "duckduckgo.com/?q=");
                default:
                    return uri;
            }
        }

        static void OpenUri(string uri)
        {
            if (!IsUri(uri) || !IsHttpUri(uri))
            {
                Environment.Exit(1);
            }

            ProcessStartInfo launcher = new ProcessStartInfo()
            {
                FileName = uri,
                UseShellExecute = true
            };
            Process.Start(launcher);
        }

        static void Main(string[] args)
        {
            // Assume argument is URI
            if (args.Length == 1 && IsMsEdgeUri(args[0]))
            {
                string uri = RewriteMsEdgeUriSchema(args[0]);
                OpenUri(uri);
            }
        }
    }
}
