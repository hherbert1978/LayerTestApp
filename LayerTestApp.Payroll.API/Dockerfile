#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LayerTestApp.Payroll.API/LayerTestApp.Payroll.API.csproj", "LayerTestApp.Payroll.API/"]
COPY ["LayerTestApp.Payroll.BAL/LayerTestApp.Payroll.BAL.csproj", "LayerTestApp.Payroll.BAL/"]
COPY ["LayerTestApp.Common/LayerTestApp.Common.csproj", "LayerTestApp.Common/"]
COPY ["LayerTestApp.Payroll.DAL/LayerTestApp.Payroll.DAL.csproj", "LayerTestApp.Payroll.DAL/"]
RUN dotnet restore "LayerTestApp.Payroll.API/LayerTestApp.Payroll.API.csproj"
COPY . .
WORKDIR "/src/LayerTestApp.Payroll.API"
RUN dotnet build "LayerTestApp.Payroll.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LayerTestApp.Payroll.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LayerTestApp.Payroll.API.dll"]