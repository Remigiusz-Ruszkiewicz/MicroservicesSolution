FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project file(s) and restore dependencies
COPY ["Api/Api.csproj", "Api/"]
RUN dotnet restore "Api/Api.csproj"
# Copy the rest of the files and build the project
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/

FROM build AS publish
# Publish the application
RUN dotnet publish "Api.csproj" -c Release -o /app/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/ .
ENTRYPOINT ["dotnet", "Api.dll"]
