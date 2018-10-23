namespace WineMS.Common {

  public interface IBackgroundWorker {

    bool CancellationPending { get; }
    void ReportProgress(int percentProgress);

  }

  public class NullBackgroundWorker : IBackgroundWorker {

    public bool CancellationPending => false;
    public void ReportProgress(int percentProgress) { }

  }

}