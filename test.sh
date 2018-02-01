#!/bin/bash

COMPONENT=$(ls *.nuspec | tr -d '\r' | awk -F. '{ print $1 }')
VERSION=$(grep -m1 "<version>" *.nuspec | tr -d '\r' | sed 's/[ ]//g' | awk -F ">" '{ print $2 }' | awk -F "<" '{ print $1 }')
IMAGE="pipdevs/${COMPONENT}:${VERSION}-test"

# Any subsequent(*) commands which fail will cause the shell script to exit immediately
set -e
set -o pipefail

# Workaround to remove dangling images
docker-compose -f ./docker-compose.test.yml down

export IMAGE
docker-compose -f ./docker-compose.test.yml up --build --abort-on-container-exit --exit-code-from test

# Workaround to remove dangling images
docker-compose -f ./docker-compose.test.yml down