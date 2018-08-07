select
    pr.process,
    htp.process_name,
    pr.ping_time,
    pr.ping_result_string,
    pr.success,pr.ping_success_time,
    htc.client_id,htc.client_name
from
    ping_results pr,
    hts_process htp,
    hts_client htc
where
    pr.process=htp.ping_name
    and
    htp.destination=htc.client_id
    and
    pr.process=&PROCESS_SUB