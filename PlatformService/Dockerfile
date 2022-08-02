FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY . ./

RUN dotnet publish -o out -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app/out/* ./

EXPOSE 80

CMD [ "dotnet","PlatformService.dll" ]