using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Tools.Test.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using FluentAssertions;

namespace NetcoreCliFsc.Tests
{
    public class GivenThatIWantANewFSApp : TestBase
    {
        private static string NugetConfigWithDevFeedPath 
        {
            get { return Path.Combine(RepoRoot, "NuGet.withDevFeed.Config"); }
        }

        [Theory]
        [InlineData("console")]
        [InlineData("lib")]
        public void When_dotnet_build_is_invoked_Then_project_builds_without_warnings(string type)
        {
            var rootPath = Temp.CreateDirectory().Path;

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute($"new --lang fsharp --type {type}")
                .Should().Pass();

            new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute($"restore --no-cache -v n --configfile \"{NugetConfigWithDevFeedPath}\"")
                .Should().Pass();

            var buildResult = new TestCommand("dotnet") { WorkingDirectory = rootPath }
                .Execute("build -v n")
                .Should().Pass();

            if (type == "console")
            {
                var runResult = new TestCommand("dotnet") { WorkingDirectory = rootPath }
                    .Execute("run -v n")
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
