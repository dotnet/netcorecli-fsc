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
using static NetcoreCliFsc.Tests.TestSuite;

namespace NetcoreCliFsc.Tests
{
    public class CommonScenario : TestBase
    {
        private static string RestoreProps()
        {
            var props = new Dictionary<string,string>() 
            {
                { "FSharpNETSdkVersion", GetEnvironmentVariable("TEST_SUITE_FSHARP_NET_SDK_PKG_VERSION")},
                { "FSharpCorePkgVersion", GetEnvironmentVariable("TEST_SUITE_FSHARP_CORE_PKG_VERSION")},
            };

            return string.Join(" ", props.Where(kv => kv.Value != null).Select(kv => $"/p:{kv.Key}={kv.Value}") );
        }

        [Fact]
        public void TestAppWithArgs()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("netcoreapp1.0/TestConsoleAppTemplate", rootPath);
            TestAssets.CopyDirTo("TestAppWithArgs", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();

            test("dotnet")
                .Execute($"run {LogArgs}")
                .Should().Pass();
        }

        [WindowsOnlyFact(Reason = ".NET 4.5.1 require an to install the SDK or a Targeting Pack for that framework version. No clue how to do that on non win Os")]
        //TODO how to build .net app/lib in osx/unix? need mono? need mono on ci server?
        public void TestAppWithArgs451()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("net451/TestConsoleAppTemplate", rootPath);
            TestAssets.CopyDirTo("TestAppWithArgs", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            string rid = GetCurrentRID();

            test("dotnet")
                .Execute($"restore -r {rid} {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build -r {rid} {LogArgs}")
                .Should().Pass();

            test(Path.Combine(rootPath, "bin", "Debug", "net451", "ConsoleApp.exe"))
                .Execute($"arg1 arg2")
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
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();
        }

        [Fact]
        public void TestXmlDoc()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("TestLibrary", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            Assert.Equal(false, File.Exists(Path.Combine(rootPath, "doc.xml")));

            test("dotnet")
                .Execute($"build {LogArgs} /p:DocumentationFile=doc.xml")
                .Should().Pass();

            Assert.Equal(true, File.Exists(Path.Combine(rootPath, "doc.xml")));
        }

        [Fact]
        public void TestImplicitFrameworkDefines()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("netcoreapp1.0/TestConsoleAppTemplate", rootPath);
            TestAssets.CopyDirTo("TestAppDefines", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();

            var result = test("dotnet").ExecuteWithCapturedOutput($"run {LogArgs}");

            result.Should().Pass();

            Assert.NotNull(result.StdOut);
            Assert.Contains($"TFM: 'NETCOREAPP1_0'", result.StdOut.Trim());
        }

        [Fact]
        public void TestImplicitConfigurationDefines()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("netcoreapp1.0/TestConsoleAppTemplate", rootPath);
            TestAssets.CopyDirTo("TestAppDefines", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            var configurations = new Dictionary<string,string> { 
                { "release", "RELEASE" },
                { "debug", "DEBUG" },
            };

            foreach (var kv in configurations)
            {
                test("dotnet")
                    .Execute($"build {LogArgs} -c {kv.Key}")
                    .Should().Pass();

                var result = test("dotnet").ExecuteWithCapturedOutput($"run -c {kv.Key} {LogArgs}");

                result.Should().Pass();

                Assert.NotNull(result.StdOut);
                Assert.Contains($"CONF: '{kv.Value}'", result.StdOut.Trim());
            }
        }

        [Fact]
        public void TestMultipleLibraryInSameDir()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("TestLibrary", rootPath);
            TestAssets.CopyDirTo("TestMultipleLibraryInSameDir", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore TestLibrary.fsproj {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"restore TestLibrary2.fsproj {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build TestLibrary.fsproj {LogArgs}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build TestLibrary2.fsproj {LogArgs}")
                .Should().Pass();
        }

