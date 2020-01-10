using CSharpFunctionalExtensions;
using Pastel.Evolution;
using WineMS.Common.Extensions;
using WineMS.Evolution.Orders;
using WineMS.WineMS.DataAccess;

namespace WineMS.Evolution.CreditNotes {

  public static class EvolutionCreditNoteTransactionFunctions {

    public static Result<WineMsCreditNoteTransactionDocument> ProcessTransaction(
      WineMsCreditNoteTransactionDocument wineMsCreditNoteTransactionDocument) =>
      CreateCreditNote(wineMsCreditNoteTransactionDocument)
        .Bind(
          creditNote => creditNote.AddSalesOrderLines(wineMsCreditNoteTransactionDocument))
        .Bind(
          creditNote => ExceptionWrapper
            .Wrap(
              () => {
                creditNote.Save();
                wineMsCreditNoteTransactionDocument.IntegrationDocumentNumber = creditNote.OrderNo;
                return Result.Ok(wineMsCreditNoteTransactionDocument);
              }));

    private static Result<CreditNote> CreateCreditNote(
      WineMsCreditNoteTransactionDocument creditNoteTransactionDocument) =>
      ExceptionWrapper
        .Wrap(
          () => {
            var customer = new Customer(creditNoteTransactionDocument.CustomerAccountCode);

            var creditNote = (CreditNote)
                new CreditNote {
                    Customer = customer,
                    DeliveryDate = creditNoteTransactionDocument.TransactionDate,
                    Description = "Credit Note",
                    DiscountPercent = (double) creditNoteTransactionDocument.DocumentDiscountPercentage,
                    DueDate = creditNoteTransactionDocument.TransactionDate,
                    InvoiceDate = creditNoteTransactionDocument.TransactionDate,
                    OrderDate = creditNoteTransactionDocument.TransactionDate,
                    OrderNo = creditNoteTransactionDocument.DocumentNumber
                  }
                  .SetDeliveryAddress(customer)
                  .SetPostalAddress(customer)
                  .SetMessageLines(creditNoteTransactionDocument)
                  .SetTaxMode(customer)
                  .SetExchangeRate(customer, creditNoteTransactionDocument.ExchangeRate)
              ;

            return Result.Ok(creditNote);
          });

  }

}