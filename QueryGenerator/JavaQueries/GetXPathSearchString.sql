select 
    xpath_value, 
    search_method 
from 
    HTS_TRANSACTION_SOAP_MESSAGE hsm, 
    hts_checkpoint cp, 
    hts_transactions hts 
where 
    cp.trans_id=hts.trans_id 
    and 
    hts.collab_name=hsm.collab_name 
    and 
    cp.checkpoint_id=&CHECKPOINT_ID_SUB
    and 
    hsm.checkpoint_loc=&CHECKPOINT_LOC_SUB