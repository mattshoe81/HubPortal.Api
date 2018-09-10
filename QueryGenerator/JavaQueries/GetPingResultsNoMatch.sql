select
    r.process,
    r.process,
    s.ping_enabled,
    r.ping_time,
    r.success,
    r.duration,
    r.ping_success_time
from
    ping_results r,
    ping_string s
where not exists
    (
        select *
        from hts_process p
        where p.ping_name=r.process
    )
    and
    r.process=s.process_name
