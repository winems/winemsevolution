using RadiusCSharp.Core.AppSettings;
using RadiusCSharp.Core.Bootstrap;
using RadiusCSharp.Core.FileSystem;
using RadiusCSharp.Log4Net.Logging;
using WineMS.Common;
using WineMsEvolutionCli.Extensions;

namespace WineMsEvolutionCli {

  class Program {

    static void Main(string[] args)
    {
      RadiusProgramMainFunctions
        .ProgramMain(
          "winems-evolution-processing-cli",
          () => {
            Log4NetLoggingFactory.BindLog4Net(
              Log4NetLoggingFactory.DefaultConfig(
                "log-folder"
                  .AppSetting()
                  .UseFolderOrDefault(
                    @"C:\Neurasoft\logs\WineMS\Evolution"),
                "winems-evolution-log.txt"));
            CommonInitFunctions.Init();
          },
          () =>
          {
            ApplicationInformationFunctions.LogApplicationInformation(
              ApplicationInformationFunctions.GetApplicationInformation());

            CommandLineExecuteFunctions.Execute(args);

          },
          exception => { },
          () => { },
          RadiusProgramMainFunctions.FinallyPause.IfDebuggerAttached);
    }

  }

}