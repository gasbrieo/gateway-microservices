FROM golang:1.21-alpine AS build

WORKDIR /app
COPY . .

RUN go build -o metrics-service

FROM alpine:latest

WORKDIR /app
COPY --from=build /app/metrics-service .

EXPOSE 7000
ENTRYPOINT ["./metrics-service"]
