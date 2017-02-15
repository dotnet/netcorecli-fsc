// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.DotNet.InternalAbstractions;
using Xunit;

namespace Microsoft.DotNet.Tools.Test.Utilities
{
    public class WindowsOnlyFactAttribute : FactAttribute
    {
        public string Reason { get; set; }

        public WindowsOnlyFactAttribute()
        {
            if (RuntimeEnvironment.OperatingSystemPlatform != Platform.Windows)
            {
                this.Skip = "This test requires windows to run"
                          + (string.IsNullOrEmpty(Reason)? "" : $". Why? {Reason}");
            }
        }
    }

    public class WindowsOnlyTheoryAttribute : TheoryAttribute
    {
        public WindowsOnlyTheoryAttribute()
        {
            if (RuntimeEnvironment.OperatingSystemPlatform != Platform.Windows)
            {
                this.Skip = "This test requires windows to run";
            }
        }
    }
}