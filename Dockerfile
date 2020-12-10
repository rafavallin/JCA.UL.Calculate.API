FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY . ./
WORKDIR /app/JCA.UL.Calculate.API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build /app/JCA.UL.Calculate.API/out .
ENTRYPOINT [ "dotnet", "JCA.UL.Calculate.API.dll"] 