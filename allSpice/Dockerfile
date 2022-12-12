FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything

# Restore as distinct layers
# Build and publish a release
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Start runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build-env /app/out .
# ------------------------------------⬇ replace with your apps name + .dll
CMD ASPNETCORE_URLS=http://* dotnet allSpice.dll