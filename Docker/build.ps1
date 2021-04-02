$current = Get-Location
Set-Location -Path $PSScriptRoot -PassThru

docker build -t dpires/base -f ./Dockerfile ..

Set-Location -Path $current -PassThru