# Use the .NET 7.0 runtime image as a base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET 7.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /build

# Copy the csproj file and restore any dependencies (via nuget)
COPY ["Flats4us.csproj", "./"]
RUN dotnet restore

# Copy the main source project files
COPY . .

RUN dotnet build "Flats4us.csproj" -c Release -o /build/out

FROM build AS publish
RUN dotnet publish "Flats4us.csproj" -c Release -o /publish/out

FROM base AS final
WORKDIR /app
COPY --from=publish /publish/out .

# Copy additional folders
COPY Images/ Images/
COPY Logs/ Logs/
COPY Lang/ Lang/

ENTRYPOINT ["dotnet", "Flats4us.dll"]
