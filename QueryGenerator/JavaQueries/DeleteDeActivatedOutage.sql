Delete from hts_outage 
where 
    outage_start_time < sysdate-7 
    and 
    last_status=&LAST_STATUS_SUB