#!/bin/bash

if [ -z "$1" ]; then
    echo "Usage: ./create <day-number>"
    exit 1
fi

# Pad day number to 2 digits
DAY=$(printf "%02d" "$1")
FOLDER="Day$DAY"

echo "Creating $FOLDER..."

# Create folder
mkdir -p "$FOLDER"

# Create new console project
dotnet new console -o "$FOLDER"

# Add to solution
dotnet sln add "$FOLDER/$FOLDER.csproj"

echo "$FOLDER created and added to solution!"
