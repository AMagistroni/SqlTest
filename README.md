# SqlTest
Test of sql query.
When a query found any result, an error file will be created.

# Configuration:

Insert in your secrets the connectionstring of your database

```csharp
{
  "ConnectionStrings": {
    "ConnectionString" :  "Test"
  }
}
```

In the appsettings.json

- Directories: Comma separated strings of directories where sql test can found sql query file
- DisableFileStart: Start string of files that will be ignored
- DirBase: Directory that sql test use to write results.
- FileName: Name of the file error.

