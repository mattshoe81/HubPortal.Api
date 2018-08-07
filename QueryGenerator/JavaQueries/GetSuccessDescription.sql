select distinct 
    vs.description, 
    vs.logical_operator, 
    vs.checkpoint_loc, 
    vs.search_sequence_id 
from 
    hts_transaction_value_search vs, 
    hts_process p 
where 
    p.process_name = ? 
    and 
    p.collab_name is not null
    and 
    vs.usage_type = 'SUCCESS_CRITERIA' 
    and 
    p.collab_name = vs.collab_name 
order by vs.search_sequence_id