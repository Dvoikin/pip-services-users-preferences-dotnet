#!/bin/bash

COMPONENT=$(ls *.nuspec | tr -d '\r' | awk -F. '{ print $1 }')
VERSION=$(grep -m1 "<version>" *.nuspec | tr -d '\r' | sed 's/[ ]//g' | awk -F ">" '{ print $2 }' | awk -F "<" '{ print $1 }')
BUILD_IMAGE="pipdevs/${COMPONENT}:${VERSION}-build"
TEST_IMAGE="pipdevs/${COMPONENT}:${VERSION}-test"

rm -rf ./obj
rm -rf ./run/bin
rm -rf ./run/obj
rm -rf ./src/bin
rm -rf ./src/obj
rm -rf ./test/bin
rm -rf ./test/obj
rm -rf *.nupkg

docker rmi $BUILD_IMAGE --force
docker rmi $TEST_IMAGE --force
docker image prune --force