using System.Windows.Forms;
using WineMsEvolutionGui.Extensions;
using WineMS.BusinessLogic.GeneralLedger;
using WineMS.Common;

namespace WineMsEvolutionGui {

  public partial class FrmMain : Form {

    private BackgroundWorkerCancellationProvider _processGeneralLedgerCancel;

    public FrmMain() { InitializeComponent(); }

    private void FrmMain_Load(object sender, System.EventArgs e) { LoadStatusBarInformation(); }

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

    private void mniExit_Click(object sender, System.EventArgs e) { Close(); }

    private void mniProcessGeneralLedger_Click(object sender, System.EventArgs e)
    {
      if (_processGeneralLedgerCancel != null) {
        _processGeneralLedgerCancel.Cancel();
        return;
      }

      _processGeneralLedgerCancel =
        BackgroundWorkerFunctions
          .Execute(
            context => { WineMsTransactionFunctions.ProcessGeneralLedgerTransactions(); },
            context => { _processGeneralLedgerCancel = null; },
            context => { });
    }

  }

}