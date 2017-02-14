using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Tools.Test.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using FluentAssertions;
using static NetcoreCliFsc.Tests.TestSuite;

namespace NetcoreCliFsc.Tests
{
    public class GivenThatIWantANewFSApp : TestBase
    {
        // [Theory]
        // [InlineData("console")]
        // [InlineData("lib")]
        // [InlineData("web")]
        public void When_dotnet_build_is_invoked_Then_project_builds_without_warnings(string type)
        {
            var rootPath = Temp.CreateDirectory().Path;

            Func<string,TestCommand> test = name => new TestCommand(name) { WorkingDirectory = rootPath };

            test("dotnet")
                .Execute($"new --lang fsharp --type {type}")
                .Should().Pass();

            test("dotnet")
                .Execute($"restore {RestoreDefaultArgs} {RestoreSourcesArgs(NugetConfigSources)}")
                .Should().Pass();

            test("dotnet")
                .Execute($"build {LogArgs}")
                .Should().Pass();

            if (type == "console")
            {
                test("dotnet")
                    .Execute($"run {LogArgs}")
                    .Should().Pass();
            }
        }

        /*
        [Fact]
        public void When_NewtonsoftJson_dependency_added_Then_project_restores_and_runs()
        {
            var rootPath = Temp.CreateDirectory().Path;
            var projectJsonFile = Path.Combine(rootPath, "project.json");

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("new --lang fsharp");
            
            GivenThatIWantANewCSApp.AddProjectJsonDependency(projectJsonFile, "Newtonsoft.Json", "7.0.1");

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("restore")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("run")
                .Should().Pass();
        }
        */
    }
}
