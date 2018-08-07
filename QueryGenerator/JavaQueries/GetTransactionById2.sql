SELECT
    ht.trans_id,
    pr.process_name,
    tr.transaction_type_name,
    ht.trans_time,
    ht.trans_completed,
    ht.total_elapsed_time,
    ht.url, cl.client_name,
    cli.client_name,
    ht.ping_flag,
    pr.service_layer,
    ht.is_successful,
    cl.client_id,
    ht.session_id,
    pr.source_connector,
    pr.destination_connector
FROM
    hts_transactions ht,
    hts_process pr,
    hts_transaction_type tr,
    hts_client cl,
    hts_client cli
WHERE
    ht.trans_id = &TRANS_ID_SUB
    AND
    ht.process_id = pr.process_id
    AND
    pr.trans_type = tr.trans_type_id
    AND
    pr.source = cl.client_id
    AND
    pr.destination = cli.client_id