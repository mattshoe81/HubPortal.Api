AND ht.process_id in
	(
		select process_id
		from hts_process
		where process_name like '@'
	)
