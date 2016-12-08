
param(
    [string] $Configuration = "Debug",
    [string] $Architecture = "x64",
    [string[]] $Targets = @("Build", "Tests"),
    [switch] $Help,
    [Parameter(Position=0, ValueFromRemainingArguments=$true)] $ExtraParameters )

if($Help)
{
    Write-Host "Usage: build [[-Help] [-Targets <TARGETS...>] "
    Write-Host ""
    Write-Host "Options:"
    Write-Host "  -Targets <TARGETS...>              Comma separated build targets to run (Build, Tests; Default is a full build and tests)"
    Write-Host "  -Help                              Display this help message"
    exit 0
}

#make path absolute
$rootDir = Split-Path -parent (Split-Path -parent $PSCommandPath)

function Install-DotnetSdk([string] $sdkVersion)
{
    Write-Host "# Install .NET Core Sdk versione '$sdkVersion'" -foregroundcolor "magenta"
    $sdkInstallScriptUrl = "https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0-preview2.1/scripts/obtain/dotnet-install.ps1"
    $sdkInstallScriptPath = ".dotnetsdk\dotnet_cli_install.ps1"
    Write-Host "Downloading sdk install script '$sdkInstallScriptUrl' to '$sdkInstallScriptPath'"
    New-Item "$rootDir\.dotnetsdk" -Type directory -ErrorAction Ignore
    Invoke-WebRequest $sdkInstallScriptUrl -OutFile "$rootDir\$sdkInstallScriptPath"

    Write-Host "Running sdk install script..."
    ./.dotnetsdk/dotnet_cli_install.ps1 -InstallDir ".dotnetsdk\sdk-$sdkVersion" -Channel "preview" -version $sdkVersion
}

function Run-Cmd
{
  param( [string]$exe, [string]$arguments )
  Write-Host "$exe $arguments" -ForegroundColor "Blue"
  iex "$exe $arguments 2>&1" | Out-Host
  if ($LastExitCode -ne 0) {
    throw "Command failed with exit code $LastExitCode."
  }
  Write-Host ""
}

function Using-Sdk ([string] $sdkVersion)
{
  $sdkPath = "$rootDir\.dotnetsdk\sdk-$sdkVersion"
  Write-Host "# Using sdk '$sdkVersion'" -foregroundcolor "magenta"
  $env:Path = "$sdkPath;$env:Path"
  Run-Cmd "dotnet" "--version"
}

function Do-preview3
{
  $sdkVersion = '1.0.0-preview2-1-003177'

  Install-DotnetSdk $sdkVersion

  Using-Sdk $sdkVersion

  & "$rootDir\scripts\run-tests.ps1"
  if ($LASTEXITCODE -ne 0) { throw "Failed to build" } 
}

# main
try {
  Push-Location $PWD

  Do-preview3
}
finally {
  Pop-Location
}
