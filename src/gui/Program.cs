using System;
using System.Windows.Forms;
using RadiusCSharp.Core.AppSettings;
using RadiusCSharp.Core.Bootstrap;
using RadiusCSharp.Core.FileSystem;
using RadiusCSharp.Log4Net.Logging;
using RadiusCSharp.WinForms.Dialogs;
using WineMS.Common;

namespace WineMsEvolutionGui {

  static class Program {

    [STAThread]
    static void Main()
    {
      RadiusProgramMainFunctions
        .ProgramMain(
          "winems-evolution-processing",
          () =>
          {
            ApplicationState.LogFolder =
              "log-folder"
                .AppSetting()
                .UseFolderOrDefault(
                  @"C:\Neurasoft\logs\WineMS\Evolution");

            Log4NetLoggingFactory.BindLog4Net(
              Log4NetLoggingFactory.DefaultConfig(
                ApplicationState.LogFolder,
                "winems-evolution-log.txt"));
            CommonInitFunctions.Init();
          },
          () =>
          {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
          },
          exception => { exception.LogAndShowExceptionDialog(); },
          () => { });
    }

  }

}