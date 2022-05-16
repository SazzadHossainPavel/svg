# SVG APP

This is a ASP.NET Core and Angular project for creating SVG figure of rectangles.

## Run SVG Web API's

Navigate to `svgApi` folder. Using dotnet cli run `dotnet run`

## Run Client app

Navigate to `NgSvg` folder. Run `ng serve -o`

# Additional Commands

## Install SQL server on M1 Mac

`sudo docker pull mcr.microsoft.com/azure-sql-edge:latest`

## Run SQL Server with docker

`docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=Sqlm1@123' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge`
