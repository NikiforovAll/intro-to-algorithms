#!/usr/bin/env bash

dotnet build ./src/IntroToAlgorithms/ -c Release -f netcoreapp3.1
dotnet tool install -g dotnet-try
dotnet try verify .
