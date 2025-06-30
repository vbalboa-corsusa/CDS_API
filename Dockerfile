# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de solución y de proyecto
COPY CDS_API.sln ./
COPY CDS_API/CDS_API.csproj CDS_API/
COPY CDS_BLL/CDS_BLL.csproj CDS_BLL/
COPY CDS_DAL/CDS_DAL.csproj CDS_DAL/
COPY CDS_Models/CDS_Models.csproj CDS_Models/

# Restaura dependencias
RUN dotnet restore

# Copia el resto del código
COPY CDS_API/ CDS_API/
COPY CDS_BLL/ CDS_BLL/
COPY CDS_DAL/ CDS_DAL/
COPY CDS_Models/ CDS_Models/

WORKDIR /src/CDS_API

# Publica la app (compila para producción)
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Puerto por defecto de ASP.NET Core
EXPOSE 80

# Comando de inicio
ENTRYPOINT ["dotnet", "CDS_API.dll"]