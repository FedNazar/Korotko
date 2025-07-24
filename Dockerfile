ARG DOTNET_VERSION=9.0
ARG BUILD_CONFIG=Release

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION AS sdk

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /src

COPY ["Korotko.API/", "Korotko.API/"]
COPY ["Korotko.Application/", "Korotko.Application/"]
COPY ["Korotko.Infrastructure/", "Korotko.Infrastructure/"]
COPY ["Korotko.Domain/", "Korotko.Domain/"]

WORKDIR /src/Korotko.API

RUN dotnet publish -c "$BUILD_CONFIG" -o "app"

# Fake MariaDB info so dotnet-ef would shut up
ENV MARIADB_ROOT_PASSWORD=12345678
ENV ConnectionStrings__MariaDB=server=localhost;user=root;password=12345678;database=dummy
ENV MariaDB__OverrideVersion=true
ENV MariaDB__VersionMajor=8
ENV MariaDB__VersionMinor=0
ENV MariaDB__VersionPatch=0

RUN dotnet-ef migrations bundle -o "app/efbundle"

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION AS base

COPY --from=sdk src/Korotko.API/app .
COPY --from=sdk src/Korotko.API/entrypoint.sh entrypoint.sh

RUN chmod +x entrypoint.sh
RUN chmod +x efbundle

ENTRYPOINT ["./entrypoint.sh"]
