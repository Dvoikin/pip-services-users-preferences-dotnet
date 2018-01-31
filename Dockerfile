# Build stage
FROM microsoft/aspnetcore-build:2 AS build

# set working directory
WORKDIR /app

# Restore
COPY run/run.csproj ./run/
RUN dotnet restore run/run.csproj
COPY src/src.csproj ./src/
RUN dotnet restore src/src.csproj
COPY test/test.csproj ./test/
RUN dotnet restore test/test.csproj

# Copy src
COPY . .

# Test
ENV TEAMCITY_PROJECT_NAME=fake
#RUN dotnet test tests/tests.csproj

# Publish
RUN dotnet publish run/run.csproj -o /obj

# Runtime stage
FROM microsoft/aspnetcore:2

# set working directory
WORKDIR /app

# Copy compiled binaries
COPY --from=build /obj ./bin

# Copy configuration
COPY config/*.* ./config/

ENV MONGO_SERVICE_URI ""
ENV MONGO_SERVICE_HOST mongo
ENV MONGO_SERVICE_PORT 27017
ENV MONGO_DB app

EXPOSE 8080

CMD ["dotnet", "./bin/run.dll", "-c", "./config/config.yml"]