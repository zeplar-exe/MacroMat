FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MacroMat.CLI/MacroMat.CLI.csproj", "MacroMat.CLI/"]
RUN dotnet restore "MacroMat.CLI/MacroMat.CLI.csproj"
COPY . .
WORKDIR "/src/MacroMat.CLI"
RUN dotnet build "MacroMat.CLI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MacroMat.CLI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MacroMat.CLI.dll"]
