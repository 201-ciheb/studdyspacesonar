#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5222
EXPOSE 7280

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PHIASPACE.CORE/PHIASPACE.CORE.csproj", ""]
RUN dotnet restore "./PHIASPACE.CORE.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PHIASPACE.CORE/PHIASPACE.CORE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PHIASPACE.CORE/PHIASPACE.CORE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# RUN chmod +x ./entrypoint.sh
# CMD /bin/bash ./entrypoint.sh
ENTRYPOINT ["dotnet", "PHIASPACE.CORE.dll"]