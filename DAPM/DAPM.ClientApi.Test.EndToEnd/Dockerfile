FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["DAPM.ClientApi.Test.EndToEnd/DAPM.ClientApi.Test.EndToEnd.csproj", "DAPM.ClientApi.Test.EndToEnd/"]
RUN dotnet restore "DAPM.ClientApi.Test.EndToEnd/DAPM.ClientApi.Test.EndToEnd.csproj"

COPY . .
WORKDIR "/src/DAPM.ClientApi.Test.EndToEnd"
RUN dotnet build "DAPM.ClientApi.Test.EndToEnd.csproj" -c Release -o /app/build

CMD ["dotnet", "test", "DAPM.ClientApi.Test.EndToEnd.csproj", "--logger:trx"]
