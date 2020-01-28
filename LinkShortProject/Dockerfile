FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["LinkShortProject/LinkShortProject.csproj", "LinkShortProject/"]
RUN dotnet restore "LinkShortProject/LinkShortProject.csproj"
COPY . .
WORKDIR "/src/LinkShortProject"
RUN dotnet build "LinkShortProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinkShortProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinkShortProject.dll"]