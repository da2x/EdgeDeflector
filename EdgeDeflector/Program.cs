/*
 * Copyright © 2017–2021 Daniel Aleksandersen
 * SPDX-License-Identifier: MIT
 * License-Filename: LICENSE
 */

using System;
using System.Collections.Specialized;
using System.Diagnostics;
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
                return true;
            }
            catch (UriFormatException)
            {
            }
            catch (ArgumentNullException)
            {
            }
            return false;
        }

        static bool IsHttpUri(string uri)
        {
            return uri.StartsWith("HTTPS://", StringComparison.OrdinalIgnoreCase) || uri.StartsWith("HTTP://", StringComparison.OrdinalIgnoreCase);
        }

        static bool IsMsEdgeUri(string uri)
        {
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
            string msedge_protocol_pattern = "^microsoft-edge:/*";

            Regex rgx = new Regex(msedge_protocol_pattern);
            string new_uri = rgx.Replace(uri, string.Empty);

            if (IsHttpUri(new_uri))
            {
                return new_uri;
            }

            // May be new-style Cortana URI - try and split out
            if (IsNonAuthoritativeWithUrlQueryParameter(uri))
            {
                string cortanaUri = GetURIFromCortanaLink(uri);
                if (IsHttpUri(cortanaUri))
                {
                    // Correctly form the new URI before returning
                    return cortanaUri;
                }
            }

            // defer fallback to web browser
            return "http://" + new_uri;
        }

        static void OpenUri(string uri)
        {
            if (!IsUri(uri) || !IsHttpUri(uri))
            {
                Environment.Exit(1);
            }

            ProcessStartInfo launcher = new ProcessStartInfo(uri)
            {
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
