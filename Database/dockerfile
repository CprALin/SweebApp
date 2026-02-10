 
# Use the official Microsoft SQL Server 2022 image as the base image
FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=${DB_PASSWORD}

# Expose SQL Server port
EXPOSE ${PORT}

# use docker build to build the image : e.g docker build -t sweebapp_db_image .
