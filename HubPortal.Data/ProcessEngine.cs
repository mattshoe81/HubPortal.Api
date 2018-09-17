using System;
using System.Collections.Generic;
using HubPortal.Data.Models;
using HubPortal.Data.Utilities;
using HubPortal.QueryGenerator.ContextFreeGrammar;
using Oracle.ManagedDataAccess.Client;

namespace HubPortal.Data {

    public class ProcessEngine {

        public static IEnumerable<Process> GetActiveProcesses() {
            IQuery query = QueryBuilder.GetQuery(Symbols.GET, Symbols.ACTIVE_PROCESS);
            List<Process> processes = new List<Process>();

            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                connection.Open();
                string queryString = query.ToString();
                using (OracleCommand command = new OracleCommand(query.ToString(), connection)) {
                    processes = ReadProcesses(command);
                }
            }

            return processes;
        }

        public static object GetInactiveProcesses() {
            IQuery query = QueryBuilder.GetQuery(Symbols.GET, Symbols.INACTIVE_PROCESS);
            List<Process> processes = new List<Process>();

            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                connection.Open();
                string queryString = query.ToString();
                using (OracleCommand command = new OracleCommand(query.ToString(), connection)) {
                    processes = ReadProcesses(command);
                }
            }

            return processes;
        }

        /// <summary>
        /// Returns a list of strings containing the names of all the processes in the database.
        /// </summary>
        /// <returns>List of strings containing the names of all the processes in the database</returns>
        public static IEnumerable<string> GetProcessNameList() {
            IQuery query = QueryBuilder.GetQuery(Symbols.FINDALL, Symbols.PROCESS_LIST);
            return OracleDataUtil.GetListOfString(query);
        }

        public static Process UpdatePingEnabled(string processName, string yesOrNo) {
            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                string updateString = $"update PING_STRING set PING_ENABLED='{yesOrNo}' where PROCESS_NAME='{processName}'";
                connection.Open();
                using (OracleCommand command = new OracleCommand(updateString, connection)) {
                    //command.ExecuteNonQuery();
                }
            }
            Process updatedProcess = new Process();
            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                string queryString = $"select distinct s.ping_display_name, r.process, s.ping_enabled, r.ping_time, r.success, r.duration, r.ping_success_time, s.ping_recommended, htp.process_name from ping_results r, ping_string s, hts_process htp where r.process = s.process_name and r.process = htp.ping_name and htp.process_name = '{processName}'";
                connection.Open();
                using (OracleCommand command = new OracleCommand(queryString, connection)) {
                    //updatedProcess = ReadProcesses(command)?[0];
                }
            }
            return updatedProcess;
        }

        private static List<Process> ReadProcesses(OracleCommand command) {
            List<Process> processes = new List<Process>();
            OracleDataReader reader = command.ExecuteReader();
            try {
                while (reader.Read()) {
                    Process process = new Process();
                    process.PingName = reader.IsDBNull(reader.GetOrdinal("PING_DISPLAY_NAME")) ? null : reader.GetString(reader.GetOrdinal("PING_DISPLAY_NAME"));
                    process.ProcessName = reader.IsDBNull(reader.GetOrdinal("PROCESS_NAME")) ? null : reader.GetString(reader.GetOrdinal("PROCESS_NAME"));
                    process.PingDesirable = reader.IsDBNull(reader.GetOrdinal("PING_RECOMMENDED")) ? null : reader.GetString(reader.GetOrdinal("PING_RECOMMENDED"));
                    process.PingTime = reader.IsDBNull(reader.GetOrdinal("PING_TIME")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("PING_TIME"));
                    process.SuccessPingTime = reader.IsDBNull(reader.GetOrdinal("PING_SUCCESS_TIME")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("PING_SUCCESS_TIME"));
                    process.Pingable = reader.IsDBNull(reader.GetOrdinal("PING_ENABLED")) ? null : reader.GetString(reader.GetOrdinal("PING_ENABLED"));
                    process.Status = reader.IsDBNull(reader.GetOrdinal("SUCCESS")) ? null : reader.GetString(reader.GetOrdinal("SUCCESS"));
                    process.Duration = reader.IsDBNull(reader.GetOrdinal("DURATION")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DURATION"));
                    processes.Add(process);
                }
            } finally {
                reader.Close();
            }

            return processes;
        }
    }
}
