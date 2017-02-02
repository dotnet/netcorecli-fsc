using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Tools.Test.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using FluentAssertions;
using static System.Environment;

namespace NetcoreCliFsc.DotNet.Tests
{
    public class CommonScenario : TestBase
    {
        private static IEnumerable<string> NugetConfigSources
        {
            get 
            { 
                yield return "https://api.nuget.org/v3/index.json";
                var pkgsDir = Path.Combine(RepoRoot, "test", "packagesToTest");
                if (Directory.Exists(pkgsDir))
                    yield return pkgsDir;
            }
        }

        private static string RestoreSourcesArgs(IEnumerable<string> sources)
        {
            return string.Join(" ", sources.Select(x => $"--source \"{x}\""));
        }

        private static string RestoreProps()
        {
            var props = new Dictionary<string,string>() 
            {
                { "FSharpNETSdkVersion", GetEnvironmentVariable("TEST_SUITE_FSHARP_NET_SDK_PKG_VERSION")},
                { "MicrosoftFSharpCorenetcoreVersion", GetEnvironmentVariable("TEST_SUITE_MS_FSHARP_CORE_PKG_VERSION")},
            };

            return string.Join(" ", props.Where(kv => kv.Value != null).Select(kv => $"/p:{kv.Key}={kv.Value}") );
        }

        private static string LogArgs => "-v n";

        [Fact]
        public void TestAppWithArgs()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("TestAppWithArgs", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore --no-cache {LogArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();

            test("dotnet")
                .Execute($"run {LogArgs}")
                .Should().Pass();
        }

        [Fact]
        public void TestLibrary()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("TestLibrary", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore --no-cache {LogArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
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

            var appDir = Path.Combine(rootPath, "TestApp");

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = appDir };

            test("dotnet")
                .Execute($"restore --no-cache {LogArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();

            test("dotnet")
                .Execute($"run {LogArgs}")
                .Should().Pass();
        }

        [Fact]
        public void TestPathWithBlank()
        {
            var rootPath = Path.Combine(Temp.CreateDirectory().Path, "path with blank");

            TestAssets.CopyDirTo("TestLibrary", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore --no-cache {LogArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();
        }
    }
}
