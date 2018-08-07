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
    ps.Ping_recommended,
    hp.process_id
from
    hts_process hp,
    hts_transaction_type ht,
    ping_string ps
where
    hp.ping_name=ps.process_name
    and
    hp.trans_type=ht.trans_type_id
    and
    ps.process_name=&PROCESS_SUB