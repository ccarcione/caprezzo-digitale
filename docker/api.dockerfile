FROM mcr.microsoft.com/dotnet/sdk:6.0 AS netbuilder
WORKDIR /repo
COPY CaprezzoDigitale.WebApi .
RUN dotnet publish CaprezzoDigitale.WebApi.csproj -o /publish/CaprezzoDigitale.WebApi

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS prod
WORKDIR /api
COPY --from=netbuilder /publish/CaprezzoDigitale.WebApi .
ENTRYPOINT ["dotnet", "CaprezzoDigitale.WebApi.dll"]