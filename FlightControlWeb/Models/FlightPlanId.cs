
namespace FlightControlWeb.Models
{
    public class FlightPlanId
    {
        public FlightPlanId(string id, FlightPlan f)
        {
            this.ID = id;
            this.FlightP = f;

        }
        public FlightPlan FlightP { get; set; }

        public string ID { get; set; }
    }
}
