select
    c.checkpoint_id,
    c.trans_id,
    c.checkpoint_loc AS CHECKPOINT_LOCATION,
    c.checkpoint_timestamp AS CHECKPOINT_TIME,
    sum(length(d.trans_data)) AS DATA_SIZE,
    c.elapsed_time,
    c.checkpoint_type,
    c.server_name
from
    hts_checkpoint c
    INNER JOIN
    hts_checkpoint_data d
    ON
    c.checkpoint_id = d.checkpoint_id
where c.trans_id = '@'
GROUP BY
    c.checkpoint_id,
    c.trans_id,
    c.checkpoint_loc,
    c.checkpoint_timestamp,
    c.elapsed_time,
    c.checkpoint_type,
    c.server_name
order by c.checkpoint_timestamp asc
