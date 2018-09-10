select pr.ping_result_string
from ping_results pr
where pr.process = &PROCESS_SUB
