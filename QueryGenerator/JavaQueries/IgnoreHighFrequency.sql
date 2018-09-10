 and ht.process_id
 NOT IN (
    SELECT process_id
    FROM hts_process
    WHERE
        process_name = 'DB Updater'
        or
        process_name = 'Geocode Service'
    )
