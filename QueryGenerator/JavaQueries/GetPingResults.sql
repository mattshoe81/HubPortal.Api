select
    p.process_name,
    r.process,
    s.ping_enabled,
    r.ping_time,
    r.success,
    r.duration,
    r.ping_success_time
from
    hts_process p,
    ping_results r,
    ping_string s
where
    p.ping_name = r.process
    and
    r.process=s.process_name