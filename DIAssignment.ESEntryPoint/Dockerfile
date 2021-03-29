#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DIAssignment.ESEntryPoint/DIAssignment.ESEntryPoint.csproj", "DIAssignment.ESEntryPoint/"]
RUN dotnet restore "DIAssignment.ESEntryPoint/DIAssignment.ESEntryPoint.csproj"
COPY . .
WORKDIR "/src/DIAssignment.ESEntryPoint"
RUN dotnet build "DIAssignment.ESEntryPoint.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DIAssignment.ESEntryPoint.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DIAssignment.ESEntryPoint.dll"]