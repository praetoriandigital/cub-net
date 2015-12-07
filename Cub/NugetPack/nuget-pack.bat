MSBuild ..\Cub.csproj /property:Configuration=Release
mkdir lib
mkdir lib\Net20
copy ..\bin\Release\Cub.dll .\lib\Net20
NuGet Pack Cub.nuspec
set /p=Press ENTER to continue...
