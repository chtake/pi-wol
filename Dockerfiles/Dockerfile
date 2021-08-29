FROM --platform=${TARGETPLATFORM} mcr.microsoft.com/dotnet/aspnet:5.0.9-buster-slim
ARG TARGETARCH

RUN apt-get update -y && \
    apt-get upgrade -y && \
    apt-get install -y net-tools && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app

COPY ./publish-${TARGETARCH}/publish .

VOLUME ["/app/data"]
EXPOSE 80/tcp

ENTRYPOINT [ "dotnet", "PiWol.WebApp.dll" ]