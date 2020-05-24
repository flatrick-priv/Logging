# Logging
My first attempt at a logging-library

## Setup

1. Add configuration for the main project (see example below)
1. Add library to main project
1. Load library in Main()
1. Run `Logs.SetupLogging()` for it to read config in App.Config and setup Endpoints
1. For any class that wants to use it, add `using Logging;`
1. To log any message, write `Logs.LogMessage("message", LogLevel.LEVEL)`

## Available LogLevels

* Critical
* Error
* Warning
* Information
* Debug
* Tracing

## Example App.config for main application

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section  name="Logging"
              type="Logging.Configuration.LoggingConfig, Logging" />
  </configSections>
  <Logging>
    <endpoints>
      <!-- <add  endpoint="EventLog"
            path="Application"
            logLevel="Information"/> -->
      <add endpoint="LogToText"
           path="%userprofile%\desktop\logs"
           logLevel="Trace"/>
    </endpoints>
  </Logging>
</configuration>
```
