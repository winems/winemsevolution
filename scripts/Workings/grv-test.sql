-- select * from _IntegrationPurchaseOrderBuffer where Posted=0 order by Company, [Transaction Type], [Document No_]
-- select * from _Integration_WineMSEvoPurchaseInvoiceMapping

-- delete from IntegrationMappings

-- update [WineMS Buffer Entry] set Posted=0 WHERE ([Transaction Type] IN (N'GOODSRECEIVED', N'ADJUSTGOODSRECEIVED'))

/*
update [WineMS Buffer Entry] set Posted=1
update [WineMS Buffer Entry] set Posted=0 WHERE [Document No_]='20PU00001617' --([Transaction Type] IN (N'GOODSRECEIVED', N'ADJUSTGOODSRECEIVED'))
delete from IntegrationMappings;
insert into IntegrationMappings (CompanyId,Guid, IntegrationDocumentNumber, IntegrationDocumentType)
select 'a', Guid, [Document No_], 'GRV'
	from _IntegrationPurchaseOrderBuffer
	 where [Document Date] < '2020-03-01';

*/

--select * from IntegrationMappings

