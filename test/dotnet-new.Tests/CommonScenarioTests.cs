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
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute($"restore --no-cache -v n --configfile \"{NugetConfigWithDevFeedPath}\"")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("build -v n")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("run -v n")
                .Should().Pass();
        }

        [Fact]
        public void TestLibrary()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("TestLibrary", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute($"restore --no-cache -v n --configfile \"{NugetConfigWithDevFeedPath}\"")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("build -v n")
                .Should().Pass();
        }

        [Fact]
        public void TestApp()
        {
            var rootPath = Temp.CreateDirectory().Path;

            foreach (var a in new[] { "TestLibrary", "TestApp" })
            {
                var projDir = Path.Combine(rootPath, a);
                TestAssets.CopyDirTo(a, projDir);
                TestAssets.CopyDirTo("TestSuiteProps", projDir);
            }

            new TestCommand("dotnet") { WorkingDirectory = Path.Combine(rootPath, "TestApp") }
                .Execute($"restore --no-cache -v n --configfile \"{NugetConfigWithDevFeedPath}\"")
                .Should().Pass();

            var appDir = Path.Combine(rootPath, "TestApp");

            new TestCommand("dotnet") { WorkingDirectory = appDir }
                .Execute("build -v n")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = appDir }
                .Execute("run -v n")
                .Should().Pass();
        }
    }
}
