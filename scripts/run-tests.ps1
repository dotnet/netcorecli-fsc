$global:testSuite = @{}

# test helper

function Run-Test {
  Param([string] $Name, [scriptblock] $Check)
  
  Write-Host "## Testing $Name..."  -ForegroundColor "magenta"
  try {
    $Check.Invoke()
    Write-Host "## Testing $Name [OK]"  -ForegroundColor "green"
    $global:testSuite.Add($Name, $TRUE)
  }
  catch {
    Write-Host "## Testing $Name [FAILED]"  -ForegroundColor "red"
    Write-Host $_.Exception.Message
    $global:testSuite.Add($Name, $FALSE)
  }
}

function Dotnet-Build {
  Run-Cmd "dotnet" "--verbose build"
}

function Dotnet-Run {
  Param([string] $Arguments)

  Run-Cmd "dotnet" "--verbose run $Arguments"
}

# dotnet new
<#

dotnet new doesnt work ootb in preview2

Run-Test "dotnet new" {

  Remove-Item "$rootDir\test\test-dotnet-new" -Recurse -ErrorAction Ignore

  mkdir "$rootDir\test\test-dotnet-new" -Force | cd

  Run-Cmd "dotnet" "new --lang f#"

  Run-Cmd "dotnet" "restore -f `"$rootDir\bin`""

  Dotnet-Build

  Dotnet-Run "c d"
}
#>

# test from assets

function Dotnet-Restore {
  Run-Cmd "dotnet" "restore -v Information -f `"$rootDir\bin`" --configfile `"$rootDir\test\NuGet.Config`""
}

Run-Test "test/TestAppWithArgs" {

  cd "$rootDir\test\TestAppWithArgs"

  Dotnet-Restore

  Dotnet-Build

  Dotnet-Run ""
}

Run-Test "test/TestLibrary" {

  cd "$rootDir\test\TestLibrary"

  Dotnet-Restore

  Dotnet-Build
}

Run-Test "test/TestApp" {

  cd "$rootDir\test\TestApp"

  Dotnet-Restore

  Dotnet-Build

  Dotnet-Run ""
}

Write-Host "# Tests results"  -ForegroundColor "magenta"
foreach ($h in $global:testSuite.GetEnumerator()) {
    $color = If ($h.Value) {"green"} Else {"red"}
    $text = If ($h.Value) {"PASSED"} Else {"FAILED"}
    Write-Host "- $($h.Name): [$text]" -ForegroundColor $color
}

If ($global:testSuite.ContainsValue($FALSE)) {
  exit 2
}
