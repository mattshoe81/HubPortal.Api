using HubPortal.Data.Models;
using HubPortal.Data.Utilities;
using HubPortal.QueryGenerator.ContextFreeGrammar;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace HubPortal.Data {

    public class PingEngine {

        public static Ping GetPing(string processName) {
            string query = $"select p.PING_NAME, s.PING_TYPE, s.CLIENT_ID, s.TYPE, s.PING_STRING, s.EXPECTED_RESULT, s.PING_ENABLED from PING_STRING s, hts_process p where p.ping_name = s.PROCESS_NAME AND p.PROCESS_NAME = '{processName}'";
            Ping ping = new Ping();
            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                connection.Open();
                using (OracleCommand command = new OracleCommand(query, connection)) {
                    OracleDataReader reader = command.ExecuteReader();
                    try {
                        reader.Read();
                        ping.ClientID = reader.IsDBNull(reader.GetOrdinal("CLIENT_ID")) ? null : reader.GetString(reader.GetOrdinal("CLIENT_ID"));
                        ping.Name = reader.IsDBNull(reader.GetOrdinal("PING_NAME")) ? null : reader.GetString(reader.GetOrdinal("PING_NAME"));
                        ping.Type = reader.IsDBNull(reader.GetOrdinal("TYPE")) ? null : reader.GetString(reader.GetOrdinal("TYPE"));
                        ping.PingType = reader.IsDBNull(reader.GetOrdinal("PING_TYPE")) ? null : reader.GetString(reader.GetOrdinal("PING_TYPE"));
                        ping.PingString = reader.IsDBNull(reader.GetOrdinal("PING_STRING")) ? null : reader.GetString(reader.GetOrdinal("PING_STRING"));
                        ping.Expected = reader.IsDBNull(reader.GetOrdinal("EXPECTED_RESULT")) ? null : reader.GetString(reader.GetOrdinal("EXPECTED_RESULT"));
                        ping.Enabled = reader.IsDBNull(reader.GetOrdinal("PING_ENABLED")) ? null : reader.GetString(reader.GetOrdinal("PING_ENABLED"));
                    } finally {
                        reader.Close();
                    }
                }
            }

            return ping;
        }
    }
}
