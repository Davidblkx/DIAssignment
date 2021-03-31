$current = Get-Location
Set-Location -Path $PSScriptRoot -PassThru

docker build -t dpires/base .

Set-Location -Path $current -PassThru