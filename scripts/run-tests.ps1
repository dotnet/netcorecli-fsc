
# pack src/dotnet-compile-fsc

cd "$rootDir/src/dotnet-compile-fsc"

Run-Cmd "dotnet" "restore -v Information --no-cache --configfile `"$rootDir\NuGet.Config`""

Run-Cmd "dotnet" "-v pack -c Release"

# run tests

cd "$rootDir/test/"

Run-Cmd "dotnet" "restore -v Information --no-cache --configfile `"$rootDir\NuGet.Config`""

cd "$rootDir/test/dotnet-new.Tests"

Run-Cmd "dotnet" "-v test"
