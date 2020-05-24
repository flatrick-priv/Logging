# Logging
My first attempt at a logging-library

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
