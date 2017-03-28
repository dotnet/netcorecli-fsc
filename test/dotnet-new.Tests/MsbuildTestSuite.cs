using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.DotNet.Tools.Test.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using FluentAssertions;
using static System.Environment;

namespace NetcoreCliFsc.Tests
{
    public static class MsbuildTestSuite
    {
        public static string RestoreSourcesArgs(IEnumerable<string> sources)
        {
            return "\"/p:RestoreSources=" + string.Join(MsbuildPropertySeparator, sources) + "\"";
        }

        private static string MsbuildPropertySeparator = "%3B";

        public static IEnumerable<string> NugetConfigSources => TestSuite.NugetConfigSources;

        public static string RestoreDefaultArgs => $"/p:RestoreNoCache=true {LogArgs} \"/p:RestorePackagesPath={TestSuite.NugetPackagesDir}\"";

        public static string LogArgs => "/v:n";
    }
}
