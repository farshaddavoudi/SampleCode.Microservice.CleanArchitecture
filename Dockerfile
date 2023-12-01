#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Get Base Image (Full .NET Core SDK)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY Src/**API/*API.csproj ./
RUN dotnet restore


# Copy everything else and build
COPY . ./
WORKDIR /src/SampleMicroserviceApp.Identity.Web.API
#if we don't have any additional build requirements, we can omit the dotnet build step and stick with the dotnet publish step 
RUN dotnet build "SampleMicroserviceApp.Identity.Web.API.csproj" -c Release -o /app/build


# Publish
FROM build AS publish
RUN dotnet publish "SampleMicroserviceApp.Identity.Web.API.csproj" -c Release -o /app/publish /p:UseAppHost=false


# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleMicroserviceApp.Identity.Web.API.dll"]