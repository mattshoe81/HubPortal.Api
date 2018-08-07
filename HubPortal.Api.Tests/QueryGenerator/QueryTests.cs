using HubPortal.QueryGenerator.ContextFreeGrammar;
using HubPortal.QueryGenerator.Exceptions;
using System;
using Xunit;

namespace HubPortal.Api.Tests.QueryGenerator {

    public class QueryTests {

        #region Public Methods

        [Fact]
        public void RefineTest_PropertyException() {
            IQuery query = CFGBuilder.GetQuery("process");
            Exception ex = Assert.Throws<QuerySyntaxException>(() => query.Refine("xxx", "value"));
            Assert.Equal("xxx is not a valid Property.", ex.Message);
        }

        [Fact]
        public void RefineTest_SpecialChar1() {
            IQuery query = CFGBuilder.GetQuery("process");
            query.Refine(Symbols.PROCESS_NAME, "&");
            Assert.Equal("FIND process { processName : '&' }", query.ToString());
        }

        [Fact]
        public void RefineTest_SpecialChar2() {
            IQuery query = CFGBuilder.GetQuery("process");
            query.Refine(Symbols.PROCESS_NAME, "-");
            Assert.Equal("FIND process { processName : '-' }", query.ToString());
        }

        [Fact]
        public void RefineTest_SpecialChar3() {
            IQuery query = CFGBuilder.GetQuery("process");
            query.Refine(Symbols.PROCESS_NAME, " ");
            Assert.Equal("FIND process { processName : ' ' }", query.ToString());
        }

        [Fact]
        public void RefineTest_SpecialChar4() {
            IQuery query = CFGBuilder.GetQuery("process");
            query.Refine(Symbols.PROCESS_NAME, ".");
            Assert.Equal("FIND process { processName : '.' }", query.ToString());
        }

        [Fact]
        public void RefineTest_ValueException() {
            IQuery query = CFGBuilder.GetQuery("process");
            Exception ex = Assert.Throws<QuerySyntaxException>(() => query.Refine(Symbols.ACCOUNT_NUMBER, "***"));
            Assert.Equal("*** is not a valid Value.", ex.Message);
        }

        [Fact]
        public void Test_Boundary() {
            IQuery query = CFGBuilder.GetQuery("process");
            Assert.Equal("FIND process", query.ToString());
        }

        [Fact]
        public void Test_Routine() {
            IQuery query = CFGBuilder.GetQuery("process");
            query.Refine("processName", "1st Auto Claims ESL");
            query.Refine("startTime", "noon oclock");
            query.Refine("endTime", "midnight");
            query.Refine("serverName", "the server");
            query.Refine("transactionType", "something");
            query.AddLookup("coverage");
            query.Refine("policyNumber", "012345");
            query.Refine("claimNumber", "0987654323");
            query.Refine("referralNumber", "the number");

            Assert.Equal("FIND process { processName : '1st Auto Claims ESL' } { startTime : 'noon oclock' } { endTime : 'midnight' } { serverName : 'the server' } { transactionType : 'something' } AND coverage { policyNumber : '012345' } { claimNumber : '0987654323' } { referralNumber : 'the number' }", query.ToString());
        }

        #endregion Public Methods
    }
}