# builds all aot binaries

.PHONY: all

all:
	dotnet publish -o Binaries/osx-x64 -r osx-x64 -c Release -f net6.0  --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=True 
	dotnet publish -o Binaries/win-x64 -r win-x64 -c Release -f net6.0 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=True 
	dotnet publish -o Binaries/linux-x64 -r linux-x64 -c Release -f net6.0 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=True 


