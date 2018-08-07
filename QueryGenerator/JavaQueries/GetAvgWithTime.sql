select
    p.process_name,
    sum(t.ihreq_elapsed_time),
    sum(t.IHRES_ELAPSED_TIME),
    sum(t.APP_ELAPSED_TIME),
    count(*)
from
    hts_transactions t,
    hts_process p
where
    p.process_id=t.process_id
    and
    t.trans_time >= sysdate-&SUB
group by p.process_name