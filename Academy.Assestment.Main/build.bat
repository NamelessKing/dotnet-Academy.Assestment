@echo off

echo Publish project...
dotnet publish Academy.Assestment.Main.csproj -o bin/Publish -c Release

echo Build image
docker build -t academy-assestment-app-image:1.0 .

echo Tag latest image
docker tag academy-assestment-app-image:1.0 academy-assestment-app-image:latest

echo Completed