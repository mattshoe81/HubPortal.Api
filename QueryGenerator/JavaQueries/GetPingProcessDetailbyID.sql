select 
    hp.process_name, 
    hp.source,
    hp.destination,
    hp.service_layer, 
    hp.source_connector, 
    hp.destination_connector,
    hp.pingable,
    hp.client_contact_instructions, 
    hp.other_instructions,
    ht.transaction_type_name,
    hp.process_id,
    hp.trans_type,
    hp.ping_name,
    hp.search_field,
    hp.collab_name 
from 
    hts_process hp, 
    hts_transaction_type ht 
where  
    hp.trans_type=ht.trans_type_id 
    and 
    hp.process_id=&PROCESS_SUB