using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Tools.Test.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using FluentAssertions;

namespace NetcoreCliFsc.DotNet.Tests
{
    public class CommonScenario : TestBase
    {
        private static string NugetConfigWithDevFeedPath 
        {
            get { return Path.Combine(RepoRoot, "NuGet.withDevFeed.Config"); }
        }

        [Fact]
        public void TestAppWithArgs()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("TestAppWithArgs", rootPath);

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute($"-v restore --no-cache -v Information --configfile \"{NugetConfigWithDevFeedPath}\"")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("-v build")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("-v run")
                .Should().Pass();
        }

        [Fact]
        public void TestLibrary()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("TestLibrary", rootPath);

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute($"-v restore --no-cache -v Information --configfile \"{NugetConfigWithDevFeedPath}\"")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("-v build")
                .Should().Pass();
        }

        [Fact]
        public void TestApp()
        {
            var rootPath = Temp.CreateDirectory().Path;

            foreach (var a in new[] { "TestLibrary", "TestApp" })
            {
                Directory.CreateDirectory(Path.Combine(rootPath, a));
                TestAssets.CopyDirTo(a, Path.Combine(rootPath, a));
            }

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute($"-v restore --no-cache -v Information --configfile \"{NugetConfigWithDevFeedPath}\"")
                .Should().Pass();

            var appDir = Path.Combine(rootPath, "TestApp");

            new TestCommand("dotnet") { WorkingDirectory = appDir }
                .Execute("-v build")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = appDir }
                .Execute("-v run")
                .Should().Pass();
        }
    }
}
