select
    s.ping_display_name,
    s.process_name,
    s.ping_enabled,
    s.ping_recommended
from
    ping_String s
where
    s.process_name
    not in (
            select
                process
            from ping_results r
            where
                process is not null
        )