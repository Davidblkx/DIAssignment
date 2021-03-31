#!/bin/bash

CURRENT=$PWD
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"
cd $DIR

docker build -t dpires/base .

cd $CURRENT