FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
# Puerto por defecto para la ejecución local. En un PaaS manda la variable PORT (ver Program.cs)
# y el TLS lo termina el proxy del hosting, por eso aquí solo se habla HTTP.
ENV ASPNETCORE_URLS=http://*:5000

USER app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

# Se copian primero todos los .csproj: Web.API referencia a Movies y Shared, así que restore
# necesita tenerlos delante. Además, la capa de restore queda cacheada mientras no cambien
# las dependencias.
COPY ["MoviInfoBack.sln", "./"]
COPY ["Web.API/Web.API.csproj", "Web.API/"]
COPY ["Movies/Movies.csproj", "Movies/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Test/Test.csproj", "Test/"]
RUN dotnet restore "Web.API/Web.API.csproj"

COPY . .

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Web.API/Web.API.csproj" \
      -c $configuration \
      -o /app/publish \
      --no-restore \
      /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.API.dll"]
