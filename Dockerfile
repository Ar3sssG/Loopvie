# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy all project files and restore dependencies
COPY WLAPI/WLAPI.csproj WLAPI/
COPY WLBusinessLogic/WLBusinessLogic.csproj WLBusinessLogic/
COPY WLDataLayer/WLDataLayer.csproj WLDataLayer/
RUN dotnet restore WLAPI/WLAPI.csproj

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR /src/WLAPI
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WLAPI.dll"]
