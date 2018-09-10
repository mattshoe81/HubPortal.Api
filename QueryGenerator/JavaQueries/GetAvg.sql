select
    p.process_name,
    sum(c.ihreq_elapsed_time),
    sum(c.app_elapsed_time),
    sum(c.ihres_elapsed_time),
    sum(c.transaction_count)
from sum_response_time c,
    hts_process p
where p.process_id=c.process_id
group by p.process_name
order by p.process_name asc
