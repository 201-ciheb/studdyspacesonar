#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5159
EXPOSE 7056

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PHIASPACE.TRAINING/PHIASPACE.TRAINING.csproj", ""]
RUN dotnet restore "./PHIASPACE.TRAINING.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PHIASPACE.TRAINING/PHIASPACE.TRAINING.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PHIASPACE.TRAINING/PHIASPACE.TRAINING.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PHIASPACE.TRAINING.dll"]