use wms_WELLINGTON_WINES
select top 100 p.cAuditNumber, a.Master_Sub_Account, a.Description, p.*
	from PostGL p 
		join Accounts a
			on a.AccountLink=p.AccountLink
	where p.AutoIdx > 118850
	order by p.AutoIdx desc;
/*
select top 100 p.cAuditNumber, p.* 
	from PostST p
	where p.AutoIdx > 63365
	order by p.AutoIdx desc;

select top 100 h.DocState, h.OrderNum, l.* 
	from _btblInvoiceLines l
		join InvNum h
			on h.AutoIndex = l.iInvoiceID
	where l.idInvoiceLines > 55700
	order by l.idInvoiceLines desc
*/