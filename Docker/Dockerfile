FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

COPY config.json /app/config.json

# Default env variables
ENV CLUSTER_SEEDS "[]"
ENV CLUSTER_IP ""
ENV CLUSTER_PORT "5213"
ENV ACTORSYSTEM "dia"

# Akka.Remote inbound listening endpoint
EXPOSE 5213

# Copy entrypoint script
COPY Docker/entry.sh /entry.sh
RUN chmod +x /entry.sh

# Create temp directory
RUN mkdir -p /tempapp
RUN chmod 777 /tempapp

ENTRYPOINT ["/bin/bash","/entry.sh"]