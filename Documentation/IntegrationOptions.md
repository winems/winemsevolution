# WineMS Sage Evolution integration tool

## IntegrationOptions Table

### Connection strings

#### KeyName: EvolutionCommonDatabase-<companyId>

Connection string to the Evolution common database.

companyId is defined on the transaction buffer table. Set to default (e.g.: EvolutionCommonDatabase-default) to provide a global connection string that will be used if a companyId specific connection string is not found.

E.g.: Integrated Security=SSPI;MultipleActiveResultSets=True;Data Source=.\sql2016;Initial Catalog=EvolutionCommon720

#### KeyName: EvolutionCompanyDatabase-<companyId>

Connection string to the Evolution company database.

companyId is defined on the transaction buffer table. Set to default (e.g.: EvolutionCompanyDatabase-default) to provide a global connection string that will be used if a companyId specific connection string is not found.

E.g.: Integrated Security=SSPI;MultipleActiveResultSets=True;Data Source=.\sql2016;Initial Catalog=EvolutionSample720

### Transactions

#### KeyName: journal-transaction-code

The Evolution transaction code to use for journal transactions.

E.g.: JNL

#### KeyName: purchase-order-integration-type

Purchase order integration type.

Possible values are:

- purchase-order: Will save the purchase order as an open Evolution purchase order.
- goods-receive-voucher: Will post the purchase order as a completed goods received voucher. This will confirm all quantities on the purchase order.
- supplier-invoice: Will post the purchase order as a completed supplier invoice. This will confirm all quantities on the purchase order.

#### KeyName: return-to-supplier-integration-type

Return to supplier integration type. If not defined or not one of the possible values the application will default to return-to-supplier-post.

Possible values are:

- return-to-supplier-post: Will complete the return to supplier, posting all general ledger entries.
- return-to-supplier-save-only: Will save the return to supplier as an open Evolution Return to Supplier.

#### KeyName: sales-order-integration-type

Sales order integration type. If not defined or not one of the possible values the application will default to sales-order.

Possible values are:

- sales-order: Will save the sales order as an open Evolution sales order.
- tax-invoice: Will post the sales order as a completed sales invoice. This will confirm all quantities on the sales order.

#### KeyName: sales-order-use-evolution-invoice-number

Set to True to have the application use the next invoice number defined by Evolution. If this key is False or missing the invoice number will be set to the WineMS Document Number (\_IntegrationSalesOrderBuffer.[Document No_]).



