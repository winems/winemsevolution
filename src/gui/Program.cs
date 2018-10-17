using System;
using System.Windows.Forms;
using RadiusCSharp.Core.AppSettings;
using RadiusCSharp.Core.Bootstrap;
using RadiusCSharp.Core.FileSystem;
using RadiusCSharp.Log4Net.Logging;
using RadiusCSharp.WinForms.Dialogs;

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
            Log4NetLoggingFactory.BindLog4Net(
              Log4NetLoggingFactory.DefaultConfig(
                "log-folder".AppSetting()
                            .UseFolderOrDefault(
                              @"C:\Neurasoft\logs\WineMS\Evolution"),
                "winems-evolution-log.txt"));
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