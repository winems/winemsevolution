namespace WineMS.Common {

  public interface ICancelableBackgroundWorker {

    bool CancellationPending { get; }

  }

}