select ht.trans_id, 
	pr.process_name, 
	tr.transaction_type_name, 
	ht.trans_time, 
	ht.trans_completed, 
	ht.total_elapsed_time, 
	ht.url, 
	cl.client_name as clname1, 
	cli.client_name as clname2, 
	ht.ping_flag, 
	ht.is_successful,
	(
		select count(*)
		from hts_checkpoint 
		where trans_id = ht.trans_id
	) as checkpointcount 
from hts_transactions ht, 
	hts_process pr, 
	hts_transaction_type tr, 
	hts_client cl, hts_client cli 
where (
	ht.process_id = pr.process_id
	and pr.trans_type = tr.trans_type_id 
	and pr.source = cl.client_id
	and pr.destination = cli.client_id
	AND ROWNUM < 10000
	
