using NUnit.Framework;
using FlightControlWeb.Models;
using UnitTestFlightWeb.classTest;
using Moq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace TestFlightManager
{

    public class FlightManagerTest
    {


        [SetUp]
        public void Setup()
        {
        }

        //Test to check logic of 
        [Test]
        public async System.Threading.Tasks.Task Test1Async()
        {

            //arrange
            string id = "id";
            FlightPlan exceptedFlightPlan = GetOriginalFlightPlan();
            //stubs
            ExternalDb stubExData = new ExternalDb();
            InnerDb inner = new InnerDb();
            //mock
            Mock<IExternalFlight> extenalTestMock = new Mock<IExternalFlight>();


            extenalTestMock.Setup(x => x.GetExternalFlightPlanAsync(It.IsAny<String>()))
                .Returns(GetFlightPlan());

            //class to check
            FlightPlanManager flightManager = new FlightPlanManager(inner, extenalTestMock.Object);

            //act
            FlightPlan flightPlan = await flightManager.GetFlightPlanById(id);
            string excepted = JsonConvert.SerializeObject(exceptedFlightPlan);
            string getFlight = JsonConvert.SerializeObject(flightPlan);
            //assert
            extenalTestMock.Verify(mock => mock.GetExternalFlightPlanAsync(
                It.IsAny<String>()), Times.Once());
            Assert.AreEqual(excepted, getFlight);

            Assert.Pass();
        }

        private FlightPlan GetOriginalFlightPlan()
        {
            List<Segment> s = new List<Segment>()
            {
                new Segment(33.234, 31.18,650)
            };
            FlightPlan f = new FlightPlan(216, "swir1",
            new InitialLocation(33.244, 31.12, "2020-05-11T12:40:21Z"),
            s);

            return f;
        }

        private async Task<FlightPlan> GetFlightPlan()
        {
            List<Segment> s = new List<Segment>()
            {
                new Segment(33.234, 31.18,650)
            };
            FlightPlan f = new FlightPlan(216, "swir1",
            new InitialLocation(33.244, 31.12, "2020-05-11T12:40:21Z"),
            s);

            return await Task.FromResult(f);
        }
    }
}