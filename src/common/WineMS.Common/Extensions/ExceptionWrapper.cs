using System;
using CSharpFunctionalExtensions;
using RadiusCSharp.Core.Logging;

namespace WineMS.Common.Extensions {

  public static class ExceptionWrapper {

    public static Result Wrap(Func<Result> func) {
      try {
        return func();
      }
      catch (Exception e) {
        e.LogException();
        return Result.Fail(e.GetExceptionMessages());
      }
    }

    public static Result<T> Wrap<T>(Func<Result<T>> func) {
      try {
        return func();
      }
      catch (Exception e) {
        e.LogException();
        return Result.Fail<T>(e.GetExceptionMessages());
      }
    }

  }

}