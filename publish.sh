#!/bin/bash

COMPONENT=$(ls *.nuspec | tr -d '\r' | awk -F. '{ print $1 }')
VERSION=$(grep -m1 "<version>" *.nuspec | tr -d '\r' | sed 's/[ ]//g' | awk -F ">" '{ print $2 }' | awk -F "<" '{ print $1 }')
IMAGE1="pipdevs/${COMPONENT}:${VERSION}-${BUILD_NUMBER-0}"
IMAGE2="pipdevs/${COMPONENT}:latest"
TAG="v${VERSION}-${BUILD_NUMBER-0}"

# Any subsequent(*) commands which fail will cause the shell scrupt to exit immediately
set -e
set -o pipefail

# Set tag on git repo
git tag $TAG
git push --tags

# Push production image to docker registry
docker login -u $DOCKER_USER -p $DOCKER_PASS
docker push $IMAGE1
docker push $IMAGE2

