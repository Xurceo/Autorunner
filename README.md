# Autorunner
Simple app for running other apps and commands when it itself starting.
Running apps like Chrome, discord and even composing docker images when starting.

## Build from source
One file and add to startup.

### Requirements
- Windows Desktop workload
- .NET SDK 10.x

```bash
dotnet publish MySolution.sln -c Release -r win-x64 --self-contained true -f net10.0-windows
```

## TODO List
```brainfuck
[ ] - Custom Icons
[ ] - Profiles
More later...
```
