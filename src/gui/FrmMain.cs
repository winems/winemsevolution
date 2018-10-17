﻿using System;
using System.Windows.Forms;
using RadiusCSharp.WinForms.Dialogs;
using WineMsEvolutionGui.Extensions;
using WineMS.BusinessLogic.GeneralLedger;
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
              var (version, wineMsDatabase, evolutionDatabase) =
                ((string, string, string)) context.Result;
              tsVersion.Text = version;
              tsWineMSDatabase.Text = wineMsDatabase;
              tsEvolutionDatabase.Text = evolutionDatabase;
              ApplicationInformationFunctions
                .LogApplicationInformation(((string, string, string)) context.Result);
            }
          );
    }

    private void mniExit_Click(object sender, EventArgs e) { Close(); }

    private void mniProcessGeneralLedger_Click(object sender, EventArgs e)
    {
      RunProcess(WineMsTransactionFunctions.ProcessGeneralLedgerTransactions);
    }

    private void mniProcessStock_Click(object sender, EventArgs e)
    {
      RunProcess(WineMsTransactionFunctions.ProcessStockTransactions);
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

    private void RunProcess(Action<ICancelableBackgroundWorker> process)
    {
      if (_runningProcesCancelProvider != null)
        return;

      SetMenuState(ProcessRunningState.Start);

      _runningProcesCancelProvider =
        BackgroundWorkerFunctions
          .Execute(
            context => { process(context.Sender); },
            context =>
            {
              _runningProcesCancelProvider = null;
              "Process completed.".ShowInformationMessage();
              SetMenuState(ProcessRunningState.Stop);
            },
            context => { });
    }

    private void SetMenuState(ProcessRunningState state)
    {
      var stopped = state == ProcessRunningState.Stop;
      mniFile.Enabled = stopped;
      mniProcessGeneralLedger.Enabled = stopped;
      mniProcessPurchaseOrders.Enabled = stopped;
      mniProcessStock.Enabled = stopped;
      mniCancel.Enabled = !stopped;
    }

  }

  internal enum ProcessRunningState {

    Start,
    Stop

  }

}