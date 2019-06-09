#!/bin/bash

if [ -n "${IS_CI_BUILD}" ]
then
    echo "CI BUILD"
else
    echo "LOCAL BUILD"
fi

APP_VERSION=`dotnet-gitversion /output json /showvariable NuGetVersion`
build_date=$(date '+%Y-%m-%d %H:%M:%S')

if [ -n "${IS_CI_BUILD}" ]
then
    base_dir=${APPVEYOR_BUILD_FOLDER}
    build_version=${APP_VERSION}-${APPVEYOR_BUILD_NUMBER}.${APPVEYOR_REPO_COMMIT:0:8}
    IS_TAG=${APPVEYOR_REPO_TAG}
else
    base_dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && cd .. >/dev/null 2>&1 && pwd )"
    build_version="0.0.0-local"
    DOCKER_REPO="chtake/pi-wol"
    IS_TAG=false
fi

for arg in "$@"
do
    declare "task_$arg=true"
done

# prepare agent for arm builds
if [ -n "${IS_CI_BUILD}" ]
then
    docker run --rm --privileged hypriot/qemu-register
fi

cd ${base_dir}

# build
function build {
    runtime=$1
    runtime_short=$2

    dotnet restore
    dotnet publish ${base_dir}/src/PiWol.WebApp/PiWol.WebApp.csproj -c release -r ${runtime} -o ${base_dir}/publish-${runtime_short}/publish
    cp Dockerfiles/Dockerfile_${runtime_short} ${base_dir}/publish-${runtime_short}/Dockerfile
    cp README.md ${base_dir}/publish-${runtime_short}/
    cp docker-compose.yml ${base_dir}/publish-${runtime_short}/

    if [ -n "${IS_CI_BUILD}" ]
    then
        7z a artifact-${runtime_short}.zip ${base_dir}/publish-${runtime_short}/*
    else
        tar -cf artifact-${runtime_short}.tar publish-${runtime_short}/*
    fi
}

# test
function test {
    dotnet restore
    dotnet test -r ${base_dir}/test-results --logger:trx
}

# targets
function target_build {
    echo "Patching version.ts -- export const version = { version: '${build_version}', buildDate: '${build_date}' };" 
    
    echo "export const version = { version: '${build_version}', buildDate: '${build_date}' };" \
        > ${base_dir}/src/PiWol.WebApp/ClientApp/src/app/version.ts

    # build amd64
    echo "Building amd64-${APP_VERSION}"
    build linux-x64 amd64

    # build arm
    echo "Building arm-${APP_VERSION}"
    build linux-arm arm
}

function target_test {
    test
}

function target_deploy_develop {
    docker build -t ${DOCKER_REPO}:arm-develop-${APP_VERSION} publish-arm/
    docker build -t ${DOCKER_REPO}:amd64-develop-${APP_VERSION} publish-amd64/

    docker push ${DOCKER_REPO}:arm-develop-${APP_VERSION}
    docker push ${DOCKER_REPO}:amd64-develop-${APP_VERSION}

    docker -D manifest create ${DOCKER_REPO}:develop ${DOCKER_REPO}:arm-develop-${APP_VERSION} ${DOCKER_REPO}:amd64-develop-${APP_VERSION}
    docker -D manifest create ${DOCKER_REPO}:develop-${APP_VERSION} ${DOCKER_REPO}:arm-develop-${APP_VERSION} ${DOCKER_REPO}:amd64-develop-${APP_VERSION}
    
    docker manifest push -p ${DOCKER_REPO}:develop
    docker manifest push -p ${DOCKER_REPO}:develop-${APP_VERSION}
}

function target_deploy_release {
    docker build -t ${DOCKER_REPO}:arm-${APP_VERSION} publish-arm/
    docker build -t ${DOCKER_REPO}:amd64-${APP_VERSION} publish-amd64/

    docker push ${DOCKER_REPO}:arm-${APP_VERSION}
    docker push ${DOCKER_REPO}:amd64-${APP_VERSION}

    docker -D manifest create ${DOCKER_REPO}:latest ${DOCKER_REPO}:arm-${APP_VERSION} ${DOCKER_REPO}:amd64-${APP_VERSION}
    docker -D manifest create ${DOCKER_REPO}:${APP_VERSION} ${DOCKER_REPO}:arm-${APP_VERSION} ${DOCKER_REPO}:amd64-${APP_VERSION}

    docker manifest push -p ${DOCKER_REPO}:latest
    docker manifest push -p ${DOCKER_REPO}:${APP_VERSION}
}

# run tasks

if [ -n "${task_build}" ]
then
    target_build
fi

if [ -n "${task_test}" ]
then
    target_test
fi

if [ -n "${task_deploy_develop}" ]
then
    target_deploy_develop
fi

if [ -n "${task_deploy_release}" ]
then
    if [ -n "${IS_TAG}" ] && [ "${IS_TAG}" == "true" ]; then
        target_deploy_release
    fi
fi