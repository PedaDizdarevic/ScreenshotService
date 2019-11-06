FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS buildcontainer
WORKDIR /app

COPY ./ScreenshotService/ScreenshotService.csproj ./
RUN dotnet restore

COPY ./ScreenshotService/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ScreenshotService.dll", "--urls=http://0.0.0.0:5001/"] 