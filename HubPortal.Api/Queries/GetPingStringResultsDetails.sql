select distinct 
    s.ping_display_name, 
    r.process, 
    s.ping_enabled, 
    r.ping_time, 
    r.success, 
    r.duration, 
    r.ping_success_time, 
    s.ping_recommended, 
    htp.process_name 
from  
    ping_results r, 
    ping_string s, 
    hts_process htp 
where  
    r.process = s.process_name 
    and 
    r.process = htp.ping_name 
order by success desc, 
ping_display_name
       