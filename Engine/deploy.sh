#!/bin/bash
git checkout $1
git pull
sudo service slashedbot stop
sudo -u iksaku dotnet restore
sudo -u iksaku dotnet publish -c Release
sudo service slashedbot start