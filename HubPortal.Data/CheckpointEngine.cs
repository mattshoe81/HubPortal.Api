using System;
using System.Collections.Generic;

using HubPortal.Data.Models;
using HubPortal.Data.Utilities;
using HubPortal.QueryGenerator.ContextFreeGrammar;

using Oracle.ManagedDataAccess.Client;

namespace HubPortal.Data {

    public class CheckpointEngine {

        public static IEnumerable<Checkpoint> GetCheckpoints(string transactionid) {
            IQuery query = QueryBuilder.GetQuery(Symbols.FINDALL, Symbols.CHECKPOINT).Refine(Symbols.TRANSACTION_ID, transactionid);

            List<Checkpoint> checkpoints = new List<Checkpoint>();
            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                connection.Open();
                string dbQuery = query.ToString();
                using (OracleCommand command = new OracleCommand(query.ToString(), connection)) {
                    OracleDataReader reader = command.ExecuteReader();
                    try {
                        while (reader.Read()) {
                            Checkpoint checkpoint = new Checkpoint();
                            checkpoint.ID = reader.GetInt32(reader.GetOrdinal("CHECKPOINT_ID"));
                            checkpoint.Location = reader.IsDBNull(reader.GetOrdinal("CHECKPOINT_LOCATION")) ? null : reader.GetString(reader.GetOrdinal("CHECKPOINT_LOCATION"));
                            checkpoint.Time = reader.IsDBNull(reader.GetOrdinal("CHECKPOINT_TIME")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CHECKPOINT_TIME"));
                            checkpoint.Type = reader.IsDBNull(reader.GetOrdinal("CHECKPOINT_TYPE")) ? null : reader.GetString(reader.GetOrdinal("CHECKPOINT_TYPE"));
                            checkpoint.TransactionId = reader.IsDBNull(reader.GetOrdinal("TRANS_ID")) ? null : reader.GetString(reader.GetOrdinal("TRANS_ID"));
                            checkpoint.ServerName = reader.IsDBNull(reader.GetOrdinal("SERVER_NAME")) ? null : reader.GetString(reader.GetOrdinal("SERVER_NAME"));
                            checkpoint.ElapsedTime = reader.GetInt32(reader.GetOrdinal("ELAPSED_TIME"));
                            checkpoint.Size = reader.GetInt32(reader.GetOrdinal("DATA_SIZE"));
                            checkpoint.CheckpointId = reader.GetInt32(reader.GetOrdinal("CHECKPOINT_ID"));
                            checkpoints.Add(checkpoint);
                        }
                    } finally {
                        reader.Close();
                    }
                }
            }

            return checkpoints;
        }
    }
}
