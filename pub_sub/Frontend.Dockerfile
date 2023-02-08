FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build-env
WORKDIR /app

COPY *.sln .
COPY src/ ./src/

RUN dotnet restore
RUN dotnet publish src/Frontend/Frontend.csproj -c Release -o ./out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out ./Frontend
ENTRYPOINT ["dotnet", "Frontend/Frontend.dll"]