select distinct 
    p.process_name, 
    p.ping_name, 
    s.ping_enabled 
from 
    hts_process p, 
    ping_string s, 
    ping_results r 
where 
    p.ping_name=s.process_name 
    and not exists (
        select * 
        from ping_results r 
        where r.process=s.process_name
    )