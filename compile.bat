dotnet publish -c Release
IF EXIST bin\Release\net8.0\publish %SystemRoot%\explorer.exe "bin\Release\net8.0\publish"
PAUSE