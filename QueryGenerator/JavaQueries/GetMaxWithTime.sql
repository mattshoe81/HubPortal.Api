select 
    p.process_name, 
    max(t.ihreq_elapsed_time), 
    max(t.IHRES_ELAPSED_TIME), 
    max(t.APP_ELAPSED_TIME) 
from 
    hts_transactions t, 
    hts_process p 
where 
    p.process_id=t.process_id 
    and 
    t.trans_time >= sysdate-&SUB 
group by p.process_name