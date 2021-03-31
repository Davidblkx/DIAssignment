FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

# Default env variables
ENV CLUSTER_SEEDS "[]"
ENV CLUSTER_IP ""
ENV CLUSTER_PORT "5213"
ENV ACTOR_SYSTEM_NAME "DIA"

# Akka.Remote inbound listening endpoint
EXPOSE 5213

# Copy entrypoint script
COPY /entry.sh /entry.sh
RUN chmod +x /entry.sh

ENTRYPOINT ["/bin/bash","/entry.sh"]