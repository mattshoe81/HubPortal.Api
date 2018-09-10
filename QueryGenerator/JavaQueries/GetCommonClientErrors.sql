select
    error_text,
    error_action
from Hts_ClientErrors
where
    client_id = &CLIENT_ID_SUB
    and
    error_Description like &ERROR_DESCRIPTION_SUB
