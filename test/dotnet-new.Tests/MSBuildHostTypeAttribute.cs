using System;
using Xunit;
using static System.Environment;

namespace NetcoreCliFsc.Tests
{
    [Flags]
    public enum MSBuildHostTypesOnly
    {
        Core = 1,
        MSBuild = 2,
        Mono = 4,
    }

    public class MSBuildHostTypeOnlyFactAttribute : FactAttribute
    {
        public string Reason { get; set; }

        public MSBuildHostTypeOnlyFactAttribute(MSBuildHostTypesOnly msbuildHostType)
        {
            if (!TestSuite.MSBuildHostTypesOnly.HasFlag(msbuildHostType))
            {
                var hostName = Enum.GetName(typeof(MSBuildHostTypesOnly), msbuildHostType);
                this.Skip = $"This test requires msbuild host type {hostName} to run"
                          + (string.IsNullOrEmpty(Reason)? "" : $". Why? {Reason}");
            }
        }
    }

    public class MSBuildHostTypeOnlyTheoryAttribute : TheoryAttribute
    {
        public string Reason { get; set; }

        public MSBuildHostTypeOnlyTheoryAttribute(MSBuildHostTypesOnly msbuildHostType)
        {
            if (!TestSuite.MSBuildHostTypesOnly.HasFlag(msbuildHostType))
            {
                var hostName = Enum.GetName(typeof(MSBuildHostTypesOnly), msbuildHostType);
                this.Skip = $"This test requires msbuild host type {hostName} to run"
                          + (string.IsNullOrEmpty(Reason)? "" : $". Why? {Reason}");
            }
        }
    }
}
