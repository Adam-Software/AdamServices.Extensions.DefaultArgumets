# AdamServices.Extensions.DefaultArgumets
[![.NET Publish Nuget Package And Release](https://github.com/Adam-Software/AdamServices.Extensions.DefaultArgumets/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/Adam-Software/AdamServices.Extensions.DefaultArgumets/actions/workflows/dotnet-desktop.yml)     
![GitHub License](https://img.shields.io/github/license/Adam-Software/AdamServices.Extensions.DefaultArgumets)
![GitHub Release](https://img.shields.io/github/v/release/Adam-Software/AdamServices.Extensions.DefaultArgumets)
![NuGet Version](https://img.shields.io/nuget/v/AdamServices.Extensions.DefaultArgumets)

The library for parsing command line arguments of Adam Services projects is an extension for Microsoft Dependency Injection (DI)

Use the shared [wiki](https://github.com/Adam-Software/AdamServices.Utilities.Managment/wiki) to find information about the project.

## For users

### Install

.NET CLI
```cmd
dotnet add package AdamServices.Extensions.DefaultArguments
```

Package Manager
```cmd
NuGet\Install-Package AdamServices.Extensions.DefaultArguments
```

### Update the configuration of the DI project

* Add DefaultArguments initialization to the service configurations
   
  **Method 1.** If the application does not have command line options
  ```c#
  .ConfigureServices((context, services) =>
  {
     services.AddAdamDefaultArgumentsParser(args);  
  })
  ```
  This will add default command line options such as `--help` and `--version`.

  **Method 2.** If the application has command line options
  ```c#
  .ConfigureServices((context, services) =>
  {
     services.AddAdamArgumentsParserTransient<ArgumentService>(args);
  })
  ```
  `ArgumentService` is the `CommandLine.VerbAttribute` class.
