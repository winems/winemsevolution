namespace WineMS.Common {

  public interface IBackgroundWorker {

    bool CancellationPending { get; }
    void ReportProgress(int percentProgress);

  }

}