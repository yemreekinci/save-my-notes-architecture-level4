FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["SaveMyNotes.API/SaveMyNotes.API.csproj", "SaveMyNotes.API/"]
COPY ["SaveMyNotes.Application/SaveMyNotes.Application.csproj", "SaveMyNotes.Application/"]
COPY ["SaveMyNotes.Infrastructure/SaveMyNotes.Infrastructure.csproj", "SaveMyNotes.Infrastructure/"]
COPY ["SaveMyNotes.Domain/SaveMyNotes.Domain.csproj", "SaveMyNotes.Domain/"]

RUN dotnet restore "SaveMyNotes.API/SaveMyNotes.API.csproj"
COPY . .
WORKDIR "/src/SaveMyNotes.API"
RUN dotnet publish "SaveMyNotes.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ---------------- runtime ----------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Sadece publish edilen dosyaları alıyoruz, ML kütüphanelerine gerek kalmadı
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SaveMyNotes.API.dll"]