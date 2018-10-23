namespace WineMS.BusinessLogic.Extensions {

  public static class ProgressReportFunctions {

    public static int CalcPercentProgress(int count, int maxCount) =>
      (int) ((double) count / maxCount * 100);

  }

}