        [WindowsOnlyFact(Reason = ".NET 4.5.1 require an to install the SDK or a Targeting Pack for that framework version. No clue how to do that on non win Os")]
        public void TestLibraryCross()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("TestLibraryCross", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();

            test("dotnet")
                .Execute($"pack {LogArgs}")
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
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
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
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();
        }

        private string GetCurrentRID()
        {
            var rootPath = Temp.CreateDirectory().Path;

            Func<string,TestCommand> test = n => new TestCommand(n) { WorkingDirectory = rootPath };
            
            var result = test("dotnet").ExecuteWithCapturedOutput($"--info");

            result.Should().Pass();

            var dotnetInfo = result.StdOut;

            string rid = 
                dotnetInfo
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.StartsWith("RID:"))
                .Select(s => s.Replace("RID:", "").Trim())
                .FirstOrDefault();

            return rid;
        }

        private void CreateNoopExe(string intoDir, string name, bool fail = false)
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("Noop", rootPath);

            Func<string,TestCommand> test = n => new TestCommand(n) { WorkingDirectory = rootPath };

            string rid = GetCurrentRID();
            string msbuildArgs = $"/p:AssemblyName={name} " + (fail? "/p:Fail=true" : "");

            test("dotnet")
                .Execute($"restore -r {rid} {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()} {msbuildArgs}")
                .Should().Pass();
            
            test("dotnet")
                .Execute($"publish -r {rid} -o \"{intoDir}\" {msbuildArgs}")
                .Should().Pass();
        }

        [Fact]
        public void DifferentDotnetInPATH()
        {
            var rootPath = Temp.CreateDirectory().Path;

            var fakeDotnetDir = Path.Combine(rootPath, "dotnetsdk");

            Directory.CreateDirectory(fakeDotnetDir);
            CreateNoopExe(fakeDotnetDir, "dotnet", fail : true);

            var appDir = Path.Combine(rootPath, "TestApp");

            TestAssets.CopyDirTo("TestLibrary", appDir);
            TestAssets.CopyDirTo("TestSuiteProps", appDir);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = appDir };

            test("dotnet")
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            var dotnetPath = Microsoft.DotNet.Cli.Utils.Env.GetCommandPath("dotnet");

            var newPATHEnvVar = fakeDotnetDir + Path.PathSeparator + GetEnvironmentVariable("PATH");
            
            test(dotnetPath)
                .WithEnvironmentVariable("PATH", newPATHEnvVar)
                .Execute($"build {LogArgs}")
                .Should().Pass();
        }


        [Fact]
        public void TestAppWithRes()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("netcoreapp1.0/TestConsoleAppTemplate", rootPath);
            TestAssets.CopyDirTo("TestAppWithRes", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();

            var result = test("dotnet").ExecuteWithCapturedOutput($"run {LogArgs}");

            result.Should().Pass();

            Assert.NotNull(result.StdOut);
            Assert.Equal("Hi!", result.StdOut.Trim());
        }


        [WindowsOnlyFact(Reason = ".NET 4.5.1 require an to install the SDK or a Targeting Pack for that framework version. No clue how to do that on non win Os")]
        public void TestAppWithResNet451()
        {
            var rootPath = Temp.CreateDirectory().Path;

            TestAssets.CopyDirTo("net451/TestConsoleAppTemplate", rootPath);
            TestAssets.CopyDirTo("TestAppWithRes", rootPath);
            TestAssets.CopyDirTo("TestSuiteProps", rootPath);

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            string rid = GetCurrentRID();

            test("dotnet")
                .Execute($"restore -r {rid} {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)} {RestoreProps()}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build -r {rid} {LogArgs}")
                .Should().Pass();


            var result = 
                test(Path.Combine(rootPath, "bin", "Debug", "net451", "ConsoleApp.exe"))
                .ExecuteWithCapturedOutput("");

            result.Should().Pass();

            Assert.NotNull(result.StdOut);
            Assert.Equal("Hi!", result.StdOut.Trim());
        }

    }
}
