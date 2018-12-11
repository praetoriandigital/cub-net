using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net;

namespace Cub.Tests
{
    public class PlanTests
    {
        [Test]
        public void LoadPlanWithProduct()
        {
            var json = @"{
                ""object"": ""plan"", 
                ""id"": ""pln_fFkXL6xWdSIWuZtE"",
                ""product"": ""prd_S1NTfyEzmFueE28Q"",
            }";

            var plan = CObjectFactory.FromJObject(JObject.Parse(json)) as Plan;

            Assert.NotNull(plan);

            // get related product

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var product = plan.Product;
            Assert.IsNotEmpty(product.Name);
        }
    }
}
