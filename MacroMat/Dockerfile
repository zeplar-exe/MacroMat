FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MacroMat/MacroMat.csproj", "MacroMat/"]
RUN dotnet restore "MacroMat/MacroMat.csproj"
COPY . .
WORKDIR "/src/MacroMat"
RUN dotnet build "MacroMat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MacroMat.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MacroMat.dll"]
