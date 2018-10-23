
Logging
-------

If you do not explicitly implement logging a default NullLogger will be used. This logger does nothing.

To implement logging do one of the following:

1) Install the RadiusCSharp.Log4Net package. After installing this package see the Readme-log4net.txt file for
   implementation details.

2) Implement and register a custom logging factory by assigning a function to LoggingFactory.NewLoggerContext.
   Your custom function must return an instance of LoggerContext which needs to provide functions for logging.
