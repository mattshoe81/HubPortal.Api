AND (
	cl.CLIENT_ID = (
		select client_id
		from hts_client
		where CLIENT_NAME = '@'
	)
	OR
	cli.CLIENT_ID = (
		select client_id
		from hts_client
		where CLIENT_NAME = '@'
	)
)