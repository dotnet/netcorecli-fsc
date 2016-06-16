# dotnet new


Write-Host "## Testing dotnet new..."  -foregroundcolor "magenta"

Remove-Item "$rootDir\test\test-dotnet-new" -Recurse -ErrorAction Ignore

mkdir "$rootDir\test\test-dotnet-new" -Force | cd

Run-Cmd "dotnet" "new --lang f#"

Run-Cmd "dotnet" "restore -f `"$rootDir\bin`""

Run-Cmd "dotnet" "--verbose build"

Run-Cmd "dotnet" "--verbose run a b"

Write-Host "## Testing dotnet new [OK]"  -foregroundcolor "green"
