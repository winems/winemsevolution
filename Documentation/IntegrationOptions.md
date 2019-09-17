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



