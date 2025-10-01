
COPY ./*.sln ./
COPY Test/Test.csproj Test/Test.csproj
RUN  dotnet restore ./Test.csproj


WORKDIR /src
COPY . ./
RUN  dotnet restore ./Test.csproj 


# Build and publish the solution
RUN dotnet publish Test/Test.csproj -c Release -o /app/publish

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Test.dll"]
