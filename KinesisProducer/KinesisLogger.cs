using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.Kinesis;
using Amazon.Kinesis.ClientLibrary
using Amazon.Kinesis.Model;
using Newtonsoft.Json;

namespace KinesisProducer {

    public class KinesisLogger {

        public async static void Put(string key, string data) {
            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            using (MemoryStream ms = new MemoryStream(bytes)) {
                AmazonKinesisConfig config = new AmazonKinesisConfig();
                config.RegionEndpoint = Amazon.RegionEndpoint.USEast1;

                //create client that pulls creds from web.config and takes in Kinesis config
                AmazonKinesisClient client = new AmazonKinesisClient(config);

                //create put request
                PutRecordRequest requestRecord = new PutRecordRequest();
                //list name of Kinesis stream
                requestRecord.StreamName = "hubportal-test";
                //give partition key that is used to place record in particular shard
                requestRecord.PartitionKey = key;
                //add record as memorystream
                requestRecord.Data = ms;
                //PutRecordResponse recordResponse = await client.PutRecordAsync(requestRecord);
                //string response = $"Shard ID: {recordResponse.ShardId}, Metadata: {recordResponse.ResponseMetadata}";
            }
        }

        public static void Seed() {
            SeedData data = new SeedData();
            Put(data.Client, data.RandomNumberData);
        }
    }
}
