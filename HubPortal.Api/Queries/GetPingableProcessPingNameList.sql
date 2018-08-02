select distinct(ping_name) 
from hts_process 
where 
    pingable='Y' 
    and 
    ping_name is not null 
order by ping_name asc