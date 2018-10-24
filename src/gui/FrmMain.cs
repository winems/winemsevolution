using System;
using System.Windows.Forms;
using CSharpFunctionalExtensions;
using RadiusCSharp.Core.Logging;
using RadiusCSharp.WinForms.Dialogs;
using WineMsEvolutionGui.Extensions;
using WineMS.BusinessLogic;
using WineMS.BusinessLogic.Extensions;
using WineMS.Common;

namespace WineMsEvolutionGui {

  public partial class FrmMain : Form {

    private BackgroundWorkerCancellationProvider _runningProcesCancelProvider;

    public FrmMain() { InitializeComponent(); }

    private void FrmMain_Load(object sender, EventArgs e) { LoadStatusBarInformation(); }

    private void LoadStatusBarInformation()
    {
      BackgroundWorkerFunctions
        .Execute(
            context =>
            {
              context.Result = ApplicationInformationFunctions.GetApplicationInformation();
            },
            context =>
            {
              var (version, wineMsDatabase) =
                ((string, string)) context.Result;
              tsVersion.Text = version;
              tsWineMSDatabase.Text = wineMsDatabase;
            }
          );
    }

    private void mniExit_Click(object sender, EventArgs e) { Close(); }

    private void mniProcessGeneralLedger_Click(object sender, EventArgs e)
    {
      RunProcess(WineMsTransactionFunctions.ProcessGeneralLedgerTransactions);
    }

    private void mniProcessSalesOrders_Click(object sender, EventArgs e)
    {
      RunProcess(WineMsTransactionFunctions.ProcessSalesOrderTransactions);
    }

    private void mniProcessPurchaseOrders_Click(object sender, EventArgs e)
    {
      RunProcess(WineMsTransactionFunctions.ProcessPurchaseOrderTransactions);
    }

    private void mniCancel_Click(object sender, EventArgs e)
    {
      if ("Cancel process".ShowAskYesNo("Are you sure?") == YesNoResponse.No) return;
      _runningProcesCancelProvider?.Cancel();
    }

    private void RunProcess(Func<IBackgroundWorker, Result> process)
    {
      if (_runningProcesCancelProvider != null)
        return;

      SetMenuState(ProcessRunningState.Start);
      tsProgressBar.Value = 0;
      tsProgressBar.Visible = true;

      _runningProcesCancelProvider =
        BackgroundWorkerFunctions
          .Execute(
            context =>
            {
              try {
                process(context.Sender)
                  .OnFailure(
                    error => { error.ShowExceptionDialog(); });
              }
              catch (Exception e) {
                e.LogAndShowExceptionDialog();
              }
            },
            context =>
            {
              _runningProcesCancelProvider = null;
              "Process completed.".ShowInformationMessage();
              SetMenuState(ProcessRunningState.Stop);
              tsProgressBar.Visible = false;
            },
            context =>
            {
              tsProgressBar.Value = context.Args.ProgressPercentage;
              tsProgressBar.ProgressBar?.Update();
            });
    }

    private void SetMenuState(ProcessRunningState state)
    {
      var stopped = state == ProcessRunningState.Stop;
      mniFile.Enabled = stopped;
      mniProcessGeneralLedger.Enabled = stopped;
      mniProcessPurchaseOrders.Enabled = stopped;
      mniProcessSalesOrders.Enabled = stopped;
      mniCancel.Enabled = !stopped;
    }

  }

  internal enum ProcessRunningState {

    Start,
    Stop

  }

}