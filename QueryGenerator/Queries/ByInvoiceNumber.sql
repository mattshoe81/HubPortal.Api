﻿
AND ht.TRANS_ID IN (
	SELECT TRANS_ID 
	FROM HTS_TRANSACTION_XREF
	WHERE XREF_FIELD = 'INVOICE_NUMBER'
		AND XREF_VALUE = '@'
)
