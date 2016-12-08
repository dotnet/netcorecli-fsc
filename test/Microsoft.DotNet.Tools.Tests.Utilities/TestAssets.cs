// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.DotNet.Tools.Test.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using FluentAssertions;

namespace Microsoft.DotNet.Tools.Test.Utilities
{
    public class TestAssets
    {
        private string testAssetsDir;

        public TestAssets(string rootDir)
        {
            this.testAssetsDir = Path.Combine(rootDir, "testAssets");
        }

        public void CopyDirTo(string testAssetDirName, string toDir)
        {
            if (!Directory.Exists(toDir))
                Directory.CreateDirectory(toDir);

            string testAssetDir = Path.Combine(testAssetsDir, testAssetDirName);

            var files = Directory.GetFiles(testAssetDir, "*.*");
            foreach (var f in files)
            {
                File.Copy(f, Path.Combine(toDir, Path.GetFileName(f)));
            }
        }    
    }
}
