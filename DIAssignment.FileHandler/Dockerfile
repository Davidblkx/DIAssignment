#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DIAssignment.FileHandler/DIAssignment.FileHandler.csproj", "DIAssignment.FileHandler/"]
RUN dotnet restore "DIAssignment.FileHandler/DIAssignment.FileHandler.csproj"
COPY . .
WORKDIR "/src/DIAssignment.FileHandler"
RUN dotnet build "DIAssignment.FileHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DIAssignment.FileHandler.csproj" -c Release -o /app/publish

FROM dpires/base AS final
COPY --from=publish /app/publish .
CMD ["dotnet", "DIAssignment.FileHandler.dll"]