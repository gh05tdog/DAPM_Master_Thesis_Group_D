#!/bin/bash

DIR1="DAPM"
DIR2="DAPM-Frontend"

BUILD=false

while [[ "$#" -gt 0 ]]; do
    case $1 in
        --build) BUILD=true ;;
        *) echo "Unknown parameter passed: $1"; exit 1 ;;
    esac
    shift
done


cd "$DIR1"
if [ "$BUILD" = true ]; then
    echo "Running docker-compose up --build in $DIR"
    docker-compose up --build -d
else
    echo "Running docker-compose up in $DIR"
    docker-compose up -d
fi

cd "../$DIR2"
if [ "$BUILD" = true ]; then
    echo "Running docker-compose up --build in $DIR"
    docker-compose up --build -d
else
    echo "Running docker-compose up in $DIR"
    docker-compose up -d
fi

echo "Docker compose done for both directories."
