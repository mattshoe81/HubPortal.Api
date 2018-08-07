select 
    ht.trans_id, 
    pr.process_name, 
    tr.transaction_type_name, 
    ht.trans_time, 
    ht.policy_number, 
    ht.claim_number, 
    ht.referral_number, 
    ht.csr, 
    ht.trans_completed, 
    ht.total_elapsed_time, 
    ht.url, 
    cl.client_name, 
    cli.client_name, 
    ht.ping_flag 
from 
    hts_transactions ht, 
    hts_process pr, 
    hts_transaction_type tr, 
    hts_client cl, 
    hts_client cli 
where 
    ht.process_id = &PROCESS_ID_SUB
    and 
    ht.process_id = pr.process_id 
    and 
    pr.trans_type = tr.trans_type_id 
    and 
    pr.source=cl.client_id 
    and 
    pr.destination=cli.client_id