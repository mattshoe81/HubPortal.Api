SELECT 
    ht.trans_id,  
    pr.process_name, 
    tr.transaction_type_name, 
    ht.trans_time, 
    ht.trans_completed, 
    ht.total_elapsed_time, 
    ht.url, 
    cl.client_name 
        as clname1, 
    cli.client_name 
        as clname2, 
    ht.ping_flag, 
    ht.is_successful, 
    (
        SELECT Count(*) 
        FROM   hts_checkpoint 
        WHERE  trans_id = ht.trans_id
    ) AS checkpointcount 
FROM   
    hts_transactions ht, 
    hts_process pr, 
    hts_transaction_type tr, 
    hts_client cl, 
    hts_client cli 
WHERE  
    ht.process_id = pr.process_id 
    AND 
    pr.trans_type = tr.trans_type_id 
    AND 
    pr.source = cl.client_id 
    AND 
    pr.destination = cli.client_id