FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GoldenPixelBackend/GoldenPixelBackend.csproj", "GoldenPixelBackend/"]
COPY . .
RUN dotnet restore "GoldenPixelBackend/GoldenPixelBackend.csproj"
WORKDIR "/src/GoldenPixelBackend"
RUN dotnet build "GoldenPixelBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoldenPixelBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoldenPixelBackend.dll"]
