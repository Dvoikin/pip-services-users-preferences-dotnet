#!/bin/bash

COMPONENT=$(ls *.nuspec | tr -d '\r' | awk -F. '{ print $1 }')
VERSION=$(grep -m1 "<version>" *.nuspec | tr -d '\r' | sed 's/[ ]//g' | awk -F ">" '{ print $2 }' | awk -F "<" '{ print $1 }')
IMAGE="pipdevs/${COMPONENT}:${VERSION}-build"
CONTAINER="${COMPONENT}"

# Any subsequent(*) commands which fail will cause the shell script to exit immediately
set -e
set -o pipefail

# Remove build files
rm -rf ./obj

# Build docker image
docker build -f Dockerfile.build -t ${IMAGE} .

# Create and copy compiled files, then destroy
docker create --name ${CONTAINER} ${IMAGE}
docker cp ${CONTAINER}:/obj ./obj
docker rm ${CONTAINER}