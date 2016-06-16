param(
    [string]$VersionSuffix="013072",
    [string[]]$Targets=@("Build", "Tests"),
    [switch]$Help)

if($Help)
{
    Write-Host "Usage: build [[-Help] [-Targets <TARGETS...>] [-VersionSuffix <VERSION>]"
    Write-Host ""
    Write-Host "Options:"
    Write-Host "  -VersionSuffix  <VERSION>          Use <VERSION> as version suffix for package"
    Write-Host "  -Targets <TARGETS...>              Comma separated build targets to run (Build, Tests; Default is a full build and tests)"
    Write-Host "  -Help                              Display this help message"
    exit 0
}

#make path absolute
$rootDir = Split-Path -parent (Split-Path -parent $PSCommandPath)

function Run-Cmd
{
  param( [string]$exe, [string]$arguments )
  Write-Host "$exe $arguments"
  iex "$exe $arguments 2>&1" | Out-Host
  if ($LastExitCode -ne 0) {
    throw "Command failed with exit code $LastExitCode."
  }
  Write-Host ""
}

try {

Push-Location $PWD


# dotnet info

Run-Cmd "dotnet" "--info"


# build the package

if ($Targets -contains "Build") {
  Write-Host "# BUILD" -foregroundcolor "magenta"

  Write-Host "remove dir $rootDir\bin"
  Remove-Item "$rootDir\bin" -Recurse -ErrorAction Ignore

  Write-Host "cd src\dotnet-compile-fsc"
  cd src\dotnet-compile-fsc

  Run-Cmd "dotnet" "restore"

  Run-Cmd "dotnet" "pack -c Release -o `"$rootDir\bin`" --version-suffix $VersionSuffix"

  Write-Host "# BUILD [OK]"  -foregroundcolor "green"
}

# run tests

if ($Targets -contains "Tests") {
  Write-Host "# RUN TESTS" -foregroundcolor "magenta"
  . $rootDir\scripts\run-tests.ps1
}


}
finally {
  Pop-Location
}
