
BASEDIR=$(dirname "$0")
REPOROOT=$(dirname "$BASEDIR")

# test helper

RunCmd ()
{
  echo "[EXEC] $1 $2"
  eval "$1 $2"
  if [ $? != 0 ]; then
      echo "run-build: Error $?."
      exit $?
  fi
}




# pack src/dotnet-compile-fsc

cd "$REPOROOT/src/dotnet-compile-fsc"

RunCmd "dotnet" 'restore -v Information --no-cache --configfile "$REPOROOT/NuGet.Config"'

RunCmd "dotnet" "-v pack -c Release"

# run tests

cd "$REPOROOT/test/"

RunCmd "dotnet" 'restore -v Information --no-cache --configfile "$REPOROOT/NuGet.Config"'

cd "$REPOROOT/test/dotnet-new.Tests"

RunCmd "dotnet" "-v test"
