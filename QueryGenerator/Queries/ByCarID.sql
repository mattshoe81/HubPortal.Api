﻿AND ht.COLLAB_NAME IN (
	SELECT COLLAB_NAME
	FROM HTS_TRANSACTION_VALUE_SEARCH
	WHERE FIELD = '@'
)
