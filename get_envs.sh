#!/bin/bash

cd /home/Flats4us

COMMIT_HASH=$(git rev-parse HEAD)
COMMIT_DATE=$(git log -1 --format=%cd --date=format:"%Y-%m-%d %H:%M:%S")

sed -i "s/##COMMIT_HASH##/$COMMIT_HASH/g" src/environments/environment.prod.ts
sed -i "s/##COMMIT_DATE##/$COMMIT_DATE/g" src/environments/environment.prod.ts