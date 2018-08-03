using System;
using Xunit;
using HubPortal.QueryGenerator;
using HubPortal.QueryGenerator.Extensions;

namespace HubPortal.Api.Tests.QueryGenerator
{
    public class TokenizerTests {

        [Fact]
        public void ParserTest() {
            string queryString = "process".GetQuery().RefineQuery("processName", "1st Auto Claims ESL").RefineQuery("transactionType", "something");
            string query = Parser.Parse(queryString);

            Assert.Equal("FIND process { processName : '1st Auto Claims ESL' } { transactionType : 'something' }", queryString);
        }
    }
}
