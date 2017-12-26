/*
 * Copyright © 2017 Daniel Aleksandersen
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
        static bool IsElevated()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void ElevatePermissions()
        {
            ProcessStartInfo rerun = new ProcessStartInfo()
            {
                FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                UseShellExecute = true,
                Verb = "runas"
            };
            Process.Start(rerun);
        }

        private static void RegisterProtocolHandler()
        {
            RegistryKey uriclass_key = Registry.ClassesRoot.OpenSubKey("EdgeUriDeflector", true);
            if (uriclass_key == null)
            {
                uriclass_key = Registry.ClassesRoot.CreateSubKey("EdgeUriDeflector", true);
            }

            uriclass_key.SetValue(string.Empty, "URL: Microsoft Edge Protocol Deflector");

            RegistryKey icon_key = uriclass_key.OpenSubKey("DefaultIcon", true);
            if (icon_key == null)
            {
                icon_key = uriclass_key.CreateSubKey("DefaultIcon");
            }

            string exec_path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            icon_key.SetValue(string.Empty, exec_path + ",0");
            icon_key.Close();

            RegistryKey shellcmd_key = uriclass_key.OpenSubKey(@"shell\open\command", true);
            if (shellcmd_key == null)
            {
                shellcmd_key = uriclass_key.CreateSubKey(@"shell\open\command");
            }

            shellcmd_key.SetValue(string.Empty, exec_path + " \"%1\"");
            shellcmd_key.Close();

            uriclass_key.SetValue("URL Protocol", string.Empty);
            
            uriclass_key.Close();

            RegistryKey software_key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\EdgeUriDeflector", true);
            if (software_key == null)
            {
                software_key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Clients\EdgeUriDeflector", true);
            }

            RegistryKey capability_key = software_key.OpenSubKey("Capabilities", true);
            if (capability_key == null)
            {
                capability_key = software_key.CreateSubKey("Capabilities", true);
            }
            
            capability_key.SetValue("ApplicationDescription", "Open web links normally forced to open in Microsoft Edge in your default web browser.");
            capability_key.SetValue("ApplicationName", "EdgeDeflector");

            RegistryKey urlass_key = capability_key.OpenSubKey("UrlAssociations", true);
            if (urlass_key == null)
            {
                urlass_key = capability_key.CreateSubKey("UrlAssociations", true);
            }

            urlass_key.SetValue("microsoft-edge", "EdgeUriDeflector");
            urlass_key.Close();

            capability_key.Close();
            software_key.Close();

            RegistryKey registeredapps_key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\RegisteredApplications", true);
            registeredapps_key.SetValue("EdgeUriDeflector", @"SOFTWARE\Clients\EdgeUriDeflector\Capabilities");
            registeredapps_key.Close();
        }

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
            uri = uri.ToLower();
            return uri.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || uri.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }

        static bool IsMsEdgeUri(string uri)
        {
            uri = uri.ToLower();
            return uri.StartsWith("microsoft-edge:", StringComparison.OrdinalIgnoreCase) && !uri.Contains(" ");
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

            // Install when running without argument
            else if (args.Length == 0)
            {
                if (!IsElevated())
                {
                    ElevatePermissions();
                }
                else
                {
                    RegisterProtocolHandler();
                }
            }
        }
    }
}
