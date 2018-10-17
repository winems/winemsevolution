using System;
using System.ComponentModel;

namespace WineMsEvolutionGui.Extensions {

  public static class BackgroundWorkerFunctions {

    public static BackgroundWorkerCancellationProvider Execute(
      Action<DoWorkContext> doWork,
      Action<WorkCompletedContext> workComplete,
      Action<WorkProgressContext> reportProgress = null)
    {
      var bw = new BackgroundWorker();
      bw.DoWork += (sender, args) => { doWork(new DoWorkContext(sender, args)); };

      bw.RunWorkerCompleted += (sender, args) =>
      {
        workComplete(new WorkCompletedContext(sender, args));
      };

      if (reportProgress != null) {
        bw.ProgressChanged += (sender, args) =>
        {
          reportProgress(new WorkProgressContext(sender, args));
        };
        bw.WorkerReportsProgress = true;
      }

      bw.RunWorkerAsync();

      return new BackgroundWorkerCancellationProvider {
        Cancel = () => bw.CancelAsync()
      };
    }

  }

  public class BackgroundWorkerCancellationProvider {

    public Action Cancel;

  }

  public struct DoWorkContext {

    public object Sender { get; }

    public object Result {
      get => Args.Result;
      set => Args.Result = value;
    }

    public DoWorkEventArgs Args { get; }

    public DoWorkContext(object sender, DoWorkEventArgs args)
    {
      Sender = sender;
      Args = args;
    }

  }

  public struct WorkCompletedContext {

    public object Sender { get; }

    public object Result => Args.Result;

    public RunWorkerCompletedEventArgs Args { get; }

    public WorkCompletedContext(object sender, RunWorkerCompletedEventArgs args)
    {
      Sender = sender;
      Args = args;
    }

  }

  public struct WorkProgressContext {

    public object Sender { get; }

    public ProgressChangedEventArgs Args { get; }

    public WorkProgressContext(object sender, ProgressChangedEventArgs args)
    {
      Args = args;
      Sender = sender;
    }

  }

}