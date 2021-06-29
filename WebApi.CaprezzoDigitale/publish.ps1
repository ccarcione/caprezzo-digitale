dotnet restore
dotnet build
dotnet publish --configuration Debug --framework netcoreapp3.1 --self-contained true --runtime linux-x64
New-Item -ItemType Directory -Force -Path publish
$datetime = Get-Date -Format "dd_MM_yyyy__HH_mm_ss"
Compress-Archive -Path bin\Debug\netcoreapp3.1\linux-x64\publish\* -DestinationPath publish\WebApi.CaprezzoDigitale__$datetime.zip -Force
echo "Compress-Archive.."
echo ""
echo "PREMI UN TASTO PER CHIUDERE.."
[Console]::ReadKey()
ii publish