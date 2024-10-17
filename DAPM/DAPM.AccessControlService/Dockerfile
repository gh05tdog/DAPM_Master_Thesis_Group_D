FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DAPM.AccessControlService/DAPM.AccessControlService.csproj", "DAPM.AccessControlService/"]
RUN dotnet restore "DAPM.AccessControlService/DAPM.AccessControlService.csproj"
COPY . .
WORKDIR "/src/DAPM.AccessControlService"
RUN dotnet build "DAPM.AccessControlService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DAPM.AccessControlService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DAPM.AccessControlService.dll"]
