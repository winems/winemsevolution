To implement log4net do the following during application start up:

This will bootstrap log4net using a predefined set of appenders.

	Log4NetLoggingFactory.BindLog4Net(
		new Log4NetDefaultConfig {
			LogFileRootPath = @"C:\Neurasoft\Logs\nfx",
			LogFileName = "composite-log.txt",
			Levels = Log4NetLoggingFactory.DefaultLevels,
			AddFileAppender = true,
			AddConsoleAppender = true
		});

Set the above properties to values relevant to the application.


To make use of a log4net.config do the following:
- Add a log4net.config to your project.
- Do the following call. Optionally pass the folder name where the log4net.config file is.

	Log4NetLoggingFactory.BindLog4Net();

	or

	Log4NetLoggingFactory.BindLog4Net("c:\logging");
