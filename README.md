# AdamServices.Extensions.DefaultArgumets
[![.NET Publish Nuget Package And Release](https://github.com/Adam-Software/AdamServices.Extensions.DefaultArgumets/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/Adam-Software/AdamServices.Extensions.DefaultArgumets/actions/workflows/dotnet-desktop.yml)     
![GitHub License](https://img.shields.io/github/license/Adam-Software/AdamServices.Extensions.DefaultArgumets)
![GitHub Release](https://img.shields.io/github/v/release/Adam-Software/AdamServices.Extensions.DefaultArgumets)
![NuGet Version](https://img.shields.io/nuget/v/AdamServices.Extensions.DefaultArgumets)

A library for parsing command line arguments. It automatically generates and displays the application version, application name, and copyright. It also adds two parameters `--help` and `--version`. If you use custom arguments, help will be automatically generated for them. Is an extension for Microsoft Dependency Injection (DI).

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

* Add DefaultArguments initialization to the service configurations. Default arguments will be added when using both methods. Read more [here](#default-argument)
   
  **Method 1.** If the application does not have command line options
  ```c#
  .ConfigureServices((context, services) =>
  {
     services.AddAdamDefaultArgumentsParser(args);  
  })
  ```
  This will add default command line options such as `--help` and `--version`.

  **Method 2.** If the application has command line options you should add custom arguments class. Learn more about custom arguments class [here](#example-custom-arguments-class).     
  ```c#
  .ConfigureServices((context, services) =>
  {
     services.AddAdamArgumentsParserTransient<ArgumentService>(args);
  })
  ```  
  This will add a transient service `ArgumentService` which can be obtained using standard methods via `ServiceProvider`. 
  ```c#
  var userArguments = host.Services.GetService<ArgumentService>();
  ```

* Change the way the host is started, such as `host.Run()` or `host.RunAsync()` on the `host.ParseAndRun()` or `host.ParseAndRunAsync()`. This will change the behavior of the host depending on the result of parsing command line parameters. Read more about it [here](#host-behavior-when-parsing-arguments).

An example can be viewed in the [test](https://github.com/Adam-Software/AdamServices.Extensions.DefaultArgumets/tree/master/src/DefaultArguments.TestApp) project.

### Default argument

* `--help`
  
  When used without specifying the custom arguments class, the --help argument will display
  ```cmd
  AppName AppVersion
  Copyright (C) 2025 AppCopyright
  
  --help       Display this help screen.
  --version    Display version information.
  ```
  
  When used wit specifying the custom arguments class, ex. [ArgumentService](#example-custom-arguments-class), the --help argument will display
    ```cmd
  AppName AppVersion
  Copyright (C) 2025 AppCopyright

  -s --test    Test
  -q --test2   Test2
  --help       Display this help screen.
  --version    Display version information.
  ```
  
* `--version`    
  Returns the application version in the format `Version.Major`.`Version.Minor`.`Version.Build`. **The `revision` version will not be displayed, even if it is specified in the project.**

### Custom arguments class
The parameter class consists of fields marked with the `Option` attribute. The attribute parameters are described [here](https://github.com/commandlineparser/commandline/wiki/Option-Attribute)

#### Limitations of the parameter class
* The command line parameter class must be marked with the `CommandLine.VerbAttribute` with the optional `IsDefault` parameter: `true`
  ```c#
  [Verb("arguments", isDefault: true)]
  ```
* The command line parameter class must be public.
* The parameter class must not have constructors or have a constructor without parameters.

#### Example custom arguments class
```C#

[Verb("arguments", isDefault: true)]
public class ArgumentService
{
    [Option(shortName: 's', longName: "test", Required = false, HelpText = "Test")]
    public bool Test { get; set; }

    [Option(shortName: 'q', longName: "test2", Required = false, HelpText = "Test2")]
    public bool Test2 { get; set; }
}

```
### Host behavior when parsing arguments

Successful parsing
* the host continues to work
* the custom arguments class fields are filled with values from the command line

Unsuccessful parsing
* the parser shows the argument that caused the error.
* the parser shows the automatically generated help
* the host stops working

When parsing the default arguments --help and --version
* the parser shows the automatically generated help or version
* the host stops working

## For developers

### Publishing releases

To publish a release, you need to:

* Upgrade the version in the project configuration and commit the changes
  ```xml
  <PropertyGroup>
    ...
    <Version>1.0.1</Version>
    ...
  </PropertyGroup>
  ```
  Version format X.X.X
* Mark the commit with a tag of the format: v.X.X.X
  e.g. version 1.0.1 tag v.1.0.1
* Push commits and tags. The release will be published automatically.

Important!

* Technically, it doesn't matter which version is specified in the project configuration, the version number is taken from the tag. They may differ, for example, as a result of an error or carelessness. Preference should be given to the version specified in the tag.
* The trigger for publishing a release is a xxx format tag. From any branch.

### What's going on at CI?

* Building a library with a project and a test application to which the library is linked via a link to the project
* Packaging the library in a nuget package
* Launching a test application that creates a file, services_info.json with fields filled in by default
* Publishing packages on nuget and github package
* Publication of the release to which the following are attached: source codes, nuget package version, services_info.json file with fields filled in by default

## Thanks
[@ericnewton76](https://github.com/ericnewton76) for [commandlineparser](https://github.com/commandlineparser)
