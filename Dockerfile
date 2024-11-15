#base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#compile and publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SickLeaveAPI.csproj", "./"]
RUN dotnet restore "SickLeaveAPI.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet build "SickLeaveAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SickLeaveAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish

#Final Immage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "SickLeaveAPI.dll", "--environment=Development"]