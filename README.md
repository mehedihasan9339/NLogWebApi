# NLog Configuration for ASP.NET Core Web API

## Overview

This project uses **NLog**, a logging framework for .NET applications. NLog helps capture and manage logs effectively, making it easier to troubleshoot issues and monitor application behavior.

## What is NLog?

NLog is an open-source logging library for .NET that allows developers to log information in various formats and send it to different targets (like files, databases, or consoles).

### Key Features of NLog

- **Flexible Targeting**: Log to multiple targets (e.g., files, console).
- **Structured Logging**: Capture detailed log messages for easier analysis.
- **Asynchronous Logging**: Improves performance by logging in the background.
- **Rich Configuration**: Easily customizable through XML configuration files.
- **Internal Logging**: Helps troubleshoot logging issues.

## Why Use NLog?

Using NLog offers several benefits:

- **Performance**: Asynchronous logging minimizes the impact on application speed.
- **Flexibility**: Change logging targets and formats without code changes.
- **Maintainability**: Structured logs make debugging simpler.
- **Community Support**: Extensive documentation and community help.

## Configuration Details

### Configuration File

The NLog configuration is defined in the `nlog.config` file, which specifies logging behavior.

#### Key Components

1. **Targets**:
   - **File Target**: Logs messages to a daily rolling file.
   - **Console Target**: Outputs logs to the console for immediate visibility.
   - **Asynchronous Wrapper**: Improves performance with background logging.

2. **Rules**:
   - Configures which loggers to capture and their minimum log levels.

3. **Internal Logging**: Records internal operations of NLog.

### Example Configuration

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <targets>
    <target xsi:type="File" name="logfile" 
            fileName="C:/MyDrive/NLogWebApi/NLogWebApi/logs/${shortdate}.log"
            layout="${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=ToString}"
            archiveEvery="Day" 
            archiveNumbering="Date" 
            maxArchiveFiles="30" 
            concurrentWrites="true" 
            keepFileOpen="false" />
    
    <target xsi:type="Console" name="console" 
            layout="${longdate}|${level:uppercase=true}|${logger}|${message}" />

    <target xsi:type="AsyncWrapper" name="asyncFile" queueLimit="1000" flushTimeout="5000">
      <target xsi:type="File" name="logfile" 
              fileName="C:/MyDrive/NLogWebApi/NLogWebApi/logs/${shortdate}.log"
              layout="${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=ToString}"
              archiveEvery="Day" 
              archiveNumbering="Date" 
              maxArchiveFiles="30" 
              concurrentWrites="true" 
              keepFileOpen="false" />
    </target>
  </targets>

  <rules>
    <logger name="*" minlevel="Debug" writeTo="asyncFile,console" />
    <logger name="Microsoft*" minlevel="Warning" writeTo="asyncFile" />
  </rules>

  <internalLogFile>logs/nlog-internal.log</internalLogFile>
  <internalLogLevel>Trace</internalLogLevel>
</nlog>
