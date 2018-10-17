using System;
using System.ComponentModel;
using WineMS.Common;

namespace WineMsEvolutionGui.Extensions {

  public static class BackgroundWorkerFunctions {

    public static BackgroundWorkerCancellationProvider Execute(
      Action<DoWorkContext> doWork,
      Action<WorkCompletedContext> workComplete,
      Action<WorkProgressContext> reportProgress = null)
    {
      var bw = new CancelableBackgroundWorker();
      bw.DoWork += (sender, args) => { doWork(new DoWorkContext((IBackgroundWorker)sender, args)); };

      bw.RunWorkerCompleted += (sender, args) =>
      {
        workComplete(new WorkCompletedContext((IBackgroundWorker)sender, args));
      };

      if (reportProgress != null) {
        bw.ProgressChanged += (sender, args) =>
        {
          reportProgress(new WorkProgressContext((IBackgroundWorker)sender, args));
        };
        bw.WorkerReportsProgress = true;
      }

      bw.WorkerSupportsCancellation = true;
      bw.RunWorkerAsync();

      return new BackgroundWorkerCancellationProvider {
        Cancel = () => bw.CancelAsync()
      };
    }

    private class CancelableBackgroundWorker : BackgroundWorker, IBackgroundWorker {

    }

  }

  public class BackgroundWorkerCancellationProvider {

    public Action Cancel;

  }

  public struct DoWorkContext {

    public IBackgroundWorker Sender { get; }

    public object Result {
      get => Args.Result;
      set => Args.Result = value;
    }

    public DoWorkEventArgs Args { get; }

    public DoWorkContext(IBackgroundWorker sender, DoWorkEventArgs args)
    {
      Sender = sender;
      Args = args;
    }

  }

  public struct WorkCompletedContext {

    public IBackgroundWorker Sender { get; }

    public object Result => Args.Result;

    public RunWorkerCompletedEventArgs Args { get; }

    public WorkCompletedContext(
      IBackgroundWorker sender,
      RunWorkerCompletedEventArgs args)
    {
      Sender = sender;
      Args = args;
    }

  }

  public struct WorkProgressContext {

    public IBackgroundWorker Sender { get; }

    public ProgressChangedEventArgs Args { get; }

    public WorkProgressContext(IBackgroundWorker sender, ProgressChangedEventArgs args)
    {
      Args = args;
      Sender = sender;
    }

  }

}