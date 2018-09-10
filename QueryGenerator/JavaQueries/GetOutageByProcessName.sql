select
    Process_name,
    destination,
    ping_name,
    outage_Start_Time,
    outage_end_time,
    outage_type,last_status
from hts_outage
where
    last_status='Active'
    and
    process_name like &PROCESS_NAME_SUB
