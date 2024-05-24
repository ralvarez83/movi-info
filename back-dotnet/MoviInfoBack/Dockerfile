FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000

#ENV ASPNETCORE_URLS=http://+:5021

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Web.API/Web.API.csproj", "Web.API/"]
RUN dotnet restore "Web.API/Web.API.csproj"
COPY . .
#WORKDIR "/src/Web.API"
#COPY Domain /src/Domain
#COPY ../Application /src/Application
#COPY ../Infraestructure /src/Infraestructure
WORKDIR "/src/Web.API"
RUN dotnet build -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Web.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.API.dll"]
