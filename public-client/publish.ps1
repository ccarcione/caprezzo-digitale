echo "npm i: aggiornamento pacchetti"
npm i
echo "publish public-client"
ng build --prod
$datetime = Get-Date -Format "dd_MM_yyyy__HH_mm_ss"
Compress-Archive -Path dist\public-client\* -DestinationPath .\dist\public-client__$datetime.zip -Force
echo "Compress-Archive.."
echo ""
echo "premi un tast per chiudere"
[Console]::ReadKey()
ii dist