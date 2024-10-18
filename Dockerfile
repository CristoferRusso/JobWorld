# Usa l'immagine SDK di .NET 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copia i file .csproj e ripristina le dipendenze
COPY *.csproj ./ 
RUN dotnet restore 

# Copia il resto del codice sorgente
COPY . ./ 

# Pubblica l'applicazione
RUN dotnet publish JobWorld.csproj -c Release -o /out 

# Usa l'immagine di runtime di .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./ 

# Espone la porta 5000
EXPOSE 5000 

# Comando per avviare l'app
ENTRYPOINT ["dotnet", "JobWorld.dll"]
